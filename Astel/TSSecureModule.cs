using System;
using System.IO;
using System.Text;
using System.Xml.Linq;
using System.Windows.Forms;
using System.Security.Cryptography;

namespace Astel
{
    internal class TSSecureModule
    {
        // ============================================================
        // GLOBAL PATHS
        // ============================================================

        public static string ts_session_root_path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), Application.CompanyName?.Replace("ü", "u") ?? "Company", Application.ProductName);
        public static string ts_data_xml_path = Path.Combine(ts_session_root_path, Application.ProductName + "Data.xml");
        public static string ts_session_file = Path.Combine(ts_session_root_path, Application.ProductName + "Session.ini");
        public static string ts_session_container = Path.GetFileNameWithoutExtension(ts_session_file);
        public static string ts_data_backup_folder = Path.Combine(ts_session_root_path, "backups");
        public static string ts_data_file_name = Path.GetFileName(ts_data_xml_path);
        public static string ts_data_backup_extension_astel = ".astel";
        public static string ts_data_backup_extension_csv_name = "CSV";
        public static string ts_data_backup_extension_csv = ".csv";

        // ============================================================
        // ITERATION COUNTS
        // ============================================================

        public const int PasswordHashIterations = 100_000;

        // ======================================================================================================
        // MODULE USER FRIENDLY MESSAGE SEND
        // ======================================================================================================

        public static Func<string, string> GetErrorMessage = key => {
            if (string.IsNullOrEmpty(key))
            {
                return "An unknown error occurred";
            }
            if (AstelMain.TSProtectionErrorMessages.Messages != null && AstelMain.TSProtectionErrorMessages.Messages.TryGetValue(key, out var msg) && !string.IsNullOrEmpty(msg))
            {
                return msg;
            }
            return "An unknown error occurred";
        };
        private static string GetFormattedErrorMessage(string key, params object[] args)
        {
            var template = GetErrorMessage(key);
            try
            {
                return string.Format(template, args);
            }
            catch
            {
                return template;
            }
        }

        // ============================================================
        // AES-256-CBC + HMAC-SHA512 (Encrypt-then-MAC)
        // ============================================================

        public class TS_AES_Encryption
        {

            private static byte[] MasterKey;
            private const int SaltSize = 16;
            private const int IvSize = 16;
            private const int HmacSize = 64;
            private const int AesKeySize = 32;
            private static readonly object _keyLock = new object();

            // --------------------------------------------------------
            // SET MASTER KEY
            // --------------------------------------------------------

            public static void SetKey(byte[] key)
            {
                if (key == null)
                    throw new ArgumentNullException(nameof(key), GetErrorMessage("AES_KeyNull"));
                if (key.Length != 32)
                    throw new ArgumentException(GetErrorMessage("AES_KeyLengthInvalid"), nameof(key));
                if (MasterKey != null)
                    Array.Clear(MasterKey, 0, MasterKey.Length);
                MasterKey = (byte[])key.Clone();
            }

            // --------------------------------------------------------
            // ENCRYPT (Enhanced with secure cleanup)
            //
            // FORMAT:
            //
            // base64(
            //   version(1) ||
            //   salt(16) ||
            //   iv(16) ||
            //   ciphertext ||
            //   hmac(64)
            // )
            // --------------------------------------------------------

            public static string TS_AES_Encrypt(string plainText)
            {
                if (MasterKey == null)
                    throw new InvalidOperationException(GetErrorMessage("AES_MasterKeyNotSet"));
                if (plainText == null)
                    throw new ArgumentNullException(nameof(plainText), GetErrorMessage("AES_PlainTextNull"));
                byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);
                byte[] aesKey = null;
                byte[] hmacKey = null;
                byte[] cipherBytes = null;
                try
                {
                    // Random salt + IV
                    byte[] salt = new byte[SaltSize];
                    byte[] iv = new byte[IvSize];
                    using (var rng = RandomNumberGenerator.Create()) { rng.GetBytes(salt); rng.GetBytes(iv); }
                    // Derive subkeys with proper error handling
                    try
                    {
                        aesKey = DeriveSubKey(MasterKey, salt, "enc", AesKeySize);
                        hmacKey = DeriveSubKey(MasterKey, salt, "auth", HmacSize);
                    }
                    catch (Exception ex)
                    {
                        throw new CryptographicException(GetFormattedErrorMessage("AES_KeyDerivationFailed", ex.Message), ex);
                    }
                    // Encrypt
                    using (var aes = Aes.Create())
                    {
                        aes.Mode = CipherMode.CBC;
                        aes.Padding = PaddingMode.PKCS7;
                        aes.KeySize = 256;
                        aes.BlockSize = 128;
                        aes.Key = aesKey;
                        aes.IV = iv;
                        using (var ms = new MemoryStream())
                        {
                            using (var cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write))
                            {
                                cs.Write(plainBytes, 0, plainBytes.Length);
                                cs.FlushFinalBlock();
                                cipherBytes = ms.ToArray();
                            }
                        }
                    }
                    // HMAC(iv || ciphertext)
                    byte[] ivAndCipher = new byte[iv.Length + cipherBytes.Length];
                    Buffer.BlockCopy(iv, 0, ivAndCipher, 0, iv.Length);
                    Buffer.BlockCopy(cipherBytes, 0, ivAndCipher, iv.Length, cipherBytes.Length);
                    byte[] hmac;
                    using (var h = new HMACSHA512(hmacKey))
                    {
                        hmac = h.ComputeHash(ivAndCipher);
                    }
                    // Build payload
                    using (var outMs = new MemoryStream())
                    {
                        outMs.WriteByte(0x01); // version
                        outMs.Write(salt, 0, salt.Length);
                        outMs.Write(iv, 0, iv.Length);
                        outMs.Write(cipherBytes, 0, cipherBytes.Length);
                        outMs.Write(hmac, 0, hmac.Length);
                        return Convert.ToBase64String(outMs.ToArray());
                    }
                }
                catch (CryptographicException)
                {
                    throw;
                }
                catch (Exception ex)
                {
                    throw new CryptographicException(GetFormattedErrorMessage("AES_EncryptionFailed", ex.Message), ex);
                }
                finally
                {
                    // Secure cleanup of sensitive data
                    if (aesKey != null)
                        Array.Clear(aesKey, 0, aesKey.Length);
                    if (hmacKey != null)
                        Array.Clear(hmacKey, 0, hmacKey.Length);
                    if (plainBytes != null)
                        Array.Clear(plainBytes, 0, plainBytes.Length);
                }
            }

            // --------------------------------------------------------
            // DECRYPT (Enhanced with secure cleanup)
            // --------------------------------------------------------

            public static string TS_AES_Decrypt(string base64Input)
            {
                if (MasterKey == null)
                    throw new InvalidOperationException(GetErrorMessage("AES_MasterKeyNotSet"));
                if (base64Input == null)
                    throw new ArgumentNullException(nameof(base64Input), GetErrorMessage("AES_Base64InputNull"));
                byte[] aesKey = null;
                byte[] hmacKey = null;
                byte[] plainBytes = null;
                try
                {
                    byte[] input;
                    try
                    {
                        input = Convert.FromBase64String(base64Input);
                    }
                    catch (FormatException ex)
                    {
                        throw new CryptographicException(GetFormattedErrorMessage("AES_InvalidBase64", ex.Message), ex);
                    }
                    if (input.Length < 1 + SaltSize + IvSize + HmacSize)
                    {
                        throw new CryptographicException(GetErrorMessage("AES_InvalidCipherFormat"));
                    }
                    int pos = 0;
                    // Version
                    byte version = input[pos++];
                    if (version != 0x01)
                    {
                        throw new CryptographicException(GetFormattedErrorMessage("AES_UnsupportedVersion", version));
                    }
                    // Salt
                    byte[] salt = new byte[SaltSize];
                    Buffer.BlockCopy(input, pos, salt, 0, SaltSize);
                    pos += SaltSize;
                    // IV
                    byte[] iv = new byte[IvSize];
                    Buffer.BlockCopy(input, pos, iv, 0, IvSize);
                    pos += IvSize;
                    // Ciphertext
                    int cipherLen = input.Length - pos - HmacSize;
                    if (cipherLen <= 0)
                    {
                        throw new CryptographicException(GetErrorMessage("AES_InvalidCipherLength"));
                    }
                    byte[] cipherBytes = new byte[cipherLen];
                    Buffer.BlockCopy(input, pos, cipherBytes, 0, cipherLen);
                    pos += cipherLen;
                    // HMAC
                    byte[] hmac = new byte[HmacSize];
                    Buffer.BlockCopy(input, pos, hmac, 0, HmacSize);
                    // Re-derive keys
                    try
                    {
                        aesKey = DeriveSubKey(MasterKey, salt, "enc", AesKeySize);
                        hmacKey = DeriveSubKey(MasterKey, salt, "auth", HmacSize);
                    }
                    catch (Exception ex)
                    {
                        throw new CryptographicException(GetFormattedErrorMessage("AES_KeyDerivationFailedDecrypt", ex.Message), ex);
                    }
                    // Recompute HMAC(iv || ciphertext)
                    byte[] ivAndCipher = new byte[iv.Length + cipherBytes.Length];
                    Buffer.BlockCopy(iv, 0, ivAndCipher, 0, iv.Length);
                    Buffer.BlockCopy(cipherBytes, 0, ivAndCipher, iv.Length, cipherBytes.Length);
                    byte[] computedHmac;
                    using (var h = new HMACSHA512(hmacKey))
                    {
                        computedHmac = h.ComputeHash(ivAndCipher);
                    }
                    // Constant-time comparison (prevents timing attacks)
                    if (!FixedTimeEquals(hmac, computedHmac))
                    {
                        throw new CryptographicException(GetErrorMessage("AES_HMACValidationFailed"));
                    }
                    // Decrypt
                    using (var aes = Aes.Create())
                    {
                        aes.Mode = CipherMode.CBC;
                        aes.Padding = PaddingMode.PKCS7;
                        aes.KeySize = 256;
                        aes.BlockSize = 128;
                        aes.Key = aesKey;
                        aes.IV = iv;
                        using (var ms = new MemoryStream())
                        {
                            using (var cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Write))
                            {
                                cs.Write(cipherBytes, 0, cipherBytes.Length);
                                cs.FlushFinalBlock();
                                plainBytes = ms.ToArray();
                            }
                        }
                    }
                    try
                    {
                        return Encoding.UTF8.GetString(plainBytes);
                    }
                    catch (Exception ex)
                    {
                        throw new CryptographicException(GetFormattedErrorMessage("AES_InvalidUTF8", ex.Message), ex);
                    }
                }
                catch (CryptographicException)
                {
                    throw;
                }
                catch (Exception ex)
                {
                    throw new CryptographicException(GetFormattedErrorMessage("AES_DecryptionFailed", ex.Message), ex);
                }
                finally
                {
                    // Secure cleanup of sensitive data
                    if (aesKey != null)
                        Array.Clear(aesKey, 0, aesKey.Length);
                    if (hmacKey != null)
                        Array.Clear(hmacKey, 0, hmacKey.Length);
                    if (plainBytes != null)
                        Array.Clear(plainBytes, 0, plainBytes.Length);
                }
            }

            // --------------------------------------------------------
            // HKDF-HMAC-SHA512 KEY DERIVATION (RFC 5869)
            // Extract-and-Expand approach
            // --------------------------------------------------------

            private static byte[] HKDF_SHA512(byte[] inputKeyMaterial, byte[] salt, byte[] info, int outputLength)
            {
                // Validate inputs
                if (inputKeyMaterial == null)
                    throw new ArgumentNullException(nameof(inputKeyMaterial), GetErrorMessage("HKDF_InputKeyNull"));
                if (outputLength <= 0)
                    throw new ArgumentOutOfRangeException(nameof(outputLength), GetErrorMessage("HKDF_OutputLengthInvalid"));
                if (outputLength > 255 * 64)
                    throw new ArgumentOutOfRangeException(GetErrorMessage("HKDF_OutputLengthTooLarge"));
                // Handle null inputs
                if (salt == null || salt.Length == 0)
                    salt = new byte[64];
                if (info == null)
                    info = new byte[0];
                byte[] prk = null;
                byte[] previousT = new byte[0];
                try
                {
                    // HKDF-Extract
                    using (var hkdfExtract = new HMACSHA512(salt))
                    {
                        prk = hkdfExtract.ComputeHash(inputKeyMaterial);
                    }
                    // HKDF-Expand
                    byte[] okm = new byte[outputLength];
                    int iterations = (outputLength + 63) / 64;
                    for (int i = 0; i < iterations; i++)
                    {
                        byte[] t = null;
                        try
                        {
                            using (var hkdfExpand = new HMACSHA512(prk))
                            {
                                using (var ms = new MemoryStream())
                                {
                                    ms.Write(previousT, 0, previousT.Length);
                                    ms.Write(info, 0, info.Length);
                                    ms.WriteByte((byte)(i + 1));
                                    t = hkdfExpand.ComputeHash(ms.ToArray());
                                }
                            }
                            int toCopy = Math.Min(64, outputLength - (i * 64));
                            Buffer.BlockCopy(t, 0, okm, i * 64, toCopy);
                            if (previousT.Length > 0)
                            {
                                Array.Clear(previousT, 0, previousT.Length);
                            }
                            previousT = t;
                            t = null;
                        }
                        finally
                        {
                            if (t != null)
                                Array.Clear(t, 0, t.Length);
                        }
                    }
                    return okm;
                }
                finally
                {
                    if (prk != null)
                        Array.Clear(prk, 0, prk.Length);
                    if (previousT != null && previousT.Length > 0)
                        Array.Clear(previousT, 0, previousT.Length);
                }
            }

            // --------------------------------------------------------
            // SUBKEY DERIVATION (Enhanced with HKDF)
            // --------------------------------------------------------

            private static byte[] DeriveSubKey(byte[] masterKey, byte[] salt, string info, int outputLength)
            {
                if (masterKey == null)
                    throw new ArgumentNullException(nameof(masterKey), GetErrorMessage("DeriveSubKey_MasterKeyNull"));
                if (salt == null)
                    throw new ArgumentNullException(nameof(salt), GetErrorMessage("DeriveSubKey_SaltNull"));
                if (string.IsNullOrEmpty(info))
                    throw new ArgumentException(GetErrorMessage("DeriveSubKey_InfoEmpty"), nameof(info));
                byte[] infoBytes = null;
                try
                {
                    infoBytes = Encoding.UTF8.GetBytes(info);
                    return HKDF_SHA512(masterKey, salt, infoBytes, outputLength);
                }
                finally
                {
                    if (infoBytes != null)
                        Array.Clear(infoBytes, 0, infoBytes.Length);
                }
            }

            // --------------------------------------------------------
            // CONSTANT-TIME COMPARISON
            // --------------------------------------------------------

            public static bool FixedTimeEquals(byte[] a, byte[] b)
            {
                if (a == null || b == null || a.Length != b.Length)
                {
                    return false;
                }
                int diff = 0;
                for (int i = 0; i < a.Length; i++)
                {
                    diff |= a[i] ^ b[i];
                }
                return diff == 0;
            }

            // --------------------------------------------------------
            // DERIVE AES KEY FROM ASTEL DATA FILE MATERIAL
            // --------------------------------------------------------

            public static byte[] DeriveKeyFromMaterial(byte[] keyMaterial, byte[] salt)
            {
                if (keyMaterial == null)
                    throw new ArgumentNullException(nameof(keyMaterial), GetErrorMessage("DeriveSubKey_MasterKeyNull"));
                if (salt == null)
                    throw new ArgumentNullException(nameof(salt), GetErrorMessage("DeriveSubKey_SaltNull"));
                byte[] infoBytes = null;
                try
                {
                    infoBytes = Encoding.UTF8.GetBytes("AstelDataKey");
                    return HKDF_SHA512(keyMaterial, salt, infoBytes, 32);
                }
                finally
                {
                    if (infoBytes != null)
                        Array.Clear(infoBytes, 0, infoBytes.Length);
                }
            }

            // --------------------------------------------------------
            // EXTRACT KEY FROM ASTEL FILE
            // --------------------------------------------------------

            public static byte[] ExtractKeyFromAstelFile(string filePath)
            {
                try
                {
                    var doc = XDocument.Load(filePath);
                    var root = doc.Element("Datas");
                    string saltBase64 = root.Attribute("ST")?.Value?.Trim();
                    string keyMaterialBase64 = root.Attribute("EK")?.Value?.Trim();
                    if (string.IsNullOrEmpty(saltBase64) || string.IsNullOrEmpty(keyMaterialBase64))
                    {
                        return null;
                    }
                    byte[] salt = Convert.FromBase64String(saltBase64);
                    byte[] keyMaterial = Convert.FromBase64String(keyMaterialBase64);
                    try
                    {
                        return DeriveKeyFromMaterial(keyMaterial, salt);
                    }
                    finally
                    {
                        Array.Clear(keyMaterial, 0, keyMaterial.Length);
                        Array.Clear(salt, 0, salt.Length);
                    }
                }
                catch (Exception)
                {
                    return null;
                }
            }

            // --------------------------------------------------------
            // TEMPORARY KEY CHANGE (For Import)
            // --------------------------------------------------------

            public static T WithTempKey<T>(byte[] tempKey, Func<T> action)
            {
                if (tempKey == null)
                    throw new ArgumentNullException(nameof(tempKey), GetErrorMessage("TempKey_Null"));
                lock (_keyLock)
                {
                    byte[] oldKey = null;
                    if (MasterKey != null)
                    {
                        oldKey = (byte[])MasterKey.Clone();
                        Array.Clear(MasterKey, 0, MasterKey.Length);
                    }
                    try
                    {
                        MasterKey = (byte[])tempKey.Clone();
                        return action();
                    }
                    finally
                    {
                        if (MasterKey != null)
                        {
                            Array.Clear(MasterKey, 0, MasterKey.Length);
                        }
                        if (oldKey != null)
                        {
                            MasterKey = oldKey;
                        }
                    }
                }
            }

        }

        // ============================================================
        // FIXED TIME STRING COMPARISON
        // ============================================================

        public static bool FixedTimeStringEquals(string a, string b)
        {
            if (a == null || b == null)
                return false;
            byte[] aBytes = Encoding.UTF8.GetBytes(a);
            byte[] bBytes = Encoding.UTF8.GetBytes(b);
            try
            {
                return TS_AES_Encryption.FixedTimeEquals(aBytes, bBytes);
            }
            finally
            {
                Array.Clear(aBytes, 0, aBytes.Length);
                Array.Clear(bBytes, 0, bBytes.Length);
            }
        }

        // ============================================================
        // PBKDF2-HMAC-SHA256
        // ============================================================

        public static byte[] PBKDF2_HMAC_SHA256(string password, byte[] salt, int iterations, int outputBytes)
        {
            if (password == null)
                throw new ArgumentNullException(nameof(password), GetErrorMessage("PBKDF2_PasswordNull"));
            if (salt == null)
                throw new ArgumentNullException(nameof(salt), GetErrorMessage("PBKDF2_SaltNull"));
            if (iterations <= 0)
                throw new ArgumentOutOfRangeException(nameof(iterations), GetErrorMessage("PBKDF2_IterationsInvalid"));
            if (outputBytes <= 0)
                throw new ArgumentOutOfRangeException(nameof(outputBytes), GetErrorMessage("PBKDF2_OutputBytesInvalid"));
            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations, HashAlgorithmName.SHA256))
            {
                return pbkdf2.GetBytes(outputBytes);
            }
        }

        // ============================================================
        // SESSION DATA PROTECTION (DPAPI)
        // ============================================================

        public class TS_SessionProtection
        {
            private static readonly byte[] s_additionalEntropy = Encoding.UTF8.GetBytes($"{Application.ProductName}_Session_Protection_v2");
            public static string ProtectSessionData(string plainData)
            {
                if (string.IsNullOrEmpty(plainData))
                    throw new ArgumentNullException(nameof(plainData), GetErrorMessage("Session_PlainDataNull"));
                byte[] plainBytes = null;
                try
                {
                    plainBytes = Encoding.UTF8.GetBytes(plainData);
                    byte[] encrypted = ProtectedData.Protect(plainBytes, s_additionalEntropy, DataProtectionScope.CurrentUser);
                    return Convert.ToBase64String(encrypted);
                }
                finally
                {
                    if (plainBytes != null)
                        Array.Clear(plainBytes, 0, plainBytes.Length);
                }
            }
            public static string UnprotectSessionData(string protectedData)
            {
                if (string.IsNullOrEmpty(protectedData))
                    throw new ArgumentNullException(nameof(protectedData), GetErrorMessage("Session_ProtectedDataNull"));
                byte[] encrypted = null;
                byte[] plainBytes = null;
                try
                {
                    encrypted = Convert.FromBase64String(protectedData);
                    plainBytes = ProtectedData.Unprotect(encrypted, s_additionalEntropy, DataProtectionScope.CurrentUser);
                    return Encoding.UTF8.GetString(plainBytes);
                }
                finally
                {
                    if (encrypted != null)
                        Array.Clear(encrypted, 0, encrypted.Length);
                    if (plainBytes != null)
                        Array.Clear(plainBytes, 0, plainBytes.Length);
                }
            }
        }

        // ============================================================
        // PASSWORD HASH
        // ============================================================

        public static string TSHashPassword(string password, string saltBase64, int iterations = PasswordHashIterations)
        {
            if (password == null)
                throw new ArgumentNullException(nameof(password), GetErrorMessage("Hash_PasswordNull"));
            if (saltBase64 == null)
                throw new ArgumentNullException(nameof(saltBase64), GetErrorMessage("Hash_SaltNull"));
            byte[] salt;
            try
            {
                salt = Convert.FromBase64String(saltBase64);
            }
            catch (FormatException)
            {
                throw new ArgumentException(GetErrorMessage("Hash_SaltInvalid"));
            }
            byte[] hash = null;
            try
            {
                hash = PBKDF2_HMAC_SHA256(password, salt, iterations, 32);
                return Convert.ToBase64String(hash);
            }
            finally
            {
                if (hash != null)
                    Array.Clear(hash, 0, hash.Length);
                if (salt != null)
                    Array.Clear(salt, 0, salt.Length);
            }
        }

        // ============================================================
        // GENERATE SALT
        // ============================================================

        public static string GenerateSalt(int size = 32)
        {
            if (size <= 0)
                throw new ArgumentOutOfRangeException(nameof(size), GetErrorMessage("Salt_SizeInvalid"));
            byte[] salt = new byte[size];
            try
            {
                using (var rng = RandomNumberGenerator.Create())
                {
                    rng.GetBytes(salt);
                }
                return Convert.ToBase64String(salt);
            }
            finally
            {
                Array.Clear(salt, 0, salt.Length);
            }
        }

        // ============================================================
        // SECURE RANDOM STRING
        // ============================================================

        public static string GenerateSecureRandomString(int strLength)
        {
            if (strLength <= 0)
                throw new ArgumentOutOfRangeException(nameof(strLength), GetErrorMessage("Random_LengthInvalid"));
            const string chars = "abcdefghijklmnopqrstuvwxyz0123456789";
            int charsetSize = chars.Length;
            int rejectionThreshold = byte.MaxValue - (byte.MaxValue % charsetSize);
            char[] result = new char[strLength];
            using (var rng = RandomNumberGenerator.Create())
            {
                byte[] buffer = new byte[1];
                for (int i = 0; i < strLength; i++)
                {
                    byte randomByte;
                    do
                    {
                        rng.GetBytes(buffer);
                        randomByte = buffer[0];
                    } while (randomByte >= rejectionThreshold);
                    result[i] = chars[randomByte % charsetSize];
                }
            }
            return "ts_" + new string(result);
        }
    }
}
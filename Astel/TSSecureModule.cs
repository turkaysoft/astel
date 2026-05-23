using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Security.Cryptography;

namespace Astel{
    internal class TSSecureModule{

        // ============================================================
        // GLOBAL PATHS
        // ============================================================

        public static string ts_session_root_path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), Application.CompanyName?.Replace("ü", "u") ?? "Company", Application.ProductName);
        public static string ts_data_xml_path = Path.Combine(ts_session_root_path, Application.ProductName + "Data.xml");
        public static string ts_session_file = Path.Combine(ts_session_root_path, Application.ProductName + "Session.ini");
        public static string ts_session_container = Path.GetFileNameWithoutExtension(ts_session_file);
        public static string ts_data_backup_folder = Path.Combine(ts_session_root_path, "backups");
        public static string ts_data_backup_extension_astel = ".astel";
        public static string ts_data_backup_extension_csv_name = "CSV";
        public static string ts_data_backup_extension_csv = ".csv";

        // ============================================================
        // AES-256-CBC + HMAC-SHA512 (Encrypt-then-MAC)
        // ============================================================

        public class TS_AES_Encryption{
            private static byte[] MasterKey;
            private const int SaltSize = 16;
            private const int IvSize = 16;
            private const int HmacSize = 64;
            private const int AesKeySize = 32;

            // --------------------------------------------------------
            // SET MASTER KEY
            // --------------------------------------------------------

            public static void SetKey(byte[] key){
                if (key == null)
                    throw new ArgumentNullException(nameof(key));
                if (key.Length != 32)
                    throw new ArgumentException("Master key must be 32 bytes (256-bit).");
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

            public static string TS_AES_Encrypt(string plainText){
                if (MasterKey == null)
                    throw new InvalidOperationException("AES Master Key is not set. Call SetKey() first.");
                if (plainText == null)
                    throw new ArgumentNullException(nameof(plainText));
                byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);
                byte[] aesKey = null;
                byte[] hmacKey = null;
                byte[] cipherBytes = null;
                try{
                    // Random salt + IV
                    byte[] salt = new byte[SaltSize];
                    byte[] iv = new byte[IvSize];
                    using (var rng = RandomNumberGenerator.Create()){ rng.GetBytes(salt); rng.GetBytes(iv); }
                    // Derive subkeys with proper error handling
                    try{
                        aesKey = DeriveSubKey(MasterKey, salt, "enc", AesKeySize);
                        hmacKey = DeriveSubKey(MasterKey, salt, "auth", HmacSize);
                    }catch (Exception ex){
                        throw new CryptographicException("Key derivation failed during encryption: " + ex.Message, ex);
                    }
                    // Encrypt
                    using (var aes = new AesManaged()){
                        aes.Mode = CipherMode.CBC;
                        aes.Padding = PaddingMode.PKCS7;
                        aes.KeySize = 256;
                        aes.BlockSize = 128;
                        aes.Key = aesKey;
                        aes.IV = iv;
                        using (var ms = new MemoryStream()){
                            using (var cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write)){
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
                    using (var h = new HMACSHA512(hmacKey)){
                        hmac = h.ComputeHash(ivAndCipher);
                    }
                    // Build payload
                    using (var outMs = new MemoryStream()){
                        outMs.WriteByte(0x01); // version
                        outMs.Write(salt, 0, salt.Length);
                        outMs.Write(iv, 0, iv.Length);
                        outMs.Write(cipherBytes, 0, cipherBytes.Length);
                        outMs.Write(hmac, 0, hmac.Length);
                        return Convert.ToBase64String(outMs.ToArray());
                    }
                }catch (CryptographicException){
                    throw;
                }catch (Exception ex){
                    throw new CryptographicException("Encryption operation failed: " + ex.Message, ex);
                }
                finally{
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

            public static string TS_AES_Decrypt(string base64Input){
                if (MasterKey == null)
                    throw new InvalidOperationException("AES Master Key is not set. Call SetKey() first.");
                if (base64Input == null)
                    throw new ArgumentNullException(nameof(base64Input));
                byte[] aesKey = null;
                byte[] hmacKey = null;
                byte[] plainBytes = null;
                try{
                    byte[] input;
                    try{
                        input = Convert.FromBase64String(base64Input);
                    }catch (FormatException ex){
                        throw new CryptographicException("Invalid base64 format in ciphertext: " + ex.Message, ex);
                    }
                    if (input.Length < 1 + SaltSize + IvSize + HmacSize){
                        throw new CryptographicException("Invalid ciphertext format (too short).");
                    }
                    int pos = 0;
                    // Version
                    byte version = input[pos++];
                    if (version != 0x01){
                        throw new CryptographicException("Unsupported ciphertext version: " + version);
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
                    if (cipherLen <= 0){
                        throw new CryptographicException("Invalid ciphertext length.");
                    }
                    byte[] cipherBytes = new byte[cipherLen];
                    Buffer.BlockCopy(input, pos, cipherBytes, 0, cipherLen);
                    pos += cipherLen;
                    // HMAC
                    byte[] hmac = new byte[HmacSize];
                    Buffer.BlockCopy(input, pos, hmac, 0, HmacSize);
                    // Re-derive keys
                    try{
                        aesKey = DeriveSubKey(MasterKey, salt, "enc", AesKeySize);
                        hmacKey = DeriveSubKey(MasterKey, salt, "auth", HmacSize);
                    }catch (Exception ex){
                        throw new CryptographicException("Key derivation failed during decryption: " + ex.Message, ex);
                    }
                    // Recompute HMAC(iv || ciphertext)
                    byte[] ivAndCipher = new byte[iv.Length + cipherBytes.Length];
                    Buffer.BlockCopy(iv, 0, ivAndCipher, 0, iv.Length);
                    Buffer.BlockCopy(cipherBytes, 0, ivAndCipher, iv.Length, cipherBytes.Length);
                    byte[] computedHmac;
                    using (var h = new HMACSHA512(hmacKey)){
                        computedHmac = h.ComputeHash(ivAndCipher);
                    }
                    // Constant-time comparison (prevents timing attacks)
                    if (!FixedTimeEquals(hmac, computedHmac)){
                        throw new CryptographicException("HMAC validation failed. Data may be tampered or corrupted.");
                    }
                    // Decrypt
                    using (var aes = new AesManaged()){
                        aes.Mode = CipherMode.CBC;
                        aes.Padding = PaddingMode.PKCS7;
                        aes.KeySize = 256;
                        aes.BlockSize = 128;
                        aes.Key = aesKey;
                        aes.IV = iv;
                        using (var ms = new MemoryStream()){
                            using (var cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Write)){
                                cs.Write(cipherBytes, 0, cipherBytes.Length);
                                cs.FlushFinalBlock();
                                plainBytes = ms.ToArray();
                            }
                        }
                    }
                    try{
                        return Encoding.UTF8.GetString(plainBytes);
                    }catch (Exception ex){
                        throw new CryptographicException("Invalid UTF-8 decoded data: " + ex.Message, ex);
                    }
                }catch (CryptographicException){
                    throw;
                }catch (Exception ex){
                    throw new CryptographicException("Decryption operation failed: " + ex.Message, ex);
                }
                finally{
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

            private static byte[] HKDF_SHA512(byte[] inputKeyMaterial, byte[] salt, byte[] info, int outputLength){
                // Validate inputs
                if (inputKeyMaterial == null)
                    throw new ArgumentNullException(nameof(inputKeyMaterial));
                if (outputLength <= 0)
                    throw new ArgumentOutOfRangeException(nameof(outputLength));
                if (outputLength > 255 * 64) // 64 bytes = HMAC-SHA512 hash size
                    throw new ArgumentOutOfRangeException("Output length too large (max 16320 bytes).");
                // Handle null inputs
                if (salt == null || salt.Length == 0)
                    salt = new byte[64]; // Hash length of SHA512
                if (info == null)
                    info = new byte[0];
                // HKDF-Extract: PRK = HMAC-Hash(salt, IKM)
                byte[] prk;
                using (var hkdfExtract = new HMACSHA512(salt)){
                    prk = hkdfExtract.ComputeHash(inputKeyMaterial);
                }
                // HKDF-Expand: OKM = PRK repeated and expanded
                byte[] okm = new byte[outputLength];
                byte[] previousT = new byte[0];
                int iterations = (outputLength + 63) / 64; // Round up to 64-byte chunks
                for (int i = 0; i < iterations; i++){
                    // T(i) = HMAC-Hash(PRK, T(i-1) || info || counter)
                    using (var hkdfExpand = new HMACSHA512(prk)){
                        using (var ms = new MemoryStream()){
                            // T(i-1)
                            ms.Write(previousT, 0, previousT.Length);
                            // info
                            ms.Write(info, 0, info.Length);
                            // counter (1-indexed)
                            ms.WriteByte((byte)(i + 1));
                            byte[] t = hkdfExpand.ComputeHash(ms.ToArray());
                            // Copy to output
                            int toCopy = Math.Min(64, outputLength - (i * 64));
                            Buffer.BlockCopy(t, 0, okm, i * 64, toCopy);
                            previousT = t;
                        }
                    }
                }
                return okm;
            }

            // --------------------------------------------------------
            // SUBKEY DERIVATION (Enhanced with HKDF)
            // --------------------------------------------------------

            private static byte[] DeriveSubKey(byte[] masterKey, byte[] salt, string info, int outputLength){
                if (masterKey == null)
                    throw new ArgumentNullException(nameof(masterKey));
                if (salt == null)
                    throw new ArgumentNullException(nameof(salt));
                if (string.IsNullOrEmpty(info))
                    throw new ArgumentException("Info must not be null or empty.", nameof(info));
                byte[] infoBytes = Encoding.UTF8.GetBytes(info);
                // Use HKDF-HMAC-SHA512 for key derivation
                // This provides proper key separation between encryption and authentication keys
                return HKDF_SHA512(masterKey, salt, infoBytes, outputLength);
            }

            // --------------------------------------------------------
            // CONSTANT-TIME COMPARISON
            // --------------------------------------------------------

            public static bool FixedTimeEquals(byte[] a, byte[] b){
                if (a == null || b == null || a.Length != b.Length){
                    return false;
                }
                int diff = 0;
                for (int i = 0; i < a.Length; i++){
                    diff |= a[i] ^ b[i];
                }
                return diff == 0;
            }
        }

        // ============================================================
        // FIXED TIME STRING COMPARISON
        // ============================================================

        public static bool FixedTimeStringEquals(string a, string b){
            if (a == null || b == null)
                return false;
            byte[] aBytes = Encoding.UTF8.GetBytes(a);
            byte[] bBytes = Encoding.UTF8.GetBytes(b);
            try{
                return TS_AES_Encryption.FixedTimeEquals(aBytes, bBytes);
            }
            finally{
                Array.Clear(aBytes, 0, aBytes.Length);
                Array.Clear(bBytes, 0, bBytes.Length);
            }
        }

        // ============================================================
        // PBKDF2-HMAC-SHA512
        // ============================================================

        public static byte[] PBKDF2_HMAC_SHA512( string password, byte[] salt, int iterations, int outputBytes){
            if (password == null)
                throw new ArgumentNullException(nameof(password));
            if (salt == null)
                throw new ArgumentNullException(nameof(salt));
            if (iterations <= 0)
                throw new ArgumentOutOfRangeException(nameof(iterations));
            if (outputBytes <= 0)
                throw new ArgumentOutOfRangeException(nameof(outputBytes));
            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations, HashAlgorithmName.SHA512)){
                return pbkdf2.GetBytes(outputBytes);
            }
        }

        // ============================================================
        // SESSION DATA PROTECTION
        // ============================================================

        public class TS_SessionProtection{
            public static string ProtectSessionData(string plainData){
                if (string.IsNullOrEmpty(plainData))
                    throw new ArgumentNullException(nameof(plainData));
                return plainData;
            }
            public static string UnprotectSessionData(string protectedData){
                if (string.IsNullOrEmpty(protectedData))
                    throw new ArgumentNullException(nameof(protectedData));
                return protectedData;
            }
        }

        // ============================================================
        // PASSWORD HASH
        // ============================================================

        public static string TSHashPassword(string password, string saltBase64, int iterations = 210000){
            if (password == null)
                throw new ArgumentNullException(nameof(password));
            if (saltBase64 == null)
                throw new ArgumentNullException(nameof(saltBase64));
            byte[] salt;
            try{
                salt = Convert.FromBase64String(saltBase64);
            }catch (FormatException){
                throw new ArgumentException("Salt must be Base64 encoded.");
            }
            byte[] hash = PBKDF2_HMAC_SHA512(password, salt, iterations, 64);;
            return Convert.ToBase64String(hash);
        }

        // ============================================================
        // GENERATE SALT
        // ============================================================

        public static string GenerateSalt(int size = 16){
            if (size <= 0)
                throw new ArgumentOutOfRangeException(nameof(size));
            byte[] salt = new byte[size];
            using (var rng = RandomNumberGenerator.Create()){
                rng.GetBytes(salt);
            }
            return Convert.ToBase64String(salt);
        }

        // ============================================================
        // SECURE RANDOM STRING
        // ============================================================

        public static string GenerateSecureRandomString(int strLength){
            if (strLength <= 0)
                throw new ArgumentOutOfRangeException(nameof(strLength));
            const string chars ="abcdefghijklmnopqrstuvwxyz0123456789";
            // Determine rejection threshold to avoid modulo bias
            // We want uniform distribution across charset
            int charsetSize = chars.Length; // 36
            int rejectionThreshold = byte.MaxValue - (byte.MaxValue % charsetSize);
            char[] result = new char[strLength];
            using (var rng = RandomNumberGenerator.Create()){
                byte[] buffer = new byte[1];
                for (int i = 0; i < strLength; i++){
                    // Retry until we get a value below rejection threshold
                    byte randomByte;
                    do{
                        rng.GetBytes(buffer);
                        randomByte = buffer[0];
                    } while (randomByte >= rejectionThreshold);
                    // Now we have uniform distribution
                    result[i] = chars[randomByte % charsetSize];
                }
            }
            return "ts_" + new string(result);
        }
    }
}
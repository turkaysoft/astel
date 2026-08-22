using System;
using System.IO;
using System.Text;
using System.Xml.Linq;
using System.Windows.Forms;
using System.Threading.Tasks;
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
        public static string ts_data_backup_folder = Path.Combine(ts_session_root_path, "backups");
        public static string ts_data_file_name = Path.GetFileName(ts_data_xml_path);
        public static string ts_data_backup_extension_astel = ".astel";
        public static string ts_data_backup_extension_csv_name = "CSV";
        public static string ts_data_backup_extension_csv = ".csv";

        // ============================================================
        // VAULT FORMAT (v0x02)
        // ============================================================

        public const string VaultV0x02 = "2";
        public const string VaultKDF = "PBKDF2-SHA512";
        public const int VaultIterations = 210_000;

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
            private const byte PayloadVersion = 0x02;
            private static readonly object _keyLock = new object();

            // ============================================================
            // SET MASTER KEY
            // ============================================================

            public static void SetKey(byte[] key)
            {
                lock (_keyLock)
                {
                    if (key == null)
                        throw new ArgumentNullException(nameof(key), GetErrorMessage("AES_KeyNull"));
                    if (key.Length != AesKeySize)
                        throw new ArgumentException(string.Format(GetErrorMessage("AES_KeyLengthInvalid"), AesKeySize, AesKeySize * 8), nameof(key));
                    if (MasterKey != null)
                        Array.Clear(MasterKey, 0, MasterKey.Length);
                    MasterKey = (byte[])key.Clone();
                }
            }

            // ============================================================
            // CLEAR MASTER KEY (lock / shutdown)
            // ============================================================

            public static void ClearKey()
            {
                lock (_keyLock)
                {
                    if (MasterKey != null)
                    {
                        Array.Clear(MasterKey, 0, MasterKey.Length);
                        MasterKey = null;
                    }
                }
            }

            // ============================================================
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
            // ============================================================

            public static string TS_AES_Encrypt(string plainText)
            {
                lock (_keyLock)
                {
                    return TS_AES_EncryptCore(plainText);
                }
            }

            private static string TS_AES_EncryptCore(string plainText)
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
                        outMs.WriteByte(PayloadVersion); // version
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

            // ============================================================
            // DECRYPT (Enhanced with secure cleanup)
            // ============================================================

            public static string TS_AES_Decrypt(string base64Input)
            {
                lock (_keyLock)
                {
                    return TS_AES_DecryptCore(base64Input);
                }
            }

            private static string TS_AES_DecryptCore(string base64Input)
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
                    if (version != PayloadVersion)
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

            // ============================================================
            // HKDF-HMAC-SHA512 KEY DERIVATION (RFC 5869)
            // Extract-and-Expand approach
            // ============================================================

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

            // ============================================================
            // SUBKEY DERIVATION (Enhanced with HKDF)
            // ============================================================

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

            // ============================================================
            // CONSTANT-TIME COMPARISON
            // ============================================================

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

            // ============================================================
            // TEMPORARY KEY CHANGE (For Import)
            // ============================================================

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
        // PBKDF2-HMAC-SHA512 (manual RFC 2898)
        // ============================================================

        public static byte[] PBKDF2_HMAC_SHA512(string password, byte[] salt, int iterations, int outputBytes)
        {
            if (password == null)
                throw new ArgumentNullException(nameof(password), GetErrorMessage("PBKDF2_PasswordNull"));
            if (salt == null)
                throw new ArgumentNullException(nameof(salt), GetErrorMessage("PBKDF2_SaltNull"));
            if (iterations <= 0)
                throw new ArgumentOutOfRangeException(nameof(iterations), GetErrorMessage("PBKDF2_IterationsInvalid"));
            if (outputBytes <= 0)
                throw new ArgumentOutOfRangeException(nameof(outputBytes), GetErrorMessage("PBKDF2_OutputBytesInvalid"));
            const int hLen = 64;
            byte[] passwordBytes = null;
            byte[] blockInput = null;
            byte[] u = null;
            byte[] uPrev = null;
            byte[] next = null;
            byte[] result = new byte[outputBytes];
            try
            {
                passwordBytes = Encoding.UTF8.GetBytes(password);
                using (var hmac = new HMACSHA512(passwordBytes))
                {
                    int l = (outputBytes + hLen - 1) / hLen;
                    for (int block = 1; block <= l; block++)
                    {
                        // U1 = PRF(P, S || INT(i))
                        blockInput = new byte[salt.Length + 4];
                        Buffer.BlockCopy(salt, 0, blockInput, 0, salt.Length);
                        blockInput[salt.Length] = (byte)((block >> 24) & 0xFF);
                        blockInput[salt.Length + 1] = (byte)((block >> 16) & 0xFF);
                        blockInput[salt.Length + 2] = (byte)((block >> 8) & 0xFF);
                        blockInput[salt.Length + 3] = (byte)(block & 0xFF);
                        uPrev = hmac.ComputeHash(blockInput);
                        Array.Clear(blockInput, 0, blockInput.Length);
                        blockInput = null;
                        u = (byte[])uPrev.Clone();
                        for (int i = 1; i < iterations; i++)
                        {
                            next = hmac.ComputeHash(uPrev);
                            for (int j = 0; j < hLen; j++)
                            {
                                u[j] ^= next[j];
                            }
                            Array.Clear(uPrev, 0, uPrev.Length);
                            uPrev = next;
                            next = null;
                        }
                        int copyLen = Math.Min(hLen, outputBytes - (block - 1) * hLen);
                        Buffer.BlockCopy(u, 0, result, (block - 1) * hLen, copyLen);
                        Array.Clear(u, 0, u.Length);
                        u = null;
                        Array.Clear(uPrev, 0, uPrev.Length);
                        uPrev = null;
                    }
                }
                return result;
            }
            finally
            {
                if (passwordBytes != null)
                    Array.Clear(passwordBytes, 0, passwordBytes.Length);
                if (blockInput != null)
                    Array.Clear(blockInput, 0, blockInput.Length);
                if (u != null)
                    Array.Clear(u, 0, u.Length);
                if (uPrev != null)
                    Array.Clear(uPrev, 0, uPrev.Length);
                if (next != null)
                    Array.Clear(next, 0, next.Length);
            }
        }

        // ============================================================
        // VAULT MASTER KEY DERIVATION (v0x02)
        // ============================================================
        // PBKDF2-HMAC-SHA512(password, AS, IT, 64)
        //   [0..31]  = AES-256 field-encryption master key
        //   [32..63] = verifier (PV)

        public static (byte[] Key, byte[] Verifier) DeriveVaultKey(string password, byte[] salt, int iterations)
        {
            if (salt == null)
                throw new ArgumentNullException(nameof(salt), GetErrorMessage("PBKDF2_SaltNull"));
            byte[] derived = null;
            try
            {
                derived = PBKDF2_HMAC_SHA512(password, salt, iterations, 64);
                byte[] key = new byte[32];
                byte[] verifier = new byte[32];
                Buffer.BlockCopy(derived, 0, key, 0, 32);
                Buffer.BlockCopy(derived, 32, verifier, 0, 32);
                return (key, verifier);
            }
            finally
            {
                if (derived != null)
                    Array.Clear(derived, 0, derived.Length);
            }
        }

        // ============================================================
        // ATOMIC XML SAVE
        // ============================================================

        public static void TSXmlAtomicSave(XDocument doc, string path)
        {
            if (doc == null)
                throw new ArgumentNullException(nameof(doc));
            if (string.IsNullOrEmpty(path))
                throw new ArgumentNullException(nameof(path));
            string dir = Path.GetDirectoryName(path);
            if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            string tempPath = path + ".tmp";
            doc.Save(tempPath);
            if (File.Exists(path))
            {
                File.Replace(tempPath, path, null);
            }
            else
            {
                File.Move(tempPath, path);
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
        // SAFE URL BUILDER (http/https only)
        // ============================================================

        public static string TryBuildSafeUrl(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return string.Empty;
            string trimmed = input.Trim();
            if (!Uri.TryCreate(trimmed, UriKind.Absolute, out Uri uri))
                return string.Empty;
            if (uri.Scheme == Uri.UriSchemeHttp || uri.Scheme == Uri.UriSchemeHttps)
                return trimmed;
            return string.Empty;
        }

        // ============================================================
        // SECURITY STATE
        // ============================================================

        // ============================================================
        // CLIPBOARD SECURITY: remember copied text, clear on shutdown
        // ============================================================

        public static class TSClipboardSecurity
        {
            internal static string _lastCopiedClipboard = null;

            public static void TrackCopiedText(string copiedText)
            {
                _lastCopiedClipboard = copiedText;
                ScheduleClipboardClear(copiedText);
            }

            public static void ClearOwnClipboardIfPresent()
            {
                try
                {
                    if (_lastCopiedClipboard != null && Clipboard.GetText() == _lastCopiedClipboard)
                    {
                        Clipboard.Clear();
                    }
                }
                catch { }
                finally
                {
                    _lastCopiedClipboard = null;
                }
            }

            private static void ScheduleClipboardClear(string copiedText)
            {
                string captured = copiedText;
                TaskScheduler scheduler;
                try
                {
                    scheduler = TaskScheduler.FromCurrentSynchronizationContext();
                }
                catch (InvalidOperationException)
                {
                    scheduler = TaskScheduler.Default;
                }
                Task.Delay(30000).ContinueWith(_ =>
                {
                    try
                    {
                        if (Clipboard.GetText() == captured)
                        {
                            Clipboard.Clear();
                        }
                    }
                    catch { }
                }, scheduler);
            }
        }

        // ============================================================
        // LOGIN / UNLOCK THROTTLE: 3 failed attempts -> 30 s lockout
        // ============================================================

        public class TSLoginThrottle
        {
            public const int MaxAttempts = 3;
            public const int LockoutSeconds = 30;

            private int _failedAttempts = 0;
            private int _lockoutRemaining = 0;
            private DateTime _lockoutUntil = DateTime.MinValue;

            public int FailedAttempts => _failedAttempts;
            public bool IsLockedOut => DateTime.Now < _lockoutUntil;
            public int RemainingSeconds => (int)(_lockoutUntil - DateTime.Now).TotalSeconds;
            public int LockoutRemaining => _lockoutRemaining;
            public bool ShouldStartLockout => _failedAttempts >= MaxAttempts;

            public void RecordFailure() => _failedAttempts++;

            public void StartLockout()
            {
                _lockoutUntil = DateTime.Now.AddSeconds(LockoutSeconds);
                _lockoutRemaining = LockoutSeconds;
                _failedAttempts = MaxAttempts;
            }

            public bool Tick()
            {
                _lockoutRemaining--;
                if (_lockoutRemaining <= 0)
                {
                    Reset();
                    return true;
                }
                return false;
            }

            public void Reset()
            {
                _failedAttempts = 0;
                _lockoutRemaining = 0;
                _lockoutUntil = DateTime.MinValue;
            }
        }
    }
}
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace FocusFM.Common.Helpers
{
    public class EncryptionDecryption
    {
        #region Variable Declaration

        /// <summary>
        /// key String
        /// </summary>
        private IConfiguration _config;
        private static string keyString;
        private const int _saltSize = 16; // 128 bits
        private const int _keySize = 32; // 256 bits
        private const int _iterations = 100000;
        private static readonly HashAlgorithmName _algorithm = HashAlgorithmName.SHA512;
        private const char segmentDelimiter = ':';

        #endregion
        public EncryptionDecryption(IConfiguration config)
        {
            _config = config;
            keyString = _config["Encryption:KeyString"];
        }
        #region Encrypt/Decrypt

        /// <summary>
        /// Get Encrypted Value of Passed value
        /// </summary>
        /// <param name="value">value to Encrypted</param>
        /// <returns>encrypted string</returns>
        public static string GetEncrypt(string value)
        {
            return Encrypt(keyString, value);
        }

        /// <summary>
        /// Get Decrypted value of passed encrypted string
        /// </summary>
        /// <param name="value">value to Decrypted</param>
        /// <returns>Decrypted string</returns>
        public static string GetDecrypt(string value)
        {
            return Decrypt(keyString, value);
        }

        private static Aes CreateAesEncryptor(string strKey)
        {
            Aes encryptor = Aes.Create();
            var pdb = new Rfc2898DeriveBytes(strKey, new byte[] { 0x56, 0x61, 0x69, 0x62, 0x48, 0x61, 0x76, 0x50, 0x61, 0x72, 0x65, 0x6b, 0x68 }, _iterations, _algorithm);
            encryptor.Key = pdb.GetBytes(32);
            encryptor.IV = pdb.GetBytes(16);
            encryptor.Mode = CipherMode.CBC;
            encryptor.Padding = PaddingMode.PKCS7;
            return encryptor;
        }

        private static string Decrypt(string strKey, string strData)
        {
            byte[] cipherBytes = Convert.FromBase64String(strData);
            using (Aes encryptor = CreateAesEncryptor(strKey))
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(cipherBytes, 0, cipherBytes.Length);
                    cs.Close();
                }
                return Encoding.Unicode.GetString(ms.ToArray());
            }
        }

        private static string Encrypt(string strKey, string strData)
        {
            byte[] clearBytes = Encoding.Unicode.GetBytes(strData);
            using (Aes encryptor = CreateAesEncryptor(strKey))
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(clearBytes, 0, clearBytes.Length);
                    cs.Close();
                }
                return Convert.ToBase64String(ms.ToArray());
            }
        }

        public static string Hash(string input)
        {
            byte[] salt = RandomNumberGenerator.GetBytes(_saltSize);
            byte[] hash = Rfc2898DeriveBytes.Pbkdf2(
                input,
                salt,
                _iterations,
                _algorithm,
                _keySize
            );
            return string.Join(
                segmentDelimiter,
                Convert.ToHexString(hash),
                Convert.ToHexString(salt),
                _iterations,
                _algorithm
            );
        }

        public static bool Verify(string input, string hashString, string saltString)
        {
            byte[] hash = Convert.FromHexString(hashString);
            byte[] salt = Convert.FromHexString(saltString);
            byte[] inputHash = Rfc2898DeriveBytes.Pbkdf2(
                input,
                salt,
                _iterations,
                _algorithm,
                hash.Length
            );

            return CryptographicOperations.FixedTimeEquals(inputHash, hash);
        }
        #endregion
    }
}
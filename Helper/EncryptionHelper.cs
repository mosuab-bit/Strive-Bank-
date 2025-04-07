using System;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace BankSystem.API.Helper
{
    public static class EncryptionHelper
    {
        private static byte[] _key;

        // يجب استدعاء هذا الميثود عند بدء تشغيل التطبيق لتحميل المفتاح
        public static void Initialize(IConfiguration configuration)
        {
            string keyBase64 = configuration["EncryptionSettings:AESKey"];
            if (string.IsNullOrEmpty(keyBase64))
                throw new Exception("Error: AES encryption key is missing or not set in appsettings.json.");

            _key = Convert.FromBase64String(keyBase64);
        }

        public static string Encrypt(string text)
        {
            if (_key == null)
                throw new Exception("Error: The encryption key is not initialized. Call EncryptionHelper.Initialize() first.");

            using Aes aes = Aes.Create();
            aes.Key = _key;
            aes.GenerateIV();

            using var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
            byte[] inputBytes = Encoding.UTF8.GetBytes(text);
            byte[] encryptedBytes = encryptor.TransformFinalBlock(inputBytes, 0, inputBytes.Length);

            byte[] result = new byte[aes.IV.Length + encryptedBytes.Length];
            Array.Copy(aes.IV, 0, result, 0, aes.IV.Length);
            Array.Copy(encryptedBytes, 0, result, aes.IV.Length, encryptedBytes.Length);

            return Convert.ToBase64String(result);
        }

        public static string Decrypt(string encryptedText)
        {
            if (_key == null)
                throw new Exception("Error: The encryption key is not initialized. Call EncryptionHelper.Initialize() first.");

            byte[] fullCipher = Convert.FromBase64String(encryptedText);

            using Aes aes = Aes.Create();
            aes.Key = _key;

            byte[] iv = new byte[16];
            byte[] cipherText = new byte[fullCipher.Length - iv.Length];

            Array.Copy(fullCipher, 0, iv, 0, iv.Length);
            Array.Copy(fullCipher, iv.Length, cipherText, 0, cipherText.Length);

            using var decryptor = aes.CreateDecryptor(aes.Key, iv);
            byte[] decryptedBytes = decryptor.TransformFinalBlock(cipherText, 0, cipherText.Length);

            return Encoding.UTF8.GetString(decryptedBytes);
        }
    }
}

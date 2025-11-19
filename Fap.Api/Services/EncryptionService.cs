using Fap.Api.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace Fap.Api.Services
{
    /// <summary>
    /// AES-256 encryption service for sensitive data
    /// </summary>
    public class EncryptionService : IEncryptionService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<EncryptionService> _logger;
        private readonly byte[] _key;
        private readonly byte[] _iv;

        public EncryptionService(IConfiguration configuration, ILogger<EncryptionService> logger)
        {
            _configuration = configuration;
            _logger = logger;
            
            // Get encryption key from configuration (MUST be in appsettings or environment variables)
            var encryptionKey = _configuration["Encryption:Key"];
            
            if (string.IsNullOrEmpty(encryptionKey))
            {
                _logger.LogWarning("?? Encryption:Key not found in configuration. Generating temporary key.");
                encryptionKey = GenerateEncryptionKey();
            }
            
            // Derive 256-bit key from config string
            using var sha = SHA256.Create();
            _key = sha.ComputeHash(Encoding.UTF8.GetBytes(encryptionKey));
            
            // Generate IV from key (first 16 bytes)
            _iv = _key.Take(16).ToArray();
        }

        public async Task<string> EncryptAsync(string plainText)
        {
            if (string.IsNullOrEmpty(plainText))
                throw new ArgumentNullException(nameof(plainText));

            try
            {
                using var aes = Aes.Create();
                aes.Key = _key;
                aes.IV = _iv;
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;

                var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using var msEncrypt = new MemoryStream();
                using var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write);
                using (var swEncrypt = new StreamWriter(csEncrypt))
                {
                    await swEncrypt.WriteAsync(plainText);
                }

                var encrypted = msEncrypt.ToArray();
                return Convert.ToBase64String(encrypted);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "? Encryption failed");
                throw new InvalidOperationException("Encryption failed", ex);
            }
        }

        public async Task<string> DecryptAsync(string cipherText)
        {
            if (string.IsNullOrEmpty(cipherText))
                throw new ArgumentNullException(nameof(cipherText));

            try
            {
                var buffer = Convert.FromBase64String(cipherText);

                using var aes = Aes.Create();
                aes.Key = _key;
                aes.IV = _iv;
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;

                var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                using var msDecrypt = new MemoryStream(buffer);
                using var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read);
                using var srDecrypt = new StreamReader(csDecrypt);

                return await srDecrypt.ReadToEndAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "? Decryption failed");
                throw new InvalidOperationException("Decryption failed", ex);
            }
        }

        public string GenerateEncryptionKey()
        {
            using var rng = RandomNumberGenerator.Create();
            var keyBytes = new byte[32]; // 256 bits
            rng.GetBytes(keyBytes);
            return Convert.ToBase64String(keyBytes);
        }
    }
}

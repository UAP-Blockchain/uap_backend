namespace Fap.Api.Interfaces
{
    /// <summary>
    /// Service for encrypting and decrypting sensitive data (e.g., private keys)
    /// </summary>
    public interface IEncryptionService
    {
        /// <summary>
        /// Encrypt a string value
        /// </summary>
        /// <param name="plainText">Plain text to encrypt</param>
        /// <returns>Encrypted string (Base64 encoded)</returns>
        Task<string> EncryptAsync(string plainText);
        
        /// <summary>
        /// Decrypt an encrypted string
        /// </summary>
        /// <param name="cipherText">Encrypted text (Base64 encoded)</param>
        /// <returns>Decrypted plain text</returns>
        Task<string> DecryptAsync(string cipherText);
        
        /// <summary>
        /// Generate a random encryption key
        /// </summary>
        /// <returns>Base64 encoded key</returns>
        string GenerateEncryptionKey();
    }
}

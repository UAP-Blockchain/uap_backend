namespace Fap.Domain.DTOs.Wallet
{
    /// <summary>
    /// Wallet information returned to services
    /// </summary>
    public class WalletInfo
    {
        /// <summary>
        /// Ethereum wallet address (0x...)
        /// </summary>
        public string Address { get; set; } = string.Empty;
        
        /// <summary>
        /// Public key (hex string)
        /// </summary>
        public string PublicKey { get; set; } = string.Empty;
        
        /// <summary>
        /// Indicates if this is a newly generated wallet
        /// </summary>
        public bool IsNewWallet { get; set; }
        
        /// <summary>
        /// Timestamp when wallet was created/retrieved
        /// </summary>
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
    
    /// <summary>
    /// Wallet creation result
    /// </summary>
    public class CreateWalletResult
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public WalletInfo? Wallet { get; set; }
        public List<string> Errors { get; set; } = new();
    }
}

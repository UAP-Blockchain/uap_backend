namespace Fap.Domain.DTOs.Auth
{
    public class BlockchainInfo
    {
        public string WalletAddress { get; set; } = string.Empty;
        public string TransactionHash { get; set; } = string.Empty;
        public long BlockNumber { get; set; }
        public DateTime RegisteredAt { get; set; }
    }
}

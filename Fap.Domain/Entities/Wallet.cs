using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fap.Domain.Entities
{
    /// <summary>
    /// Stores encrypted wallet information
    /// </summary>
    [Table("Wallets")]
    public class Wallet
    {
        [Key]
        public Guid Id { get; set; }
        
        /// <summary>
        /// Ethereum wallet address (unique identifier)
        /// </summary>
        [Required]
        [MaxLength(42)]
        public string Address { get; set; } = string.Empty;
        
        /// <summary>
        /// Encrypted private key (NEVER store plain text!)
        /// </summary>
        [Required]
        public string EncryptedPrivateKey { get; set; } = string.Empty;
        
        /// <summary>
        /// Public key (can be stored as plain text)
        /// </summary>
        [Required]
        [MaxLength(132)]
        public string PublicKey { get; set; } = string.Empty;
        
        /// <summary>
        /// User ID who owns this wallet (nullable for system wallets)
        /// </summary>
        public Guid? UserId { get; set; }
        
        /// <summary>
        /// Foreign key to User
        /// </summary>
        [ForeignKey(nameof(UserId))]
        public User? User { get; set; }
        
        /// <summary>
        /// Indicates if this wallet is active
        /// </summary>
        public bool IsActive { get; set; } = true;
        
        /// <summary>
        /// Timestamp when wallet was created
        /// </summary>
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        /// <summary>
        /// Last time wallet was accessed/used
        /// </summary>
        public DateTime? LastUsedAt { get; set; }
    }
}

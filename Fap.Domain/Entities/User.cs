using Fap.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Net;
using System.Security.Claims;

namespace Fap.Domain.Entities
{
    [Table("Users")]
    public class User
    {
        [Key] public Guid Id { get; set; }
        [Required, MaxLength(150)] public string FullName { get; set; }
        [Required, MaxLength(150)] public string Email { get; set; }
        [Required] public string PasswordHash { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Required] public Guid RoleId { get; set; }
        [ForeignKey(nameof(RoleId))] public Role Role { get; set; }

        [MaxLength(20)]
        public string? PhoneNumber { get; set; }

    [MaxLength(1024)]
    public string? ProfileImageUrl { get; set; }

    [MaxLength(255)]
    public string? ProfileImagePublicId { get; set; }

        [MaxLength(42)]
        public string? WalletAddress { get; set; }
        [MaxLength(66)]
        public string? BlockchainTxHash { get; set; }
        public long? BlockNumber { get; set; }
        public DateTime? BlockchainRegisteredAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public virtual ICollection<RefreshToken> RefreshTokens { get; set; }
        public virtual ICollection<ActionLog> ActionLogs { get; set; }
        public Student Student { get; set; }
        public Teacher Teacher { get; set; }
    }
}

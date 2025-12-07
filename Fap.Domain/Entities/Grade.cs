using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fap.Domain.Entities
{
    [Table("Grades")]
    public class Grade
    {
        [Key] public Guid Id { get; set; }

        [Required] public Guid StudentId { get; set; }
        [ForeignKey(nameof(StudentId))] public Student Student { get; set; }

        [Required] public Guid SubjectId { get; set; }
        [ForeignKey(nameof(SubjectId))] public Subject Subject { get; set; }

        [Required] public Guid GradeComponentId { get; set; }
        [ForeignKey(nameof(GradeComponentId))] public GradeComponent GradeComponent { get; set; }

        
        [Range(0, 10)] public decimal? Score { get; set; }
        [MaxLength(5)] public string? LetterGrade { get; set; }
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // ===== ON-CHAIN (GradeManagement) =====
        // gradeId trong GradeManagement (uint256)
        public ulong? OnChainGradeId { get; set; }

        // Hash của transaction recordGrade/updateGrade/approveGrade
        [MaxLength(200)] public string? OnChainTxHash { get; set; }

        // Block number của transaction
        public long? OnChainBlockNumber { get; set; }

        // Chain ID (ví dụ 31337, 11155111...)
        public int? OnChainChainId { get; set; }

        // Địa chỉ contract GradeManagement đã sử dụng
        [MaxLength(100)] public string? OnChainContractAddress { get; set; }
    }
}

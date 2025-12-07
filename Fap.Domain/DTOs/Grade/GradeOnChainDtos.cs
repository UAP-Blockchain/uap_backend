using System;

namespace Fap.Domain.DTOs.Grade
{
    /// <summary>
    /// Payload FE dùng để gọi GradeManagement.recordGrade(...)
    /// </summary>
    public class GradeOnChainPrepareDto
    {
        public Guid GradeId { get; set; }
        public Guid StudentId { get; set; }
        public string StudentWalletAddress { get; set; } = string.Empty;

        public Guid ClassId { get; set; }
        public string ComponentName { get; set; } = string.Empty;
        public decimal Score { get; set; }
        public decimal MaxScore { get; set; }

        // Giá trị đã convert cho contract (uint256)
        public ulong OnChainClassId { get; set; }
        public ulong OnChainScore { get; set; }
        public ulong OnChainMaxScore { get; set; }
    }

    /// <summary>
    /// FE gửi về sau khi đã gửi tx recordGrade thành công
    /// </summary>
    public class SaveGradeOnChainRequest
    {
        public string TransactionHash { get; set; } = string.Empty;
        public long BlockNumber { get; set; }
        public int ChainId { get; set; }
        public string ContractAddress { get; set; } = string.Empty;

        // Optional: FE có thể parse event GradeRecorded và gửi kèm gradeId on-chain
        public ulong? OnChainGradeId { get; set; }
    }
}

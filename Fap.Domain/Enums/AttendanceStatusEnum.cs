namespace Fap.Domain.Enums
{
    /// <summary>
    /// Must match DataTypes.AttendanceStatus in Solidity:
    /// 0: PRESENT, 1: ABSENT, 2: LATE, 3: EXCUSED
    /// </summary>
    public enum AttendanceStatusEnum : byte
    {
        Present = 0, // Student present
        Absent  = 1, // Student absent
        Late    = 2, // Student late
        Excused = 3  // Excused absence
    }
}

using System.IO;
using Fap.Domain.DTOs.Common;
using Fap.Domain.DTOs.Student;

namespace Fap.Api.Interfaces
{
    public interface IStudentService
    {
        Task<PagedResult<StudentDto>> GetStudentsAsync(GetStudentsRequest request);
        Task<StudentDetailDto?> GetStudentByIdAsync(Guid id);
        Task<StudentDetailDto?> GetStudentByUserIdAsync(Guid userId);
        
        /// <summary>
     /// Get current logged-in student's profile
        /// </summary>
  Task<StudentDetailDto?> GetCurrentStudentProfileAsync(Guid userId);

     /// <summary>
     /// Update the current student's profile image and return the new URL
     /// </summary>
     Task<StudentProfileImageDto> UpdateProfileImageAsync(Guid userId, Stream imageStream, string fileName);
        
        /// <summary>
        /// Get students eligible for a specific subject/class
        /// For admin to select students when assigning to a class
 /// </summary>
        Task<PagedResult<StudentDto>> GetEligibleStudentsForClassAsync(
 Guid classId,
    int page = 1,
        int pageSize = 20,
    string? searchTerm = null);
    }
}

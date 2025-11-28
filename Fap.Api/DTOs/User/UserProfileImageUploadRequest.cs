using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Fap.Api.DTOs.User
{
    public class UserProfileImageUploadRequest
    {
        [Required]
        public IFormFile File { get; set; } = default!;
    }
}

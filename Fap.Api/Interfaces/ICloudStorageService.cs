using System.IO;
using System.Threading.Tasks;
using Fap.Domain.DTOs.Common;

namespace Fap.Api.Interfaces
{
    public interface ICloudStorageService
    {
        /// <summary>
        /// Upload PDF to cloud storage
        /// </summary>
        Task<string> UploadPdfAsync(byte[] pdfBytes, string fileName);

        /// <summary>
        /// Delete PDF from cloud storage
        /// </summary>
        Task<bool> DeletePdfAsync(string fileName);

        /// <summary>
        /// Download PDF from cloud storage
        /// </summary>
        Task<byte[]?> DownloadPdfAsync(string url);

        /// <summary>
        /// Check if file exists
        /// </summary>
        Task<bool> FileExistsAsync(string fileName);

        /// <summary>
        /// Upload an image to cloud storage (returns URL & public ID)
        /// </summary>
        Task<CloudinaryUploadResult> UploadImageAsync(Stream imageStream, string fileName, string? folder = null);

        /// <summary>
        /// Upload a profile image using the configured profile folder
        /// </summary>
        Task<CloudinaryUploadResult> UploadProfileImageAsync(Stream imageStream, string fileName);

        /// <summary>
        /// Delete an image asset using its public ID
        /// </summary>
        Task<bool> DeleteImageAsync(string publicId);
    }
}

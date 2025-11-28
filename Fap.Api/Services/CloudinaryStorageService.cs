using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Fap.Api.Interfaces;
using Fap.Domain.DTOs.Common;
using Fap.Domain.Settings;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace Fap.Api.Services
{
    public class CloudinaryStorageService : ICloudStorageService
    {
        private readonly Cloudinary _cloudinary;
        private readonly CloudinarySettings _settings;
        private readonly ILogger<CloudinaryStorageService> _logger;

        public CloudinaryStorageService(
            IOptions<CloudinarySettings> settings,
            ILogger<CloudinaryStorageService> logger)
        {
            _settings = settings.Value;
            _logger = logger;

            // Initialize Cloudinary
            var account = new Account(
                _settings.CloudName,
                _settings.ApiKey,
                _settings.ApiSecret
            );

            _cloudinary = new Cloudinary(account);
            _cloudinary.Api.Secure = true; // Use HTTPS

            _logger.LogInformation("Cloudinary initialized: {CloudName}", _settings.CloudName);
        }

    public async Task<string> UploadPdfAsync(byte[] pdfBytes, string fileName)
        {
            try
            {
                using var stream = new MemoryStream(pdfBytes);

                var uploadParams = new RawUploadParams
                {
                    File = new FileDescription(fileName, stream),
                    PublicId = $"{_settings.CertificatesFolder}/{Path.GetFileNameWithoutExtension(fileName)}",
                    Folder = _settings.CertificatesFolder,
                    // ResourceType is implicitly Raw for RawUploadParams
                    Overwrite = true,
                    UseFilename = true,
                    UniqueFilename = false
                };

                var uploadResult = await _cloudinary.UploadAsync(uploadParams);

                if (uploadResult.Error != null)
                {
                    _logger.LogError("Cloudinary upload error: {Error}", uploadResult.Error.Message);
                    throw new Exception($"Upload failed: {uploadResult.Error.Message}");
                }

                var url = uploadResult.SecureUrl.ToString();
                _logger.LogInformation("PDF uploaded to Cloudinary: {Url}", url);

                return url;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error uploading PDF to Cloudinary: {FileName}", fileName);
                throw;
            }
        }

        public async Task<bool> DeletePdfAsync(string fileName)
        {
            try
            {
                var publicId = $"{_settings.CertificatesFolder}/{Path.GetFileNameWithoutExtension(fileName)}";

                var deleteParams = new DeletionParams(publicId)
                {
                    ResourceType = ResourceType.Raw
                };

                var result = await _cloudinary.DestroyAsync(deleteParams);

                if (result.Result == "ok" || result.Result == "not found")
                {
                    _logger.LogInformation("PDF deleted from Cloudinary: {FileName}", fileName);
                    return true;
                }

                _logger.LogWarning("Failed to delete PDF from Cloudinary: {Result}", result.Result);
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting PDF from Cloudinary: {FileName}", fileName);
                return false;
            }
        }

    public async Task<byte[]?> DownloadPdfAsync(string url)
        {
            try
            {
                using var httpClient = new HttpClient();
                var bytes = await httpClient.GetByteArrayAsync(url);

                _logger.LogInformation("PDF downloaded from Cloudinary: {Url}", url);
                return bytes;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error downloading PDF from Cloudinary: {Url}", url);
                return null;
            }
        }

        public async Task<bool> FileExistsAsync(string fileName)
        {
            try
            {
                var publicId = $"{_settings.CertificatesFolder}/{Path.GetFileNameWithoutExtension(fileName)}";

                var getParams = new GetResourceParams(publicId)
                {
                    ResourceType = ResourceType.Raw
                };

                var result = await _cloudinary.GetResourceAsync(getParams);

                return result != null && result.Error == null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error checking file existence: {FileName}", fileName);
                return false;
            }
        }

        public async Task<CloudinaryUploadResult> UploadImageAsync(Stream imageStream, string fileName, string? folder = null)
        {
            try
            {
                var targetFolder = string.IsNullOrWhiteSpace(folder)
                    ? _settings.CertificatesFolder
                    : folder!;

                using var memoryStream = new MemoryStream();
                await imageStream.CopyToAsync(memoryStream);
                memoryStream.Position = 0;

                var publicId = BuildPublicId(targetFolder, fileName);

                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(fileName, memoryStream),
                    PublicId = publicId,
                    Folder = targetFolder,
                    Overwrite = true,
                    UseFilename = false,
                    UniqueFilename = false,
                    Transformation = new Transformation()
                        .Quality("auto")
                        .FetchFormat("auto")
                };

                var uploadResult = await _cloudinary.UploadAsync(uploadParams);

                if (uploadResult.Error != null)
                {
                    _logger.LogError("Cloudinary image upload error: {Error}", uploadResult.Error.Message);
                    throw new Exception($"Image upload failed: {uploadResult.Error.Message}");
                }

                var result = new CloudinaryUploadResult
                {
                    Url = uploadResult.SecureUrl?.ToString() ?? string.Empty,
                    PublicId = uploadResult.PublicId,
                    Width = uploadResult.Width,
                    Height = uploadResult.Height,
                    Format = uploadResult.Format
                };

                _logger.LogInformation("Image uploaded to Cloudinary: {PublicId}", uploadResult.PublicId);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error uploading image to Cloudinary: {FileName}", fileName);
                throw;
            }
        }

        public Task<CloudinaryUploadResult> UploadProfileImageAsync(Stream imageStream, string fileName)
            => UploadImageAsync(imageStream, fileName, _settings.ProfileImagesFolder);

        public async Task<bool> DeleteImageAsync(string publicId)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(publicId))
                {
                    return true;
                }

                var deleteParams = new DeletionParams(publicId)
                {
                    ResourceType = ResourceType.Image
                };

                var result = await _cloudinary.DestroyAsync(deleteParams);

                var success = result.Result == "ok" || result.Result == "not found";
                if (success)
                {
                    _logger.LogInformation("Image deleted from Cloudinary: {PublicId}", publicId);
                }
                else
                {
                    _logger.LogWarning("Failed to delete image {PublicId}: {Result}", publicId, result.Result);
                }

                return success;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting image from Cloudinary: {PublicId}", publicId);
                return false;
            }
        }

        private static string BuildPublicId(string folder, string fileName)
        {
            var safeFileName = Path.GetFileNameWithoutExtension(fileName);
            var uniqueId = Guid.NewGuid().ToString("N");
            return $"{folder}/{uniqueId}_{safeFileName}";
        }
    }
}

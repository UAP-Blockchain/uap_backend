using Fap.Api.Interfaces;
using Fap.Domain.DTOs.Credential;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fap.Api.Controllers
{
    /// <summary>
    /// Certificate Templates Controller
    /// </summary>
    [ApiController]
    [Route("api/certificate-templates")]
    public class CertificateTemplatesController : ControllerBase
    {
        private readonly ICredentialService _credentialService;
        private readonly ILogger<CertificateTemplatesController> _logger;

        public CertificateTemplatesController(
        ICredentialService credentialService,
          ILogger<CertificateTemplatesController> logger)
        {
            _credentialService = credentialService;
            _logger = logger;
        }

        /// <summary>
        /// GET /api/certificate-templates - Get all templates
        /// </summary>
        [HttpGet]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(List<CertificateTemplateDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<List<CertificateTemplateDto>>> GetTemplates(
                [FromQuery] string? templateType = null,
          [FromQuery] bool includeInactive = false)
        {
            try
            {
                var templates = await _credentialService.GetTemplatesAsync(templateType, includeInactive);
                return Ok(templates);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting certificate templates");
                return StatusCode(500, new ProblemDetails { Status = 500, Title = "Internal Server Error" });
            }
        }

        /// <summary>
        /// GET /api/certificate-templates/samples - Get sample templates (Public)
        /// </summary>
        [HttpGet("samples")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(List<CertificateTemplateDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<List<CertificateTemplateDto>>> GetSampleTemplates(
            [FromQuery] string? templateType = null)
        {
            try
            {
                var templates = await _credentialService.GetSampleTemplatesAsync(templateType);
                return Ok(templates);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting sample templates");
                return StatusCode(500, new ProblemDetails { Status = 500, Title = "Internal Server Error" });
            }
        }

        /// <summary>
        /// GET /api/certificate-templates/{id} - Get template by ID
        /// </summary>
        [HttpGet("{id:guid}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(CertificateTemplateDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CertificateTemplateDto>> GetTemplateById(Guid id)
        {
            try
            {
                var template = await _credentialService.GetTemplateByIdAsync(id);

                if (template == null)
                {
                    return NotFound(new ProblemDetails { Status = 404, Title = "Not Found" });
                }

                return Ok(template);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting template {TemplateId}", id);
                return StatusCode(500, new ProblemDetails { Status = 500, Title = "Internal Server Error" });
            }
        }

        /// <summary>
        /// POST /api/certificate-templates - Create new template (Admin)
        /// </summary>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(CertificateTemplateDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CertificateTemplateDto>> CreateTemplate(
                [FromBody] CreateCertificateTemplateRequest request)
        {
            try
            {
                var template = await _credentialService.CreateTemplateAsync(request);

                return CreatedAtAction(
               nameof(GetTemplateById),
               new { id = template.Id },
             template
       );
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new ProblemDetails
                {
                    Status = 400,
                    Title = "Bad Request",
                    Detail = ex.Message
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating template");
                return StatusCode(500, new ProblemDetails { Status = 500, Title = "Internal Server Error" });
            }
        }

        /// <summary>
        /// PUT /api/certificate-templates/{id} - Update template (Admin)
        /// </summary>
        [HttpPut("{id:guid}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(CertificateTemplateDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CertificateTemplateDto>> UpdateTemplate(
            Guid id,
    [FromBody] UpdateCertificateTemplateRequest request)
        {
            try
            {
                var template = await _credentialService.UpdateTemplateAsync(id, request);
                return Ok(template);
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new ProblemDetails { Status = 404, Title = "Not Found" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating template {TemplateId}", id);
                return StatusCode(500, new ProblemDetails { Status = 500, Title = "Internal Server Error" });
            }
        }

        /// <summary>
        /// DELETE /api/certificate-templates/{id} - Delete template (Admin)
        /// </summary>
        [HttpDelete("{id:guid}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteTemplate(Guid id)
        {
            try
            {
                await _credentialService.DeleteTemplateAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new ProblemDetails { Status = 404, Title = "Not Found" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting template {TemplateId}", id);
                return StatusCode(500, new ProblemDetails { Status = 500, Title = "Internal Server Error" });
            }
        }

        /// <summary>
        /// GET /api/certificate-templates/{id}/preview - Preview template with sample data
        /// </summary>
        [HttpGet("{id:guid}/preview")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(FileContentResult), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PreviewTemplate(Guid id)
        {
            try
            {
                var (fileBytes, fileName) = await _credentialService.PreviewTemplateAsync(id);
                return File(fileBytes, "application/pdf", fileName);
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new ProblemDetails { Status = 404, Title = "Not Found" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error previewing template {TemplateId}", id);
                return StatusCode(500, new ProblemDetails { Status = 500, Title = "Internal Server Error" });
            }
        }
    }
}

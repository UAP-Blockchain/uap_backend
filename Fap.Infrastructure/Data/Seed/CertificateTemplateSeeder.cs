using Fap.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Fap.Infrastructure.Data.Seed
{
    /// <summary>
    /// Seeds CertificateTemplates for different types of credentials
    /// </summary>
    public class CertificateTemplateSeeder : BaseSeeder
    {
        // Fixed GUIDs for certificate templates
        public static readonly Guid CompletionCertificateId = Guid.Parse("60000000-0000-0000-0000-000000000001");
        public static readonly Guid ExcellenceCertificateId = Guid.Parse("60000000-0000-0000-0000-000000000002");
        public static readonly Guid DiplomaId = Guid.Parse("60000000-0000-0000-0000-000000000003");
        public static readonly Guid TranscriptId = Guid.Parse("60000000-0000-0000-0000-000000000004");
        public static readonly Guid ParticipationId = Guid.Parse("60000000-0000-0000-0000-000000000005");

        public CertificateTemplateSeeder(FapDbContext context) : base(context) { }

        public override async Task SeedAsync()
        {
            if (await _context.CertificateTemplates.AnyAsync())
            {
                Console.WriteLine("⏭️ Certificate Templates already exist. Skipping...");
                return;
            }

            var templates = new List<CertificateTemplate>
            {
                new CertificateTemplate
                {
                    Id = CompletionCertificateId,
                    Name = "Course Completion Certificate",
                    Description = "Awarded to students who successfully complete a course with passing grade",
                    TemplateFileUrl = "ipfs://QmTemplateCompletionCert123/template.pdf"
                },
                new CertificateTemplate
                {
                    Id = ExcellenceCertificateId,
                    Name = "Certificate of Excellence",
                    Description = "Awarded to students with outstanding academic performance (GPA >= 8.5)",
                    TemplateFileUrl = "ipfs://QmTemplateExcellenceCert456/template.pdf"
                },
                new CertificateTemplate
                {
                    Id = DiplomaId,
                    Name = "Graduation Diploma",
                    Description = "Official diploma awarded upon successful completion of degree program",
                    TemplateFileUrl = "ipfs://QmTemplateDiploma789/template.pdf"
                },
                new CertificateTemplate
                {
                    Id = TranscriptId,
                    Name = "Academic Transcript",
                    Description = "Official record of all courses and grades",
                    TemplateFileUrl = "ipfs://QmTemplateTranscript012/template.pdf"
                },
                new CertificateTemplate
                {
                    Id = ParticipationId,
                    Name = "Participation Certificate",
                    Description = "Awarded for active participation in courses or events",
                    TemplateFileUrl = "ipfs://QmTemplateParticipation345/template.pdf"
                }
            };

            await _context.CertificateTemplates.AddRangeAsync(templates);
            await SaveAsync("Certificate Templates");

            Console.WriteLine($"   ✅ Created {templates.Count} certificate templates");
        }
    }
}

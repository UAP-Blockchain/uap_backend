using Fap.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Fap.Infrastructure.Data.Seed
{
    /// <summary>
    /// Seeds Credentials (blockchain certificates) with various test scenarios
    /// </summary>
    public class CredentialSeeder : BaseSeeder
    {
        public CredentialSeeder(FapDbContext context) : base(context) { }

        public override async Task SeedAsync()
        {
            if (await _context.Credentials.AnyAsync())
            {
                Console.WriteLine("⏭️  Credentials already exist. Skipping...");
                return;
            }

            var credentials = new List<Credential>();

            // Get all students
            var students = await _context.Students.ToListAsync();

            // Get certificate templates
            var completionTemplate = await _context.CertificateTemplates
          .FirstOrDefaultAsync(ct => ct.Id == CertificateTemplateSeeder.CompletionCertificateId);
            var excellenceTemplate = await _context.CertificateTemplates
           .FirstOrDefaultAsync(ct => ct.Id == CertificateTemplateSeeder.ExcellenceCertificateId);

            if (completionTemplate == null || excellenceTemplate == null)
            {
                Console.WriteLine("⚠️  Certificate templates not found. Skipping credentials...");
                return;
            }

            var random = new Random(99999);

            // Create credentials for some students
            var studentsToCredential = students.Take(4).ToList(); // First 4 students

            int credentialCounter = 1;

            foreach (var student in studentsToCredential)
            {
                // Completion certificate (on blockchain)
                var completionCred = new Credential
                {
                    Id = Guid.NewGuid(),
                    CredentialId = $"FAP-CERT-{DateTime.UtcNow.Year}-{credentialCounter:D6}",
                    IPFSHash = $"QmCredential{credentialCounter}Hash{Guid.NewGuid().ToString("N")[..16]}",
                    FileUrl = $"https://ipfs.io/ipfs/QmCredential{credentialCounter}",
                    IssuedDate = DateTime.UtcNow.AddDays(-random.Next(30, 180)),
                    IsRevoked = false,
                    StudentId = student.Id,
                    CertificateTemplateId = completionTemplate.Id,
                    BlockchainTransactionHash = $"0x{Guid.NewGuid().ToString("N")[..64]}",
                    BlockchainStoredAt = DateTime.UtcNow.AddDays(-random.Next(30, 180)),
                    IsOnBlockchain = true
                };

                credentials.Add(completionCred);
                credentialCounter++;

                // Excellence certificate for high-performing students (some on blockchain, some pending)
                if (random.Next(100) < 50) // 50% get excellence certificate
                {
                    var excellenceCred = new Credential
                    {
                        Id = Guid.NewGuid(),
                        CredentialId = $"FAP-EXCEL-{DateTime.UtcNow.Year}-{credentialCounter:D6}",
                        IPFSHash = random.Next(100) < 70 ? $"QmExcel{credentialCounter}Hash{Guid.NewGuid().ToString("N")[..16]}" : null,
                        FileUrl = random.Next(100) < 70 ? $"https://ipfs.io/ipfs/QmExcel{credentialCounter}" : null,
                        IssuedDate = DateTime.UtcNow.AddDays(-random.Next(10, 90)),
                        IsRevoked = false,
                        StudentId = student.Id,
                        CertificateTemplateId = excellenceTemplate.Id,
                        BlockchainTransactionHash = random.Next(100) < 70 ? $"0x{Guid.NewGuid().ToString("N")[..64]}" : null,
                        BlockchainStoredAt = random.Next(100) < 70 ? DateTime.UtcNow.AddDays(-random.Next(10, 90)) : null,
                        IsOnBlockchain = random.Next(100) < 70 // 70% on blockchain, 30% pending
                    };

                    credentials.Add(excellenceCred);
                    credentialCounter++;
                }
            }

            // Add a revoked credential for testing
            if (studentsToCredential.Any())
            {
                var revokedCred = new Credential
                {
                    Id = Guid.NewGuid(),
                    CredentialId = $"FAP-REVOKED-{DateTime.UtcNow.Year}-{credentialCounter:D6}",
                    IPFSHash = $"QmRevokedHash{Guid.NewGuid().ToString("N")[..16]}",
                    FileUrl = $"https://ipfs.io/ipfs/QmRevoked{credentialCounter}",
                    IssuedDate = DateTime.UtcNow.AddDays(-200),
                    IsRevoked = true, // ⚠️ This credential is revoked
                    StudentId = studentsToCredential.First().Id,
                    CertificateTemplateId = completionTemplate.Id,
                    BlockchainTransactionHash = $"0x{Guid.NewGuid().ToString("N")[..64]}",
                    BlockchainStoredAt = DateTime.UtcNow.AddDays(-200),
                    IsOnBlockchain = true
                };

                credentials.Add(revokedCred);
            }

            await _context.Credentials.AddRangeAsync(credentials);
            await SaveAsync("Credentials");

            Console.WriteLine($"   ✅ Created {credentials.Count} credentials:");
            Console.WriteLine($"      • On Blockchain: {credentials.Count(c => c.IsOnBlockchain)}");
            Console.WriteLine($"      • Pending Blockchain: {credentials.Count(c => !c.IsOnBlockchain)}");
            Console.WriteLine($"      • Revoked: {credentials.Count(c => c.IsRevoked)}");
            Console.WriteLine($"   • Active: {credentials.Count(c => !c.IsRevoked)}");
        }
    }
}

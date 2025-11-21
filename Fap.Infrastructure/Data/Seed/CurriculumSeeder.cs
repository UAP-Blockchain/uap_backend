using System.Collections.Generic;
using Fap.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Fap.Infrastructure.Data.Seed
{
    /// <summary>
    /// Seeds academic curriculums that tie students to structured study plans.
    /// </summary>
    public class CurriculumSeeder : BaseSeeder
    {
        public static int SoftwareEngineering2024Id = 1;
        public static int DataScience2024Id = 2;

        public CurriculumSeeder(FapDbContext context) : base(context) { }

        public override async Task SeedAsync()
        {
            if (await _context.Curriculums.AnyAsync())
            {
                Console.WriteLine("Curriculums already exist. Skipping...");
                return;
            }

            var curriculums = new List<Curriculum>
            {
                new Curriculum
                {
                    Code = "SE-2024",
                    Name = "Software Engineering 2024",
                    Description = "Four-year curriculum focused on software development, quality, and architecture.",
                    TotalCredits = 120
                },
                new Curriculum
                {
                    Code = "DS-2024",
                    Name = "Data Science 2024",
                    Description = "Applied data science curriculum with analytics, programming, and database foundations.",
                    TotalCredits = 118
                }
            };

            await _context.Curriculums.AddRangeAsync(curriculums);
            await SaveAsync("Curriculums");
            
            // Store generated IDs for reference by other seeders
            var seCurriculum = await _context.Curriculums.FirstOrDefaultAsync(c => c.Code == "SE-2024");
            var dsCurriculum = await _context.Curriculums.FirstOrDefaultAsync(c => c.Code == "DS-2024");
            
            if (seCurriculum != null) SoftwareEngineering2024Id = seCurriculum.Id;
            if (dsCurriculum != null) DataScience2024Id = dsCurriculum.Id;
        }
    }
}

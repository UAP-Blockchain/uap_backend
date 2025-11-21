using System.Collections.Generic;
using Fap.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Fap.Infrastructure.Data.Seed
{
    /// <summary>
    /// Seeds the subject mappings for each curriculum with prerequisite relationships.
    /// </summary>
    public class CurriculumSubjectSeeder : BaseSeeder
    {
        public CurriculumSubjectSeeder(FapDbContext context) : base(context) { }

        public override async Task SeedAsync()
        {
            if (await _context.CurriculumSubjects.AnyAsync())
            {
                Console.WriteLine("Curriculum subjects already exist. Skipping...");
                return;
            }

            var hasCurriculums = await _context.Curriculums.AnyAsync();
            var hasSubjects = await _context.Subjects.AnyAsync();

            if (!hasCurriculums || !hasSubjects)
            {
                Console.WriteLine("Missing curriculum or subject data. Curriculum subject seeding skipped.");
                return;
            }

            var items = new List<CurriculumSubject>
            {
                // Software Engineering 2024
                new CurriculumSubject
                {
                    CurriculumId = CurriculumSeeder.SoftwareEngineering2024Id,
                    SubjectId = SubjectOfferingSeeder.CS101Id,
                    SemesterNumber = 1
                },
                new CurriculumSubject
                {
                    CurriculumId = CurriculumSeeder.SoftwareEngineering2024Id,
                    SubjectId = SubjectOfferingSeeder.MATH101Id,
                    SemesterNumber = 1
                },
                new CurriculumSubject
                {
                    CurriculumId = CurriculumSeeder.SoftwareEngineering2024Id,
                    SubjectId = SubjectOfferingSeeder.SE101Id,
                    SemesterNumber = 2,
                    PrerequisiteSubjectId = SubjectOfferingSeeder.CS101Id
                },
                new CurriculumSubject
                {
                    CurriculumId = CurriculumSeeder.SoftwareEngineering2024Id,
                    SubjectId = SubjectOfferingSeeder.MATH201Id,
                    SemesterNumber = 2,
                    PrerequisiteSubjectId = SubjectOfferingSeeder.MATH101Id
                },
                new CurriculumSubject
                {
                    CurriculumId = CurriculumSeeder.SoftwareEngineering2024Id,
                    SubjectId = SubjectOfferingSeeder.SE102Id,
                    SemesterNumber = 3,
                    PrerequisiteSubjectId = SubjectOfferingSeeder.SE101Id
                },
                new CurriculumSubject
                {
                    CurriculumId = CurriculumSeeder.SoftwareEngineering2024Id,
                    SubjectId = SubjectOfferingSeeder.CS201Id,
                    SemesterNumber = 3,
                    PrerequisiteSubjectId = SubjectOfferingSeeder.CS101Id
                },
                new CurriculumSubject
                {
                    CurriculumId = CurriculumSeeder.SoftwareEngineering2024Id,
                    SubjectId = SubjectOfferingSeeder.DB201Id,
                    SemesterNumber = 4,
                    PrerequisiteSubjectId = SubjectOfferingSeeder.CS101Id
                },
                new CurriculumSubject
                {
                    CurriculumId = CurriculumSeeder.SoftwareEngineering2024Id,
                    SubjectId = SubjectOfferingSeeder.WEB301Id,
                    SemesterNumber = 4,
                    PrerequisiteSubjectId = SubjectOfferingSeeder.DB201Id
                },

                // Data Science 2024
                new CurriculumSubject
                {
                    CurriculumId = CurriculumSeeder.DataScience2024Id,
                    SubjectId = SubjectOfferingSeeder.CS101Id,
                    SemesterNumber = 1
                },
                new CurriculumSubject
                {
                    CurriculumId = CurriculumSeeder.DataScience2024Id,
                    SubjectId = SubjectOfferingSeeder.MATH101Id,
                    SemesterNumber = 1
                },
                new CurriculumSubject
                {
                    CurriculumId = CurriculumSeeder.DataScience2024Id,
                    SubjectId = SubjectOfferingSeeder.DB201Id,
                    SemesterNumber = 2,
                    PrerequisiteSubjectId = SubjectOfferingSeeder.CS101Id
                },
                new CurriculumSubject
                {
                    CurriculumId = CurriculumSeeder.DataScience2024Id,
                    SubjectId = SubjectOfferingSeeder.CS201Id,
                    SemesterNumber = 2,
                    PrerequisiteSubjectId = SubjectOfferingSeeder.CS101Id
                },
                new CurriculumSubject
                {
                    CurriculumId = CurriculumSeeder.DataScience2024Id,
                    SubjectId = SubjectOfferingSeeder.MATH201Id,
                    SemesterNumber = 3,
                    PrerequisiteSubjectId = SubjectOfferingSeeder.MATH101Id
                },
                new CurriculumSubject
                {
                    CurriculumId = CurriculumSeeder.DataScience2024Id,
                    SubjectId = SubjectOfferingSeeder.SE101Id,
                    SemesterNumber = 3,
                    PrerequisiteSubjectId = SubjectOfferingSeeder.CS101Id
                },
                new CurriculumSubject
                {
                    CurriculumId = CurriculumSeeder.DataScience2024Id,
                    SubjectId = SubjectOfferingSeeder.SE102Id,
                    SemesterNumber = 4,
                    PrerequisiteSubjectId = SubjectOfferingSeeder.SE101Id
                },
                new CurriculumSubject
                {
                    CurriculumId = CurriculumSeeder.DataScience2024Id,
                    SubjectId = SubjectOfferingSeeder.WEB301Id,
                    SemesterNumber = 4,
                    PrerequisiteSubjectId = SubjectOfferingSeeder.DB201Id
                }
            };

            await _context.CurriculumSubjects.AddRangeAsync(items);
            await SaveAsync("Curriculum Subjects");
        }
    }
}

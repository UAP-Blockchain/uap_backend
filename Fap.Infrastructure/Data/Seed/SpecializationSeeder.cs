using Fap.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Fap.Infrastructure.Data.Seed
{
    public class SpecializationSeeder : BaseSeeder
    {
        public SpecializationSeeder(FapDbContext context) : base(context) { }

        public override async Task SeedAsync()
        {
            if (await _context.Specializations.AnyAsync())
            {
                Console.WriteLine("⏭️ Specializations already exist. Skipping...");
                return;
            }

            var specializations = new List<Specialization>
            {
                new Specialization { Id = Guid.NewGuid(), Code = "SE", Name = "Software Engineering", Description = "Focus on software development lifecycle" },
                new Specialization { Id = Guid.NewGuid(), Code = "AI", Name = "Artificial Intelligence", Description = "Focus on machine learning and AI" },
                new Specialization { Id = Guid.NewGuid(), Code = "IS", Name = "Information Systems", Description = "Focus on business information systems" },
                new Specialization { Id = Guid.NewGuid(), Code = "IA", Name = "Information Assurance", Description = "Focus on cybersecurity" },
                new Specialization { Id = Guid.NewGuid(), Code = "GD", Name = "Graphic Design", Description = "Focus on digital design and media" }
            };

            await _context.Specializations.AddRangeAsync(specializations);
            await SaveAsync("Specializations");

            // Seed SubjectSpecializations
            var subjects = await _context.Subjects.ToListAsync();
            var subjectSpecs = new List<SubjectSpecialization>();
            var random = new Random();

            foreach (var subject in subjects)
            {
                // Assign 1-2 random specializations to each subject
                var specs = specializations.OrderBy(x => random.Next()).Take(random.Next(1, 3)).ToList();
                foreach (var spec in specs)
                {
                    subjectSpecs.Add(new SubjectSpecialization
                    {
                        SubjectId = subject.Id,
                        SpecializationId = spec.Id,
                        IsRequired = true
                    });
                }
            }
            await _context.SubjectSpecializations.AddRangeAsync(subjectSpecs);
            await SaveAsync("Subject Specializations");

            // Seed TeacherSpecializations
            var teachers = await _context.Teachers.ToListAsync();
            var teacherSpecs = new List<TeacherSpecialization>();

            foreach (var teacher in teachers)
            {
                // Assign 1-2 random specializations to each teacher
                var specs = specializations.OrderBy(x => random.Next()).Take(random.Next(1, 3)).ToList();
                bool isPrimary = true;
                foreach (var spec in specs)
                {
                    teacherSpecs.Add(new TeacherSpecialization
                    {
                        TeacherId = teacher.Id,
                        SpecializationId = spec.Id,
                        IsPrimary = isPrimary,
                        AssignedAt = DateTime.UtcNow
                    });
                    isPrimary = false; // Only first one is primary
                }
            }
            await _context.TeacherSpecializations.AddRangeAsync(teacherSpecs);
            await SaveAsync("Teacher Specializations");
        }
    }
}

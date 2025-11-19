using Fap.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Fap.Infrastructure.Data.Seed
{
    /// <summary>
    /// Seeds GradeComponents (Quiz, Lab, Midterm, Final, etc.)
    /// </summary>
    public class GradeComponentSeeder : BaseSeeder
    {
        // Fixed GUIDs for grade components
        public static readonly Guid QuizId = Guid.Parse("50000000-0000-0000-0000-000000000001");
        public static readonly Guid LabId = Guid.Parse("50000000-0000-0000-0000-000000000002");
        public static readonly Guid AssignmentId = Guid.Parse("50000000-0000-0000-0000-000000000003");
        public static readonly Guid MidtermId = Guid.Parse("50000000-0000-0000-0000-000000000004");
        public static readonly Guid FinalId = Guid.Parse("50000000-0000-0000-0000-000000000005");
        public static readonly Guid ProjectId = Guid.Parse("50000000-0000-0000-0000-000000000006");
        public static readonly Guid PresentationId = Guid.Parse("50000000-0000-0000-0000-000000000007");
        public static readonly Guid AttendanceId = Guid.Parse("50000000-0000-0000-0000-000000000008");

        public GradeComponentSeeder(FapDbContext context) : base(context) { }

        public override async Task SeedAsync()
        {
            if (await _context.GradeComponents.AnyAsync())
            {
                Console.WriteLine("⏭️ Grade Components already exist. Skipping...");
                return;
            }

            var components = new List<GradeComponent>
            {
                new GradeComponent { Id = QuizId, Name = "Quiz", WeightPercent = 10 },
                new GradeComponent { Id = LabId, Name = "Lab Work", WeightPercent = 15 },
                new GradeComponent { Id = AssignmentId, Name = "Assignment", WeightPercent = 15 },
                new GradeComponent { Id = MidtermId, Name = "Midterm Exam", WeightPercent = 20 },
                new GradeComponent { Id = FinalId, Name = "Final Exam", WeightPercent = 30 },
                new GradeComponent { Id = ProjectId, Name = "Project", WeightPercent = 20 },
                new GradeComponent { Id = PresentationId, Name = "Presentation", WeightPercent = 10 },
                new GradeComponent { Id = AttendanceId, Name = "Attendance & Participation", WeightPercent = 10 }
            };

            await _context.GradeComponents.AddRangeAsync(components);
            await SaveAsync("Grade Components");

            Console.WriteLine($"   ✅ Created {components.Count} grade components");
        }
    }
}

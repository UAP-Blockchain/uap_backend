using Fap.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Fap.Infrastructure.Data.Seed
{
    /// <summary>
    /// Seeds StudentRoadmaps - academic progress tracking for students
    /// </summary>
    public class StudentRoadmapSeeder : BaseSeeder
    {
        public StudentRoadmapSeeder(FapDbContext context) : base(context) { }

        public override async Task SeedAsync()
        {
            if (await _context.StudentRoadmaps.AnyAsync())
            {
                Console.WriteLine("??  Student Roadmaps already exist. Skipping...");
                return;
            }

            var roadmaps = new List<StudentRoadmap>();

            // Get all students
            var students = await _context.Students.Take(6).ToListAsync();

            // Get all subjects
            var subjects = await _context.Subjects.ToListAsync();

            // Get all semesters
            var semesters = await _context.Semesters.OrderBy(s => s.StartDate).ToListAsync();

            if (!students.Any() || !subjects.Any() || !semesters.Any())
            {
                Console.WriteLine("??  Missing required data for roadmaps. Skipping...");
                return;
            }

            var random = new Random(77777);

            foreach (var student in students)
            {
                // Create a roadmap with varied progress
                var subjectsForStudent = subjects.OrderBy(s => s.SubjectCode).ToList();
                int sequenceOrder = 1;

                foreach (var subject in subjectsForStudent)
                {
                    // Determine semester for this subject
                    var semesterIndex = (sequenceOrder - 1) / 3; // 3 subjects per semester
                    if (semesterIndex >= semesters.Count) break;

                    var semester = semesters[semesterIndex];

                    // Determine status based on semester timing
                    var status = GetRoadmapStatus(semester, random);

                    var roadmap = new StudentRoadmap
                    {
                        Id = Guid.NewGuid(),
                        StudentId = student.Id,
                        SubjectId = subject.Id,
                        Status = status,
                        SemesterId = semester.Id,
                        SequenceOrder = sequenceOrder,
                        FinalScore = status == "Completed" ? GenerateFinalScore(random) : null,
                        LetterGrade = status == "Completed" ? null : null,
                        StartedAt = status != "Planned" ? semester.StartDate : null,
                        CompletedAt = status == "Completed" ? semester.EndDate.AddDays(-random.Next(0, 7)) : null,
                        Notes = GetRoadmapNotes(status, subject.SubjectName),
                        CreatedAt = DateTime.UtcNow.AddDays(-random.Next(30, 365)),
                        UpdatedAt = DateTime.UtcNow.AddDays(-random.Next(1, 30))
                    };

                    // Set letter grade if completed
                    if (roadmap.FinalScore.HasValue)
                    {
                        roadmap.LetterGrade = ConvertToLetterGrade(roadmap.FinalScore.Value);
                    }

                    roadmaps.Add(roadmap);
                    sequenceOrder++;
                }
            }

            await _context.StudentRoadmaps.AddRangeAsync(roadmaps);
            await SaveAsync("Student Roadmaps");

            Console.WriteLine($"   ? Created {roadmaps.Count} student roadmap entries:");
            Console.WriteLine($"    • Completed: {roadmaps.Count(r => r.Status == "Completed")}");
            Console.WriteLine($"      • In Progress: {roadmaps.Count(r => r.Status == "InProgress")}");
            Console.WriteLine($"      • Planned: {roadmaps.Count(r => r.Status == "Planned")}");
            Console.WriteLine($"      • Failed: {roadmaps.Count(r => r.Status == "Failed")}");
        }

        private string GetRoadmapStatus(Semester semester, Random random)
        {
            var now = DateTime.UtcNow;

            // If semester hasn't started yet
            if (semester.StartDate > now)
            {
                return "Planned";
            }
            // If semester has ended
            else if (semester.EndDate < now)
            {
                // 95% completed, 5% failed
                return random.Next(100) < 95 ? "Completed" : "Failed";
            }
            // If semester is ongoing
            else
            {
                return "InProgress";
            }
        }

        private decimal GenerateFinalScore(Random random)
        {
            var roll = random.Next(100);

            // Score distribution
            if (roll < 20) // 20% excellent (8.5-10)
            {
                return Math.Round((decimal)(8.5 + random.NextDouble() * 1.5), 2);
            }
            else if (roll < 50) // 30% good (7-8.5)
            {
                return Math.Round((decimal)(7.0 + random.NextDouble() * 1.5), 2);
            }
            else if (roll < 80) // 30% average (5.5-7)
            {
                return Math.Round((decimal)(5.5 + random.NextDouble() * 1.5), 2);
            }
            else // 20% below average (3-5.5)
            {
                return Math.Round((decimal)(3.0 + random.NextDouble() * 2.5), 2);
            }
        }

        private string ConvertToLetterGrade(decimal score)
        {
            if (score >= 9.0m) return "A+";
            else if (score >= 8.5m) return "A";
            else if (score >= 8.0m) return "B+";
            else if (score >= 7.0m) return "B";
            else if (score >= 6.5m) return "C+";
            else if (score >= 5.5m) return "C";
            else if (score >= 5.0m) return "D+";
            else if (score >= 4.0m) return "D";
            else return "F";
        }

        private string? GetRoadmapNotes(string status, string subjectName)
        {
            return status switch
            {
                "Completed" => $"Successfully completed {subjectName}",
                "InProgress" => $"Currently enrolled in {subjectName}",
                "Failed" => "Need to retake this course",
                "Planned" => $"Planning to take {subjectName} next semester",
                _ => null
            };
        }
    }
}

using Microsoft.EntityFrameworkCore;

namespace Fap.Infrastructure.Data.Seed
{
    /// <summary>
    /// Master data seeder - orchestrates all individual seeders
    /// ✅ Refactored into multiple small files for maintainability
    /// ✅ Updated to use SubjectOffering pattern for multi-semester support
    /// ✅ ENHANCED: Added all missing seeders for complete test coverage
    /// ✅ COMPLETE: All 23 database tables now have seed data
    /// </summary>
    public static class DataSeeder
    {
        public static async Task SeedAsync(FapDbContext context)
        {
            // Quick check - if TimeSlots exist, assume data is already seeded
            if (await context.TimeSlots.AnyAsync())
            {
                Console.WriteLine("==============================================");
                Console.WriteLine("✅ Seed data already exists. Skipping seeding.");
                Console.WriteLine("==============================================");
                return;
            }

            Console.WriteLine("==============================================");
            Console.WriteLine("🌱 Starting database seeding...");
            Console.WriteLine("==============================================");

            try
            {
                // Order is important! Follow dependency chain:
                // 1. Roles (no dependencies)
                await new RoleSeeder(context).SeedAsync();

                // 2. Permissions (depends on Roles) ✨ NEW
                await new PermissionSeeder(context).SeedAsync();

                // 3. Users (depends on Roles)
                await new UserSeeder(context).SeedAsync();

                // 4. Teachers & Students (depends on Users)
                await new TeacherStudentSeeder(context).SeedAsync();

                // 5. Semesters (no dependencies)
                await new SemesterSeeder(context).SeedAsync();

                // 6. Subjects & SubjectOfferings (depends on Semesters)
                await new SubjectOfferingSeeder(context).SeedAsync();

                // 7. Subject Criteria (depends on Subjects) ✨ NEW
                await new SubjectCriteriaSeeder(context).SeedAsync();

                // 8. TimeSlots (no dependencies)
                await new TimeSlotSeeder(context).SeedAsync();

                // 9. Classes (depends on SubjectOfferings and Teachers)
                await new ClassSeeder(context).SeedAsync();

                // 10. Enrollments & ClassMembers (depends on Classes and Students)
                await new EnrollmentSeeder(context).SeedAsync();

                // 11. Slots (depends on Classes and TimeSlots)
                await new SlotSeeder(context).SeedAsync();

                // 12. Attendance (depends on Slots and Students)
                await new AttendanceSeeder(context).SeedAsync();

                // 13. Grade Components (no dependencies)
                await new GradeComponentSeeder(context).SeedAsync();

                // 14. Grades (depends on Students, Subjects, GradeComponents)
                await new GradeSeeder(context).SeedAsync();

                // 15. Certificate Templates (no dependencies)
                await new CertificateTemplateSeeder(context).SeedAsync();

                // 16. Credentials (depends on Students and CertificateTemplates)
                await new CredentialSeeder(context).SeedAsync();

                // 17. Student Roadmaps (depends on Students, Subjects, Semesters)
                await new StudentRoadmapSeeder(context).SeedAsync();

                // 18. Refresh Tokens (depends on Users) ✨ NEW
                await new RefreshTokenSeeder(context).SeedAsync();

                // 19. OTPs (depends on Users) ✨ NEW
                await new OtpSeeder(context).SeedAsync();

                // 20. Action Logs (depends on Users, Credentials, etc.) ✨ NEW - LAST
                await new ActionLogSeeder(context).SeedAsync();

                // Final save
                await context.SaveChangesAsync();

                Console.WriteLine("==============================================");
                Console.WriteLine("✅ Database seeding completed successfully!");
                Console.WriteLine("==============================================");
                Console.WriteLine();
                Console.WriteLine("📊 SEED DATA SUMMARY:");
                Console.WriteLine($"   • Roles: {await context.Roles.CountAsync()}");
                Console.WriteLine($"   • Permissions: {await context.Permissions.CountAsync()} ✨ NEW!");
                Console.WriteLine($"   • Users: {await context.Users.CountAsync()}");
                Console.WriteLine($"   • Teachers: {await context.Teachers.CountAsync()}");
                Console.WriteLine($"   • Students: {await context.Students.CountAsync()}");
                Console.WriteLine($"   • Semesters: {await context.Semesters.CountAsync()}");
                Console.WriteLine($"   • Subjects (Master): {await context.Subjects.CountAsync()}");
                Console.WriteLine($"   • SubjectOfferings: {await context.SubjectOfferings.CountAsync()} ✨");
                Console.WriteLine($"   • SubjectCriteria: {await context.SubjectCriteria.CountAsync()} ✨ NEW!");
                Console.WriteLine($"   • TimeSlots: {await context.TimeSlots.CountAsync()}");
                Console.WriteLine($"   • Classes: {await context.Classes.CountAsync()}");
                Console.WriteLine($"   • Class Members: {await context.ClassMembers.CountAsync()}");
                Console.WriteLine($"   • Enrollments: {await context.Enrolls.CountAsync()}");
                Console.WriteLine($"   • Slots: {await context.Slots.CountAsync()} ✨");
                Console.WriteLine($"   • Attendances: {await context.Attendances.CountAsync()} ✨");
                Console.WriteLine($"   • Grade Components: {await context.GradeComponents.CountAsync()} ✨");
                Console.WriteLine($"   • Grades: {await context.Grades.CountAsync()} ✨");
                Console.WriteLine($"   • Certificate Templates: {await context.CertificateTemplates.CountAsync()} ✨");
                Console.WriteLine($"   • Credentials: {await context.Credentials.CountAsync()} ✨");
                Console.WriteLine($"   • Student Roadmaps: {await context.StudentRoadmaps.CountAsync()} ✨");
                Console.WriteLine($"   • Refresh Tokens: {await context.RefreshTokens.CountAsync()} ✨ NEW!");
                Console.WriteLine($"   • OTPs: {await context.Otps.CountAsync()} ✨ NEW!");
                Console.WriteLine($"   • Action Logs: {await context.ActionLogs.CountAsync()} ✨ NEW!");
                Console.WriteLine("==============================================");
                Console.WriteLine();
                Console.WriteLine("🎯 KEY ACHIEVEMENTS:");
                Console.WriteLine("   ✅ ALL 23 DATABASE TABLES SEEDED");
                Console.WriteLine("   ✅ Modular seeders - easy to maintain");
                Console.WriteLine("   ✅ SubjectOffering pattern - multi-semester support");
                Console.WriteLine("   ✅ No data duplication - normalized design");
                Console.WriteLine("   ✅ Complete test data for ALL APIs and features");
                Console.WriteLine("   ✅ Realistic scenarios: attendance, grades, credentials, roadmaps");
                Console.WriteLine("   ✅ Authorization: Role-based permissions");
                Console.WriteLine("   ✅ Security: OTP and refresh token flows");
                Console.WriteLine("   ✅ Audit trail: Complete action logging");
                Console.WriteLine("   ✅ Edge cases: revoked credentials, substitute teachers, expired tokens");
                Console.WriteLine("==============================================");
            }
            catch (Exception ex)
            {
                Console.WriteLine("==============================================");
                Console.WriteLine($"❌ ERROR during seeding: {ex.Message}");
                Console.WriteLine($"   Stack trace: {ex.StackTrace}");
                Console.WriteLine("==============================================");
                throw;
            }
        }
    }
}

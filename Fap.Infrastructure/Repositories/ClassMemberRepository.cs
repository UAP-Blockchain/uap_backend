using Fap.Domain.Entities;
using Fap.Domain.Repositories;
using Fap.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Fap.Infrastructure.Repositories
{
    public class ClassMemberRepository : GenericRepository<ClassMember>, IClassMemberRepository
    {
        public ClassMemberRepository(FapDbContext context) : base(context) { }

        public async Task<List<ClassMember>> GetByClassIdAsync(Guid classId)
        {
   return await _dbSet
          .Include(cm => cm.Student)
        .ThenInclude(s => s.User)
    .Where(cm => cm.ClassId == classId)
       .OrderBy(cm => cm.JoinedAt)
         .ToListAsync();
    }

        public async Task<List<ClassMember>> GetByStudentIdAsync(Guid studentId)
        {
            return await _dbSet
      .Include(cm => cm.Class)
        .ThenInclude(c => c.SubjectOffering)
    .ThenInclude(so => so.Subject)
  .Include(cm => cm.Class)
         .ThenInclude(c => c.Teacher)
                 .ThenInclude(t => t.User)
  .Where(cm => cm.StudentId == studentId)
     .OrderBy(cm => cm.JoinedAt)
  .ToListAsync();
        }

        public async Task<bool> IsStudentInClassAsync(Guid classId, Guid studentId)
     {
      return await _dbSet
   .AnyAsync(cm => cm.ClassId == classId && cm.StudentId == studentId);
        }

    public async Task<int> GetClassMemberCountAsync(Guid classId)
        {
  return await _dbSet
        .CountAsync(cm => cm.ClassId == classId);
  }

        public async Task RemoveStudentFromClassAsync(Guid classId, Guid studentId)
 {
    var classMember = await _dbSet
        .FirstOrDefaultAsync(cm => cm.ClassId == classId && cm.StudentId == studentId);

   if (classMember != null)
  {
_dbSet.Remove(classMember);
            }
   }
    }
}

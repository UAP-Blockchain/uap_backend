using Fap.Domain.Entities;

namespace Fap.Domain.Repositories;
public interface IUserRepository
{
    Task<User?> GetByIdAsync(int id);
    Task AddAsync(User user);
}

using Fap.Domain.Entities;

namespace Fap.Domain.Repositories
{
    public interface IWalletRepository : IGenericRepository<Wallet>
    {
        Task<Wallet?> GetByAddressAsync(string address);
        Task<Wallet?> GetByUserIdAsync(Guid userId);
        Task<List<Wallet>> GetActiveWalletsAsync();
        Task<bool> AddressExistsAsync(string address);
    }
}

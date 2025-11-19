using Fap.Domain.Entities;
using Fap.Domain.Repositories;
using Fap.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Fap.Infrastructure.Repositories
{
    public class WalletRepository : GenericRepository<Wallet>, IWalletRepository
    {
        public WalletRepository(FapDbContext context) : base(context)
        {
        }

        public async Task<Wallet?> GetByAddressAsync(string address)
        {
            return await _context.Set<Wallet>()
                .Include(w => w.User)
                .FirstOrDefaultAsync(w => w.Address == address);
        }

        public async Task<Wallet?> GetByUserIdAsync(Guid userId)
        {
            return await _context.Set<Wallet>()
                .Include(w => w.User)
                .FirstOrDefaultAsync(w => w.UserId == userId && w.IsActive);
        }

        public async Task<List<Wallet>> GetActiveWalletsAsync()
        {
            return await _context.Set<Wallet>()
                .Where(w => w.IsActive)
                .Include(w => w.User)
                .ToListAsync();
        }

        public async Task<bool> AddressExistsAsync(string address)
        {
            return await _context.Set<Wallet>()
                .AnyAsync(w => w.Address == address);
        }
    }
}

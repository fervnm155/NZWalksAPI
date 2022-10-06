using Microsoft.EntityFrameworkCore;
using NZWalksAPI.Data;
using NZWalksAPI.Models.Domain;

namespace NZWalksAPI.Repositories
{
    public class WalksRepository : IWalksRepository
    {
        private readonly NZWalksDbContext nZWalksDbContext;

        public WalksRepository(NZWalksDbContext nZWalksDbContext)
        {
            this.nZWalksDbContext = nZWalksDbContext;
        }

        public async Task<IEnumerable<Walk>> GetAllWalks()
        {
            return await nZWalksDbContext.Walks.Include(x => x.Region).Include(x => x.WalkDifficulty).ToListAsync();
        }

        public async Task<Walk> GetWalkById(Guid id)
        {
            return await nZWalksDbContext.Walks.Include(x => x.Region).Include(x => x.WalkDifficulty).FirstOrDefaultAsync(w => w.Id == id);
        }

        public async Task<Walk> AddWalk(Walk walk)
        {
            walk.Id = Guid.NewGuid();
            await nZWalksDbContext.AddAsync(walk);
            await nZWalksDbContext.SaveChangesAsync();
            return walk;
        }

        public async Task<Walk> UpdateWalk(Walk walk)
        {
            if (nZWalksDbContext.Walks.Where(w => w.Id == walk.Id).Count() < 1)
            {
                return null;
            }
            nZWalksDbContext.Walks.Update(walk);
            await nZWalksDbContext.SaveChangesAsync();
            return walk;
        }

        public async Task<Walk> DeleteWalk(Guid id)
        {
            var walk = await nZWalksDbContext.Walks.Where(w => w.Id == id).FirstOrDefaultAsync();
            if (walk != null)
            {
                nZWalksDbContext.Remove(walk);
                await nZWalksDbContext.SaveChangesAsync();
            }
            return walk;
        }
    }
}

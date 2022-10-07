using Microsoft.EntityFrameworkCore;
using NZWalksAPI.Data;
using NZWalksAPI.Models.Domain;

namespace NZWalksAPI.Repositories
{
    public class WalkDifficultyRepository : IWalkDifficultyRepository
    {
        private readonly NZWalksDbContext nZWalksDbContext;

        public WalkDifficultyRepository(NZWalksDbContext nZWalksDbContext)
        {
            this.nZWalksDbContext = nZWalksDbContext;
        }

        public async Task<IEnumerable<WalkDifficulty>> GetAllWalksDifficulties()
        {
            return await nZWalksDbContext.WalkDifficulties.ToListAsync();
        }

        public async Task<WalkDifficulty> GetDifficultyById(Guid id)
        {
            return await nZWalksDbContext.WalkDifficulties.FirstOrDefaultAsync(w => w.Id == id);
        }

        public async Task<WalkDifficulty> AddDifficulty(WalkDifficulty difficulty)
        {
            difficulty.Id = Guid.NewGuid();
            await nZWalksDbContext.AddAsync(difficulty);
            await nZWalksDbContext.SaveChangesAsync();
            return difficulty;
        }

        public async Task<WalkDifficulty> UpdateDifficulty(WalkDifficulty difficulty)
        {
            if (nZWalksDbContext.WalkDifficulties.Where(w => w.Id == difficulty.Id).Count() < 1)
            {
                return null;
            }
            nZWalksDbContext.WalkDifficulties.Update(difficulty);
            await nZWalksDbContext.SaveChangesAsync();
            return difficulty;
        }

        public async Task<WalkDifficulty> DeleteDifficulty(Guid id)
        {
            var difficulty = await nZWalksDbContext.WalkDifficulties.Where(w => w.Id == id).FirstOrDefaultAsync();
            if (difficulty != null)
            {
                nZWalksDbContext.Remove(difficulty);
                await nZWalksDbContext.SaveChangesAsync();
            }
            return difficulty;
        }
    }
}

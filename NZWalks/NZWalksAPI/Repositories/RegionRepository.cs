using Microsoft.EntityFrameworkCore;
using NZWalksAPI.Data;
using NZWalksAPI.Models.Domain;

namespace NZWalksAPI.Repositories
{
    public class RegionRepository : IRegionRepository
    {
        private readonly NZWalksDbContext nZWalksDbContext;

        public RegionRepository(NZWalksDbContext nZWalksDbContext)
        {
            this.nZWalksDbContext = nZWalksDbContext;
        }

        public async Task<IEnumerable<Region>> GetAll()
        {
            return await nZWalksDbContext.Regions.ToListAsync();
        }

        public async Task<Region> GetById(Guid id)
        {
            return await nZWalksDbContext.Regions.FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<Region> AddRegion(Region region)
        {
            region.Id = Guid.NewGuid();
            await nZWalksDbContext.AddAsync(region);
            await nZWalksDbContext.SaveChangesAsync();
            return region;
        }

        public async Task<Region> DeleteRegion(Guid id)
        {
            var region = await nZWalksDbContext.Regions.Where(r => r.Id == id).FirstOrDefaultAsync();
            if (region != null)
            {
                nZWalksDbContext.Remove(region);
                await nZWalksDbContext.SaveChangesAsync();
            }
            return region;
        }

        public async Task<Region> UpdateRegion(Region region)
        {
            if (nZWalksDbContext.Regions.Count(r => r.Id == region.Id) < 1)
            {
                return null;
            }

            nZWalksDbContext.Regions.Update(region);
            await nZWalksDbContext.SaveChangesAsync();
            return region;

            //ALTERNATIVE WAY WITHOUT USE UPDATE METHOD

            //var existingRegion = await nZWalksDbContext.Regions.FirstOrDefaultAsync(r => r.Id == id);
            //if(existingRegion == null)
            //{
            //    return null;
            //}
            //existingRegion.Code = region.Code;
            //existingRegion.Name = region.Name;
            //existingRegion.Area = region.Area;
            //existingRegion.Lat = region.Lat;
            //existingRegion.Long = region.Long;
            //existingRegion.Population = region.Population;

            //await nZWalksDbContext.SaveChangesAsync();

            //return existingRegion;
        }
    }
}

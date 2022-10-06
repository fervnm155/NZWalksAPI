using NZWalksAPI.Models.Domain;

namespace NZWalksAPI.Repositories
{
    public interface IRegionRepository
    {
        Task<IEnumerable<Region>> GetAll();
        Task<Region> GetById(Guid id);
        Task<Region> AddRegion(Region region);
        Task<Region> DeleteRegion(Guid id);
        Task<Region> UpdateRegion(Region region);
    }
}

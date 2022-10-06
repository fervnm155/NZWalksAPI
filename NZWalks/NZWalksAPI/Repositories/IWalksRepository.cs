using NZWalksAPI.Models.Domain;

namespace NZWalksAPI.Repositories
{
    public interface IWalksRepository
    {
        Task<IEnumerable<Walk>> GetAllWalks();
        Task<Walk> GetWalkById(Guid id);
        Task<Walk> AddWalk(Walk walk);
        Task<Walk> UpdateWalk(Walk walk);
        Task<Walk> DeleteWalk(Guid id);
    }
}

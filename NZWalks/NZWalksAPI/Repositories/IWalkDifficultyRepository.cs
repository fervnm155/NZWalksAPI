using NZWalksAPI.Models.Domain;

namespace NZWalksAPI.Repositories
{
    public interface IWalkDifficultyRepository
    {
        Task<IEnumerable<WalkDifficulty>> GetAllWalksDifficulties();
        Task<WalkDifficulty> GetDifficultyById(Guid id);
        Task<WalkDifficulty> AddDifficulty(WalkDifficulty difficulty);
        Task<WalkDifficulty> UpdateDifficulty(WalkDifficulty difficulty);
        Task<WalkDifficulty> DeleteDifficulty(Guid id);
    }
}

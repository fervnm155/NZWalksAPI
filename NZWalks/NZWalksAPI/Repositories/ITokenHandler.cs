using NZWalksAPI.Models.Domain;

namespace NZWalksAPI.Repositories
{
    public interface ITokenHandler
    {
        Task<string> GetToken(User user);
    }
}

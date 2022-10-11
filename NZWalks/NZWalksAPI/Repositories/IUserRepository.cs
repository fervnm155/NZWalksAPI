using NZWalksAPI.Models.Domain;

namespace NZWalksAPI.Repositories
{
    public interface IUserRepository
    {
        Task<User> Authenticate(string username, string password);
    }
}

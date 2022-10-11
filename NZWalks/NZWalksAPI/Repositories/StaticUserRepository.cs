using NZWalksAPI.Models.Domain;

namespace NZWalksAPI.Repositories
{
    public class StaticUserRepository : IUserRepository
    {
        private List<User> Users = new List<User>()
        {
            //new User()
            //{
            //    FirstName = "Normal", Lastname = "User", Email = "readonly@user.com",
            //    Id = Guid.NewGuid(), Username = "readonly@user.com", Password = "123",
            //    Roles = new List<string>{ "reader" }
            //},
            //new User()
            //{
            //    FirstName = "Admin", Lastname = "User", Email = "admin@user.com",
            //    Id = Guid.NewGuid(), Username = "admin@user.com", Password = "123",
            //    Roles = new List<string>{ "reader","writer" }
            //}
        };
        public async Task<User> Authenticate(string username, string password)
        {
            var user = Users.Find(x => x.Username.Equals(username, StringComparison.InvariantCultureIgnoreCase) && x.Password == password);

            return user;
        }
    }
}

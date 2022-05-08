using targetchatserver.Models;

namespace targetchatserver.Interfaces
{
    public interface IUserRepository
    {
        public Task<UserModel> GetUserByUsername(string username);
        public Task<UserModel> CreateUser(UserModel user);
        public Task<UserModel> Authenticate(UserLogin userLogin);

    }
}

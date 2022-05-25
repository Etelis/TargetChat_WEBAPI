using TargetChatServer11.Models;
using TargetChatServer11.Utils;

namespace TargetChatServer11.Interfaces
{
    public interface IUserRepository
    {
        public Task<UserModel> GetUserByUsername(string username);
        public Task<UserModel> CreateUser(UserModel user);
        public Task<UserModel> Authenticate(UserLogin userLogin);

    }
}

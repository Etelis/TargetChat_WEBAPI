using Microsoft.EntityFrameworkCore;
using TargetChatServer11.Interfaces;
using TargetChatServer11.Models;
using TargetChatServer11.Utils;

namespace TargetChatServer11.Data.Services
{
    public class UserRepository : IUserRepository
    {
        private readonly TargetChatServer11Context _context;

        public UserRepository(TargetChatServer11Context context)
        {
            _context = context;
        }

        public async Task<UserModel> CreateUser(UserModel user)
        {
            _context.UserModel.Add(user);
            await _context.SaveChangesAsync();
            return user;

        }

        public async Task<UserModel?> GetUserByUsername(string username)
        {
            var user =  await _context.UserModel.FirstOrDefaultAsync(User => User.Username.Equals(username));
            if (user == null)
            {
                return null;
            }

            return user;
        }

        public async Task<UserModel?> Authenticate(UserLogin userLogin)
        {
            return await _context.UserModel.FirstOrDefaultAsync(o => o.Username.Equals(userLogin.UserName) && o.Password == userLogin.Password);
            
        }
        private bool UserModelExists(string id)
        {
            return _context.UserModel.Any(e => e.Username == id);
        }

    }
}

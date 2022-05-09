using Microsoft.EntityFrameworkCore;
using targetchatserver.Interfaces;
using targetchatserver.Models;

namespace targetchatserver.Data.Services
{
    public class UserRepository : IUserRepository
    {
        private readonly targetchatserverContext _context;

        public UserRepository(targetchatserverContext context)
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

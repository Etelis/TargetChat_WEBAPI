using Microsoft.EntityFrameworkCore;
using targetchatserver.Interfaces;
using targetchatserver.Models;

namespace targetchatserver.Data.Services
{
    public class ContactRepository : IContactRepository
    {
        private readonly targetchatserverContext _context;

        public ContactRepository(targetchatserverContext context)
        {
            _context = context;
        }

        public Task<Contact> CreateContactOfUser(Contact contact) 
        {
            throw new NotImplementedException();
        }

        public Task<Contact> DeleteContactOfUser(string id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Contact>> GetAllContactsOfUser(UserModel user)
        {
            throw new NotImplementedException();
        }

        public async Task<Contact> GetContactById(string id, UserModel user)
        {
            throw new NotImplementedException();
        }

        private bool ContactExists(string id)
        {
            return _context.Contact.Any(e => e.Id == id);
        }
    }
}

using Microsoft.EntityFrameworkCore;
using targetchatserver.Interfaces;
using targetchatserver.Models;

namespace targetchatserver.Data.Services
{
    public class ContactRepository : IContactRepository
    {
        private readonly targetchatserverContext _context;
        private readonly IUserRepository _user;

        public ContactRepository(targetchatserverContext context, IUserRepository user)
        {
            _context = context;
            _user = user;

        }

        public async Task<Contact?> CreateContactOfUser(Contact contact, string username)
        {
            var userModel = await _user.GetUserByUsername(username);
            contact.User = userModel;
            var checkIfExist = await _context.Contact.FirstOrDefaultAsync(item => item.id.Equals(contact.id) && item.User == userModel);
            if (checkIfExist == null)
            {
                _context.Contact.Add(contact);
                await _context.SaveChangesAsync(); 
                return contact;
            }
            return null;
        }

        public async Task<Contact?> DeleteContactOfUser(string id, string username)
        {
            var contactToDelete = await GetContactById(id, username);
            if(contactToDelete == null)
            {
                return null;
            }
            _context.Remove(contactToDelete);
            await _context.SaveChangesAsync();
            return contactToDelete;
        }

        public async Task<List<Contact>> GetAllContactsOfUser(string username)
        {
            return await _context.Contact.Where(item => item.User.Username.Equals(username)).ToListAsync();
        }

        public async Task<Contact?> GetContactById(string id, string username)
        {
            var temp = await _context.Contact.Where(item => item.User.Username.Equals(username)).ToListAsync();
            var contact = temp.Find(o => o.id.Equals(id));
            return contact;
        }

        public async Task<Contact?> UpdateContactById(string id, string username, ContactUpdate contactUpdate)
        {
            var contact = await GetContactById(id, username);
            if(contact == null)
            {
                return null;
            }
            contact.name = contactUpdate.name;
            contact.server = contactUpdate.server;
            await _context.SaveChangesAsync();
            return contact;
        }
    }
}

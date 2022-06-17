using Microsoft.EntityFrameworkCore;
using TargetChatServer11.Interfaces;
using TargetChatServer11.Models;
using TargetChatServer11.Utils;

namespace TargetChatServer11.Data.Services
{
    public class ContactRepository : IContactRepository
    {
        private readonly TargetChatServer11Context _context;
        private readonly IUserRepository _user;

        public ContactRepository(TargetChatServer11Context context, IUserRepository user)
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
            if (contactToDelete == null)
            {
                return null;
            }
            _context.Contact.Remove(contactToDelete);
            await _context.SaveChangesAsync();
            return contactToDelete;
        }

        public async Task<List<Contact>> GetAllContactsOfUser(string username)
        {
            return await _context.Contact.Where(item => item.User.Username.Equals(username)).ToListAsync();
        }

        public async Task<Contact?> GetContactById(string id, string username)
        {
            var user = await _user.GetUserByUsername(username);
            var contact = await _context.Contact.FirstOrDefaultAsync(item => item.User == user && item.id.Equals(id));
            return contact;
        }

        public async Task<Contact?> UpdateContactById(string id, string username, ContactUpdate contactUpdate)
        {
            var contact = await GetContactById(id, username);
            if (contact == null)
            {
                return null;
            }
            contact.name = contactUpdate.name;
            contact.server = contactUpdate.server;
            await _context.SaveChangesAsync();
            return contact;
        }

        public async Task<Contact?> UpdateContactLastTimeMessageByID(string id, string username, string last)
        {
            var contact = await GetContactById(id, username);
            if (contact == null)
            {
                return null;
            }
            contact.last = last;
            contact.lastdate = DateTime.Now;
            await _context.SaveChangesAsync();
            return contact;
        }



    }
}

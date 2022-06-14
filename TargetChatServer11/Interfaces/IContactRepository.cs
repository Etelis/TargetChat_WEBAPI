using TargetChatServer11.Models;
using TargetChatServer11.Utils;

namespace TargetChatServer11.Interfaces
{
    public interface IContactRepository
    {
        Task<List<Contact>> GetAllContactsOfUser(string user);
        Task<Contact> GetContactById(string id, string username);
        Task<Contact> CreateContactOfUser(Contact newContact, string username);
        Task<Contact> DeleteContactOfUser(string id, string username);
        Task<Contact> UpdateContactById(string id, string username, ContactUpdate contactUpdate);
        Task<Contact?> UpdateContactLastTimeMessageByID(string id, string username, string last);
    }
}

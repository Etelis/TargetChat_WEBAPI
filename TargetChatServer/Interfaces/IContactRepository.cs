using targetchatserver.Models;

namespace targetchatserver.Interfaces
{
    public interface IContactRepository
    {
        Task<List<Contact>> GetAllContactsOfUser(string user);
        Task<Contact> GetContactById(string id, string username);
        Task<Contact> CreateContactOfUser(Contact newContact, string username);
        Task<Contact> DeleteContactOfUser(string id, string username);
        Task<Contact> UpdateContactById(string id, string username, ContactUpdate contactUpdate);
    }
}

using targetchatserver.Models;

namespace targetchatserver.Interfaces
{
    public interface IContactRepository
    {
        Task<List<Contact>> GetAllContactsOfUser(UserModel user);
        Task<Contact> GetContactById(string id, UserModel user);
        Task<Contact> CreateContactOfUser(Contact contact);
        Task<Contact> DeleteContactOfUser(string id);
    }
}

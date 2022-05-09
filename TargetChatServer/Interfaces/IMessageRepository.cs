

using targetchatserver.Models;

namespace targetchatserver.Interfaces
{
    public interface IMessageRepository
    {
        Task<List<Message>> GetMessagesByContact(Contact contact);
        Task<Message> GetMessageById(int messageId, Contact contact);
        Task<Message> CreateMessageOfContact(Message message);
        Task<Message> DeleteMessageByID(int messageId, Contact contact);   
        Task<Message> UpdateMessageById(Contact contact, int messageId, string content);
    }
}

using Microsoft.EntityFrameworkCore;
using targetchatserver.Interfaces;
using targetchatserver.Models;

namespace targetchatserver.Data.Services
{
    public class MessageRepository : IMessageRepository
    {
        private readonly targetchatserverContext _context;
        public MessageRepository(targetchatserverContext context)
        {
            _context = context; 
        }

        public async Task<Message> CreateMessageOfContact(Message message)
        {
            try
            {
                _context.Message.Add(message);
                await _context.SaveChangesAsync();
                return message;
            }

            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Message> DeleteMessageByID(int messageId, Contact contact)
        {
            var message = await _context.Message.FirstOrDefaultAsync(message => message.Id == messageId && message.Contact == contact);

            if (message == null)
                return null;

            _context.Message.Remove(message);
            await _context.SaveChangesAsync();

            return message;
        }

        public async Task<Message> GetMessageById(int messageId, Contact contact)
        {
            var message = await _context.Message.FirstOrDefaultAsync(message => message.Id == messageId && message.Contact == contact);
            if (message == null)
                return null;

            return message;
        }
        
        public async Task<List<Message>> GetMessagesByContact(Contact contact)
        {
            var messages = await _context.Message
                .Where(message => message.Contact == contact)
                .ToListAsync();

            return messages;
        }

        public async Task<Message> UpdateMessage(Message message, int messageId)
        {
            if (messageId != message.Id)
            {
                return null;
            }

            _context.Entry(message).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MessageExists(messageId))
                {
                    return null;
                }
                else
                {
                    throw;
                }
            }

            return message;
        }

        private bool MessageExists(int id)
        {
            return _context.Message.Any(e => e.Id == id);
        }
    }
}

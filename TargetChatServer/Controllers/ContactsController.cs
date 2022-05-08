#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using targetchatserver.Data;
using targetchatserver.Interfaces;
using targetchatserver.Models;

namespace targetchatserver.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ContactsController : ControllerBase
    {
        private readonly targetchatserverContext _context;
        private readonly IMessageRepository _messages;
        private readonly IContactRepository _contacts;


        private string getUserName()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                var userClaims = identity.Claims;

                return userClaims.FirstOrDefault(o => o.Type == ClaimTypes.NameIdentifier)?.Value;

            }
            return null;
        }

        private async Task<ActionResult<IEnumerable<Contact>>> getRelativeContacts()
        {
            var username = getUserName();
            return await _context.Contact.Where(item => item.User.Username.Equals(username)).ToListAsync();
        }


        public ContactsController(targetchatserverContext context, IMessageRepository messages, IContactRepository contacts)
        {
            _context = context;
            _messages = messages;
            _contacts = contacts;
        }

        // GET: api/Contacts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Contact>>> GetContact()
        {
            return await getRelativeContacts();
        }

        // GET: api/Contacts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Contact>> GetContact(string id)
        {
            var username = getUserName();
            var temp = await _context.Contact.Where(item => item.User.Username.Equals(username)).ToListAsync();
            var contact = temp.Find(o => o.Id.Equals(id));

            if (contact == null)
            {
                return NotFound();
            }

            return contact;
        }

        [HttpGet("{id}/messages")]
        public async Task<ActionResult<IEnumerable<Message>>> GetMessagesByContact(string id)
        {
            var contact = await GetContact(id);
            if (contact == null)
                return NotFound("Contact was not found");

            var messages = await _messages.GetMessagesByContact(contact.Value);

            return Ok(messages);
        }

        [HttpGet("{id}/messages/{messageid}")]
        public async Task<ActionResult<Message>> GetMessageByConact(string id, int messageid)
        {
            var contact = await GetContact(id);
            if (contact == null)
                return NotFound("Contact was not found");

            var message = await _messages.GetMessageById(messageid, contact.Value);
            if (message == null)
                return NotFound("Message with id = {messageid} was not found");


            return Ok(message);
        }


        // POST: api/Contacts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Contact>> PostContact(Contact contact)
        {
            var username = getUserName();
           // var contact = new Contact { ContactName=newContact.name, Id=newContact.id, LastDate=null, LastMessage=null, 
           // Messages = null, Server=newContact.server};
           // var temp = await _context.Contact.Where(item => item.User.Username.Equals(username)).ToListAsync();
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ContactExists(contact.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetContact", new { id = contact.Id }, contact);
        }

        // DELETE: api/Contacts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContact(string id)
        {
            var contact = await _context.Contact.FindAsync(id);
            if (contact == null)
            {
                return NotFound();
            }

            _context.Contact.Remove(contact);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ContactExists(string id)
        {
            return _context.Contact.Any(e => e.Id == id);
        }
    }
}

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
        private readonly IUserRepository _users;

        public ContactsController(targetchatserverContext context, IMessageRepository messages, IContactRepository contacts)
        {
            _context = context;
            _messages = messages;
            _contacts = contacts;
        }

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

        // GET: api/Contacts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Contact>>> GetContact()
        {
            return await _contacts.GetAllContactsOfUser(getUserName());
        }

        // POST: api/Contacts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Contact>> PostContact(TransferContact received)
        {
            var username = getUserName();
            var newContact = new Contact
            {
                id = received.id,
                name = received.name,
                lastdate = null,
                last = null,
                Messages = null,
                server = received.server
            };
            if (await _contacts.CreateContactOfUser(newContact, username) == null)
            {
                return BadRequest("Contact already exists!");
            }
            return Ok(newContact);
        }

        // GET: api/Contacts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Contact>> GetContact(string id)
        {
            return await _contacts.GetContactById(id, getUserName());
        }

        // DELETE: api/Contacts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContact(string id)
        {
            var ret = await _contacts.DeleteContactOfUser(id, getUserName());
            if (ret == null)
            {
                return NotFound("User does not exist!");
            }
            return Ok(ret);
        }

        // PUT: api/Contacts/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateContact(string id, [FromBody] ContactUpdate contactUpdate)
        {
            var username = getUserName();
            var ret = await _contacts.UpdateContactById(id, username, contactUpdate);
            if(ret == null) 
            {
                return BadRequest("User not found!");
            }
            return Ok(ret);
        }

        // GET: api/Contacts/{id}/messages
        [HttpGet("{id}/messages")]
        public async Task<ActionResult<IEnumerable<Message>>> GetMessagesByContact(string id)
        {
            var contact = await _contacts.GetContactById(id, getUserName());
            if (contact == null)
                return NotFound("Contact was not found");

            var messages = await _messages.GetMessagesByContact(contact);

            return Ok(messages);
        }

        // POST: api/Contacts/{id}/messages
        [HttpPost("{id}/messages/")]
        public async Task<ActionResult<Message>> PostMessage(string id, [FromBody] ContentToPost content)
        {
            var contact = await _contacts.GetContactById(id, getUserName());
            if (contact == null)
                return NotFound("Contact was not found");

            var message = new Message()
            {
                Content = content.content,
                Date = DateTime.Now,
                Sent = false,
                Contact = contact
            };

            if (await _messages.CreateMessageOfContact(message) == null)
            {
                return BadRequest("Error inserting message");
            }
            return Ok();
        }

        // GET: api/Contacts/{id}/messages/{m_id}
        [HttpGet("{id}/messages/{messageid}")]
        public async Task<ActionResult<Message>> GetMessageByContact(string id, int messageid)
        {
            var contact = await _contacts.GetContactById(id, getUserName());
            if (contact == null)
                return NotFound("Contact was not found");

            var message = await _messages.GetMessageById(messageid, contact);
            if (message == null)
            {
                return NotFound("Message with id = {messageid} was not found");
            }
            return Ok(message);
        }

        // Delete: api/Contacts/{id}/messages/{m_id}
        [HttpDelete("{id}/messages/{messageid}")]
        public async Task<ActionResult<Message>> DeleteMessageByContact(string id, int messageid)
        {
            var contact = await _contacts.GetContactById(id, getUserName());
            if (contact == null)
                return NotFound("Contact was not found");

            var message = await _messages.DeleteMessageByID(messageid, contact);
            if (message == null)
            {
                return NotFound("Message with id = {messageid} was not found");
            }
            return Ok(message);
        }

        // PUT: api/Contacts/{id}/messages/{m_id}
        [HttpPut("{id}/messages/{messageid}")]
        public async Task<ActionResult<Message>> UpdateMessageByContact(string id, int messageid, [FromBody] ContentToPost content)
        {
            var contact = await _contacts.GetContactById(id, getUserName());
            if (contact == null)
            {
                return NotFound("Contact was not found");
            }

            var message = await _messages.UpdateMessageById(contact, messageid, content.content);
            if (message == null)
            {
                return NotFound("Message with id = {messageid} was not found");
            }
            return Ok(message);
        }
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TargetChatServer11.Hubs;
using TargetChatServer11.Interfaces;
using TargetChatServer11.Models;
using TargetChatServer11.Utils;

namespace TargetChatServer11.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class inviteController : ControllerBase
    {
        private readonly IUserRepository _users;
        private readonly IContactRepository _contacts;
        private readonly ContactHub _contactHub;

        public inviteController(IUserRepository users, IContactRepository contacts, ContactHub contactHub)
        {
            _users = users;
            _contacts = contacts;
            _contactHub = contactHub;
        }

        // POST: api/invitations
        [HttpPost]
        public async Task<ActionResult> PostMessage(Invitation invite)
        {
            var user = await _users.GetUserByUsername(invite.To);
            if (user == null)
                return NotFound("To user was not found in DB");

            var contact = new Contact()
            {
                id = invite.From,
                server = invite.Server,
                name = invite.From,
                lastdate = DateTime.Now,
                last = null,
                Messages = null,
                User = user
            };

            if (await _contacts.CreateContactOfUser(contact, invite.To) == null)
                return BadRequest("Error adding new contact");

            //Notify connected user that contact has been added, SignalR
            await _contactHub.AddContact(invite.To, contact);

            return StatusCode(201);
        }

    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using targetchatserver.Interfaces;
using targetchatserver.Models;

namespace targetchatserver.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class invitationsController : ControllerBase
    {
        private readonly IUserRepository _users;
        private readonly IContactRepository _contacts;

        public invitationsController(IUserRepository users, IContactRepository contacts)
        {
            _users = users;
            _contacts = contacts;
        }

        // POST: api/invitations
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult> PostMessage(Invitation invite)
        {
            var user = await _users.GetUserByUsername(invite.To);
            if (user == null)
                return NotFound("To user was not found in DB");

            var contact = new Contact()
            {
                Id = invite.From,
                Server = invite.Server,
                ContactName = invite.From,
                LastDate = DateTime.UtcNow,
                LastMessage = null,
                Messages = null,
                User = user
            };

            if (await _contacts.CreateContactOfUser(contact) == null)
                return BadRequest("Error adding new contact");

            return Ok();
        }

    }
}

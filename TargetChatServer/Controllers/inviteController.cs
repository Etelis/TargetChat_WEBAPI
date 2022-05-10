using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using targetchatserver.Interfaces;
using targetchatserver.Models;

namespace targetchatserver.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class inviteController : ControllerBase
    {
        private readonly IUserRepository _users;
        private readonly IContactRepository _contacts;

        public inviteController(IUserRepository users, IContactRepository contacts)
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
                id = invite.From,
                server = invite.Server,
                name = invite.From,
                lastdate = DateTime.UtcNow,
                last = null,
                Messages = null,
                User = user
            };

            if (await _contacts.CreateContactOfUser(contact, invite.To) == null)
                return BadRequest("Error adding new contact");
            return Ok();
        }

    }
}

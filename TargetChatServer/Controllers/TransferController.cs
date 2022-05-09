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
    public class TransferController : ControllerBase
    {
        private readonly IUserRepository _users;
        private readonly IContactRepository _contacts;
        private readonly IMessageRepository _messages;

        public TransferController(IUserRepository users, IContactRepository contacts, IMessageRepository messages)
        {
            _users = users;
            _contacts = contacts;
            _messages = messages;
        }

        // POST: api/Transfer
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult> PostMessage(TransferMessage transfer)
        {
            var user = await _users.GetUserByUsername(transfer.To);
            if (user == null)
                return NotFound("To user was not found in DB");

            var contact = await _contacts.GetContactById(transfer.From, user.Username);
            if (contact == null)
                return NotFound("Contact was not found in user");

            var message = new Message()
            {
                Content = transfer.content,
                Date = DateTime.Now,
                Sent = true,
                Contact = contact,
            };

            if (await _messages.CreateMessageOfContact(message) == null)
                return BadRequest("Error inserting message");
            return Ok();
        }


    }
}

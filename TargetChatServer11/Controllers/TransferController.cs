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
using TargetChatServer11.Data;
using TargetChatServer11.Interfaces;
using TargetChatServer11.Models;
using TargetChatServer11.Hubs;
using TargetChatServer11.Utils;

namespace TargetChatServer11.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TransferController : ControllerBase
    {
        private readonly IUserRepository _users;
        private readonly IContactRepository _contacts;
        private readonly IMessageRepository _messages;
        private readonly MessageHub _messageHub;
        private readonly ContactHub _contactHub;

        public TransferController(IUserRepository users, IContactRepository contacts, IMessageRepository messages, MessageHub messageHub, ContactHub contactHub)
        {
            _users = users;
            _contacts = contacts;
            _messages = messages;
            _messageHub = messageHub;
            _contactHub = contactHub;
        }

        // POST: api/Transfer
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
                Date = DateTime.Now.ToString("r"),
                Sent = true,
                Contact = contact,
            };

            if (await _messages.CreateMessageOfContact(message) == null)
                return BadRequest("Error inserting message");

            await _contacts.UpdateContactLastTimeMessageByID(transfer.From, user.Username,transfer.content);
            // SignalR - Update connected user with new message.
            await _contactHub.ContactUpdate(user.Username, contact);
            await _messageHub.RecivedMessage(message, transfer.From, transfer.To);

            return StatusCode(201);
        }


    }
}

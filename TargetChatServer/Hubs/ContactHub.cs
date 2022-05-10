using Microsoft.AspNetCore.SignalR;
using targetchatserver.Models;

namespace targetchatserver.Hubs
{
    public class ContactHub : Hub
    {
        private readonly IDictionary<string, string> _connections;

        public ContactHub(IDictionary<string, string> connections)
        {
            _connections = connections;
        }

        public async Task AddContact(string username, Contact contact)
        {
            var connectionID = _connections[username];
            await Clients.Client(connectionID).SendAsync("ReceiveContact", new ContactToPost {
               id = contact.id, last = contact.last, lastdate = contact.lastdate, name = contact.name, server = contact.server
            });
        }

        public async Task ConnectClientToChat(string userConnection)
        {
            _connections[userConnection] = Context.ConnectionId;
        }
        public override async Task OnDisconnectedAsync(Exception e)
        {
            var item = _connections.First(kvp => kvp.Value.Equals(Context.ConnectionId));
            _connections.Remove(item);
        }
    }
}

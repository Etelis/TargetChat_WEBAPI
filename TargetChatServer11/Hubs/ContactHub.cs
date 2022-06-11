using Microsoft.AspNetCore.SignalR;
using TargetChatServer11.Models;
using TargetChatServer11.Utils;

namespace TargetChatServer11.Hubs
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
            try
            {
                var connectionID = _connections[username];
                await Clients.Client(connectionID).SendAsync("ReceiveContact", new ContactToPost
                {
                    id = contact.userName,
                    last = contact.last,
                    lastdate = contact.lastdate,
                    name = contact.displayName,
                    server = contact.server
                });
                return;
            }

            catch
            {
                return;
            }
        }

        public async Task ContactUpdate(string username, Contact contact)
        {
            try
            {
                var connectionID = _connections[username];
                await Clients.Client(connectionID).SendAsync("ContactUpdate", contact.userName);
                return;
            }

            catch { return; }
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

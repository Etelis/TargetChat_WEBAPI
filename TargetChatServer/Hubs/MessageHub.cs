using Microsoft.AspNetCore.SignalR;
using targetchatserver.Models;

namespace targetchatserver.Hubs
{
    public class MessageHub : Hub
    {
        private readonly IDictionary<UserConnection, string> _connections;
        public MessageHub(IDictionary<UserConnection, string> connections)
        {
            _connections = connections;
        }
        public async Task ConnectClientToChat(UserConnection userConnection)
        {
            _connections[userConnection] = Context.ConnectionId;
        }


        public async Task RecivedMessage(Message message, string fromContact, string toUser)
        {
            var userConnection = _connections.Keys.FirstOrDefault(c => c.username.Equals(toUser) && c.contactID.Equals(fromContact));
            if (userConnection == null)
                return;

            var connectionID = _connections[userConnection];
            await Clients.Client(connectionID).SendAsync("ReceiveMessage", );
        }
    }
}

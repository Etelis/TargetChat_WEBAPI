using Microsoft.AspNetCore.SignalR;
using TargetChatServer11.Models;
using TargetChatServer11.Utils;

namespace TargetChatServer11.Hubs
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

        public override async Task OnDisconnectedAsync(Exception e)
        {
            var item = _connections.First(kvp => kvp.Value.Equals(Context.ConnectionId));
            _connections.Remove(item);
        }

        public async Task RecivedMessage(Message message, string fromContact, string toUser)
        {
            try
            {
                var userConnection = _connections.Keys.FirstOrDefault(c => c.username.Equals(toUser) && c.contactID.Equals(fromContact));
                if (userConnection == null)
                    return;

                var connectionID = _connections[userConnection];
                await Clients.Client(connectionID).SendAsync("ReceiveMessage", new MessageToPost
                {
                    Id = message.id,
                    Content = message.content,
                    Date = message.created,
                    Sent = message.sent
                });
            }

            catch { return; }
        }
    }
}

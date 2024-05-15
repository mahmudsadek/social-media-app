using Microsoft.AspNetCore.SignalR;
using social_media_app.Models;

namespace social_media_app.Hubs
{
    public class ChatHub : Hub
    {


        private readonly IDictionary<string, UserRoomConnection> _connection;

        public ChatHub(IDictionary<string, UserRoomConnection> connection)
        {
            _connection = connection;
        }

        public async Task JoinRoom(UserRoomConnection userConnection)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, userConnection.Room!);
            _connection[Context.ConnectionId] = userConnection;
            await Clients.Group(userConnection.Room!)
                .SendAsync("ReceiveMessage", "Lets Program Bot", $"{userConnection.User} has Joined the Group", DateTime.Now);
            await SendConnectedUser(userConnection.Room!);
        }

        public async Task SendMessage(string message)
        {
            if (_connection.TryGetValue(Context.ConnectionId, out UserRoomConnection userRoomConnection))
            {
                await Clients.Group(userRoomConnection.Room!)
                    .SendAsync("ReceiveMessage", userRoomConnection.User, message, DateTime.Now);
            }
        }

        public override Task OnDisconnectedAsync(Exception? exp)
        {
            if (!_connection.TryGetValue(Context.ConnectionId, out UserRoomConnection roomConnection))
            {
                return base.OnDisconnectedAsync(exp);
            }

            _connection.Remove(Context.ConnectionId);
            Clients.Group(roomConnection.Room!)
                .SendAsync("ReceiveMessage", "Lets Program bot", $"{roomConnection.User} has Left the Group", DateTime.Now);
            SendConnectedUser(roomConnection.Room!);
            return base.OnDisconnectedAsync(exp);
        }

        public Task SendConnectedUser(string room)
        {
            var users = _connection.Values
                .Where(u => u.Room == room)
                .Select(s => s.User);
            return Clients.Group(room).SendAsync("ConnectedUser", users);
        }

    }


    //-----------------------------------------------------------------------

    /*
    public async Task JoinChat(User user)
        {
            await Clients.All
                .SendAsync("ReceiveMessage", "User", $"{user.UserName} has joined");
        }
    */
        //public async Task joinSpecificChat(User user)
        //{
        //    await Groups.AddToGroupAsync(Context.ConnectionId)
        //}
/*
        public Task SendPrivateMessage(Chat chat, Message message)
        {

            return Clients.User(chat.ReceiverId).SendAsync("ReceiveMessage", message);
        }
*/
        //public async Task AddToGroup(string groupName)
        //{
        //    await Groups.AddToGroupAsync(Context.ConnectionId, groupName);

        //    await Clients.Group(groupName).SendAsync("ReceiveMessage", $"{Context.ConnectionId} has joined the group {groupName}.");
        //}
/*
        public async Task JoinRoom(UserRoomConnection userConnection)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, userConnection.Room!);

            await Clients.Group(userConnection.Room!)
                .SendAsync("ReceiveMessage", $"{userConnection.User} has joined the group.");
        }

        public async Task RemoveFromGroup(string groupName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);

            await Clients.Group(groupName).SendAsync("Send", $"{Context.ConnectionId} has left the group {groupName}.");
        }
*/
}

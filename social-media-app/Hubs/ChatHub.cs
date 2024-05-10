﻿using Microsoft.AspNetCore.SignalR;
using social_media_app.Models;

namespace social_media_app.Hubs
{
    public class ChatHub : Hub
    {
        public async Task JoinChat(User user)
        {
            await Clients.All
                .SendAsync("ReceiveMessage", "User", $"{user.UserName} has joined");
        }

        //public async Task joinSpecificChat(User user)
        //{
        //    await Groups.AddToGroupAsync(Context.ConnectionId)
        //}

        public Task SendPrivateMessage(string user, string message)
        {
            return Clients.User(user).SendAsync("ReceiveMessage", message);
        }
    }
}

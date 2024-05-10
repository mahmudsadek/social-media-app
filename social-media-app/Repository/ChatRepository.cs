using Microsoft.EntityFrameworkCore;
using social_media_app.DBContext;
using social_media_app.Models;
using System.Linq;

namespace social_media_app.Repository
{
    public class ChatRepository : Repository<Chat>, IChatRepository
    {
        public ChatRepository(Context _context) : base(_context)
        {

        }

        public Chat GetChatWithMessagesAndUsers(int Id, string include)
        {
            Chat chat = Context.Chats.Include(include)
                .FirstOrDefault(chat => chat.Id == Id);

            return chat;
        }

        public void DeleteChatHaveMessages(Chat chat)
        {
            Context.Messages.RemoveRange(chat.Messages);
            Context.Chats.Remove(chat);
            Context.SaveChanges();
        }

        public List<Message> GetAllUserMessages(Chat chat, string userId)
        {
            List<Message> messages = new();

            foreach (Message message in chat.Messages)
            {
                messages = Context.Messages.Where(m => m.SenderMessageId == userId).ToList();
            }
            return messages;
        }

        public string GetUserName(string userId)
        {
            User user = Context.User.FirstOrDefault(user => user.Id == userId);
            return user.Name;
        }


    }
}

using Microsoft.EntityFrameworkCore;
using social_media_app.DBContext;
using social_media_app.Models;

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

        public List<Message> GetAllUserMessages(int chatId, string userId)
        {
            Chat chat = GetChatWithMessagesAndUsers(chatId, "Messages");
            List<Message> messages = new();

            foreach (Message message in chat.Messages)
            {
                messages = Context.Messages.Where(m => m.SenderMessageId == userId).ToList();
            }
            return messages;
        }


    }
}

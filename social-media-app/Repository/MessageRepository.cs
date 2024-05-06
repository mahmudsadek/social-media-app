using Microsoft.EntityFrameworkCore;
using social_media_app.DBContext;
using social_media_app.Models;

namespace social_media_app.Repository
{
    public class MessageRepository : Repository<Message> ,IMessageRepository
    { 
        public MessageRepository(Context _context) : base(_context)
        {

        }

        public string GetMessageText(int msgId)
        {
            Message message = Context.Messages.FirstOrDefault(m => m.Id == msgId);

            return message.TextMessage;
        }

    }
}

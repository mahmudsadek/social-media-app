using social_media_app.DBContext;
using social_media_app.Models;

namespace social_media_app.Repository
{
    public class MessageRepository : Repository<Message> ,IMessageRepository
    { 
        public MessageRepository(Context _context) : base(_context)
        {

        }
    }
}

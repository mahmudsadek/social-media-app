using social_media_app.DBContext;
using social_media_app.Models;

namespace social_media_app.Repository
{
    public class ChatRepository : Repository<Chat>, IChatRepository
    {
        public ChatRepository(Context _context) : base(_context)
        {

        }
    }
}

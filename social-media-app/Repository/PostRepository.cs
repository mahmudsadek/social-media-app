using social_media_app.DBContext;
using social_media_app.Models;

namespace social_media_app.Repository
{
    public class PostRepository : Repository<Post>, IPostRepository
    {
        public PostRepository(Context _context) : base(_context)
        {
        }
    }
}

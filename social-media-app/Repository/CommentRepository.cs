using social_media_app.DBContext;
using social_media_app.Models;
namespace social_media_app.Repository
{
    public class CommentRepository :Repository<Comment>, ICommentRepository
    {
        public CommentRepository(Context _context) : base(_context)
        {
        }
    }
}

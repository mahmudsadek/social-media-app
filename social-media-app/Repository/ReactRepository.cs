using social_media_app.DBContext;
using social_media_app.Models;

namespace social_media_app.Repository
{
    public class ReactRepository : Repository<React> , IReactRepository
    {
        public ReactRepository(Context _context) : base(_context)
        {
        }
        public List<React> GetAll(int postId)
        {
            return Context.React.Where(p => p.PostId == postId).ToList();
        }
    }
}

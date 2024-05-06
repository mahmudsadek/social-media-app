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

        public string CheckReactOnPost(int postId, string userId)
        {
            React? react = Context.React.Where(r => r.PostId == postId && r.UserId == userId).FirstOrDefault();

            return react != null ? "Found" : "Not found";
        }
    }
}

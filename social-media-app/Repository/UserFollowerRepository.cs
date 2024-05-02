using Microsoft.EntityFrameworkCore;
using social_media_app.DBContext;
using social_media_app.Models;

namespace social_media_app.Repository
{
    public class UserFollowerRepository : Repository<UserFollower>, IUserFollowerRepository
    {
        private readonly Context context;

        public UserFollowerRepository(Context _context) : base(_context)
        {
            context = _context;
        }

        public List<UserFollower> GetFollowersSuggestionById(string Id)
        {
            return context.UserFollower.Include(f => f.User).Where(f => f.FollowerID != Id).ToList();
        }
    }
}

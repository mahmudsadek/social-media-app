using social_media_app.DBContext;
using social_media_app.Models;

namespace social_media_app.Repository
{
    public class ReplayRepository : Repository<Replay>, IReplayRepository
    {
        public ReplayRepository(Context _context) : base(_context)
        {
        }
    }
}

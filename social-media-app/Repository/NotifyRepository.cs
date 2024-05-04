using social_media_app.DBContext;
using social_media_app.Models;

namespace social_media_app.Repository
{
    public class NotifyRepository : Repository<Notify>, INotifyRepository
    {
        public NotifyRepository(Context _context) : base(_context)
        {
        }
    }
}

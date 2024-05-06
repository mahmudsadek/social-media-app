using social_media_app.DBContext;
using social_media_app.Models;

namespace social_media_app.Repository
{
    public class BookmarkRepositroy : Repository<Bookmark>, IBookmarkRepository
    {
        public BookmarkRepositroy(Context _context) : base(_context) 
        {

        }
        
    }
}

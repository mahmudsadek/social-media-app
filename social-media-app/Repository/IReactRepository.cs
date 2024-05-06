using social_media_app.Models;

namespace social_media_app.Repository
{
    public interface IReactRepository : IRepository<React>
    {
        public List<React> GetAll(int postId);
    }
}

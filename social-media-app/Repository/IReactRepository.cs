using social_media_app.Models;

namespace social_media_app.Repository
{
    public interface IReactRepository : IRepository<React>
    {

        public React GetReact(int postId, string userId);
        public string CheckReactOnPost(int postId, string userId);
        public List<React> GetAll(int postId);
    }
}

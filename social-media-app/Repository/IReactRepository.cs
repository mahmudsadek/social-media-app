using social_media_app.Models;

namespace social_media_app.Repository
{
    public interface IReactRepository : IRepository<React>
    {
        List<React> GetAll(string[] includes = null);

        React Get(int id);

        List<React> Get(Func<React, bool> where);


        int? GetReactCount(int postId, bool isLike);

        void Insert(React item);

        void Update(React item);

        void Delete(React item);

        void Save();
    }
}

using social_media_app.Models;

namespace social_media_app.Repository
{
    public interface IPostRepository: IRepository<Post>
    {

        //Post GetById(int id);
        //List<Post> GetAll();
        //void Insert(Post post);
        //void Update(Post post);
        //void Delete(int id);
        //void Save();

        void DeleteWithRelatedEntities(Post post);

        //helloooo

    }
}

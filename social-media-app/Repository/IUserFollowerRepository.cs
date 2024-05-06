using social_media_app.Models;

namespace social_media_app.Repository
{
    public interface IUserFollowerRepository:IRepository<UserFollower>
    {
        List<UserFollower> GetFollowersSuggestionById(string Id);
    }
}

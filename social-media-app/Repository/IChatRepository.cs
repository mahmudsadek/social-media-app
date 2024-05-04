using social_media_app.DBContext;
using social_media_app.Models;

namespace social_media_app.Repository
{
    public interface IChatRepository : IRepository<Chat>
    {
        public Chat? Get(int Id, string include);

        public void DeleteChatHaveMessages(Chat chat);
    }


}

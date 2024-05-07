using social_media_app.DBContext;
using social_media_app.Models;

namespace social_media_app.Repository
{
    public interface IChatRepository : IRepository<Chat>
    {
        public Chat? GetChatWithMessagesAndUsers(int Id, string include);

        public List<Message> GetAllUserMessages(Chat chat,string userId);

        public void DeleteChatHaveMessages(Chat chat);

        public string GetUserName (string userId);
    }

}

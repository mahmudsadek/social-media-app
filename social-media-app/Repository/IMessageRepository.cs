using social_media_app.Models;

namespace social_media_app.Repository
{
    public interface IMessageRepository : IRepository<Message>
    {
        public string GetMessageText(int msgId);
    }
}

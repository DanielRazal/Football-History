namespace Server.Repositories.Inside_Interfaces
{
    public interface IMessageRepository
    {
        Task<Message> AddMessage(Message message);
        Task<List<Message>> GetAllMessages();
    }
}

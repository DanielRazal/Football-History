namespace Server.Repositories.Outside_Interfaces
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllUsers();
        Task<User> GetUserById(int id);
        Task<User> DeleteUser(int id);
        Task<User> AddUser(User user);
        Task<bool> Login(User user);
        Task<bool> UserNameExists(string userName);
        Task<bool> EmailExists(string email);
        Task<User> GetUserByEmail(string email);
        Task<User> GetUserByUserName(string userName);
        Task<(User, User)> Get2IdsByUsers(int firstId, int secondId);
    }

}

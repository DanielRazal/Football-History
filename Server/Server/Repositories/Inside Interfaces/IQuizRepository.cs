namespace Server.Repositories.Inside_Interfaces
{
    public interface IQuizRepository
    {
        Task<List<Quiz>> GetAllQuizzes();
        Task<Quiz> GetQuizById(int id);
    }
}

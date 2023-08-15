namespace Server.Repositories.Inside_Interfaces
{
    public interface IQuestionRepository
    {
        Task<List<Question>> GetAllQuestions();
        Task<Question> GetQuestionById(int id);
    }
}

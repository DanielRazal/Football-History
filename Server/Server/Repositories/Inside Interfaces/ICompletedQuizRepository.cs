namespace Server.Repositories.Inside_Interfaces
{
    public interface ICompletedQuizRepository
    {
        Task<List<CompletedQuiz>> GetAllCompletedQuizzes();
        Task<CompletedQuiz> AddCompletedQuiz(CompletedQuiz completedQuiz, List<SelectedAnswerDTO> selectedAnswerDTO);
        Task<Answer> GetAnswerById(int id);
        Task<CompletedQuiz> CompletedQuizById(int id);
        Task<bool> HasCompletedQuiz(int userId, int quizId);
        Task<int> GetNumberOfCorrectAnswers(int questionId);
        Task<Question> GetQuestionById(int id);
    }
}

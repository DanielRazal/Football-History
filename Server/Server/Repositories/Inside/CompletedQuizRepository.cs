namespace Server.Repositories.Inside
{
    public class CompletedQuizRepository : ICompletedQuizRepository
    {
        private readonly ServerContext _context;
        public CompletedQuizRepository(ServerContext context)
        {
            _context = context;
        }

        public void AddSelectedAnswerToCompletedQuiz(CompletedQuiz completedQuiz, int answerId)
        {
            var question = completedQuiz.Quiz.Questions.FirstOrDefault
            (q => q.Answers != null && q.Answers.Any(a => a.Id == answerId));
            if (question != null)
            {
                var selectedAnswer = new SelectedAnswer
                {
                    AnswerId = answerId,
                    Answer = question.Answers.FirstOrDefault(a => a.Id == answerId)!
                };

                completedQuiz.SelectedAnswers.Add(selectedAnswer);
            }
        }

        public async Task<CompletedQuiz> AddCompletedQuiz(CompletedQuiz completedQuiz, List<SelectedAnswerDTO> selectedAnswerDTOs)
        {
            completedQuiz.SelectedAnswers ??= new List<SelectedAnswer>();

            foreach (var selectedAnswerDTO in selectedAnswerDTOs)
            {
                var answer = await _context.Answers.FindAsync(selectedAnswerDTO.AnswerId);

                if (answer != null)
                {
                    var selectedAnswer = new SelectedAnswer
                    {
                        AnswerId = selectedAnswerDTO.AnswerId,
                        Answer = answer
                    };

                    completedQuiz.SelectedAnswers.Add(selectedAnswer);
                }
                else
                {
                    Console.WriteLine($"Answer with AnswerId {selectedAnswerDTO.AnswerId} not found.");
                }
            }

            completedQuiz.Id = 0;
            await _context.AddAsync(completedQuiz);
            await _context.SaveChangesAsync();

            return completedQuiz;
        }

        public async Task<bool> HasCompletedQuiz(int userId, int quizId)
        {
            var completedQuiz = await _context.CompletedQuizzes
                .Where(cq => cq.UserId == userId && cq.QuizId == quizId)
                .FirstOrDefaultAsync();

            return completedQuiz != null;
        }


        public async Task<List<CompletedQuiz>> GetAllCompletedQuizzes()
        {
            return await _context.CompletedQuizzes
                .Include(cq => cq.User)
                .Include(cq => cq.Quiz)
                .Include(cq => cq.Quiz.Questions)
                .Include(sq => sq.SelectedAnswers)
                .ThenInclude(sq => sq.Answer).OrderByDescending(sq => sq.Score)
                .ToListAsync();
        }

        public async Task<Answer> GetAnswerById(int id)
        {
            var answer = await _context.Answers.FirstOrDefaultAsync(u => u.Id == id);
            if (answer != null)
                return answer;
            else return null!;
        }

        public async Task<Quiz> GetQuizById(int id)
        {
            var quiz = await _context.Quizzes.Include(a => a.Questions)
             .FirstOrDefaultAsync(u => u.Id == id);
            if (quiz != null)
                return quiz;
            else return null!;
        }

        public async Task<Question> GetQuestionById(int id)
        {
            var question = await _context.Questions
                .Include(q => q.QuestionType)
                .Include(q => q.Answers)
                .Include(q => q.Quiz)
                .FirstOrDefaultAsync(u => u.Id == id);

            return question!;
        }
        public async Task<CompletedQuiz> CompletedQuizById(int id)
        {
            var completedQuiz = await _context.CompletedQuizzes.Include(cq => cq.User)
                .Include(cq => cq.Quiz)
                .Include(cq => cq.Quiz.Questions)
                .Include(sq => sq.SelectedAnswers)
            .FirstOrDefaultAsync(u => u.Id == id);
            if (completedQuiz != null)
                return completedQuiz;
            else return null!;
        }

        public async Task<int> GetNumberOfCorrectAnswers(int questionId)
        {
            var correctAnswersCount = await _context.Answers
                .Where(answer => answer.QuestionId == questionId && answer.IsCorrect)
                .CountAsync();

            return correctAnswersCount;
        }
    }

}

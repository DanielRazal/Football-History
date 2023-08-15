using System.Security.Cryptography;

namespace Server.Repositories.Inside
{
    public class QuizRepository : IQuizRepository
    {
        private readonly ServerContext _context;
        public QuizRepository(ServerContext context)
        {
            _context = context;
        }

        public async Task<List<Quiz>> GetAllQuizzes()
        {
            List<Quiz> allQuizzes = await _context.Quizzes
                .Include(q => q.Questions)
                    .ThenInclude(q => q.QuestionType)
                .Include(q => q.Questions)
                    .ThenInclude(q => q.Answers)
                .ToListAsync();

            ShuffleQuestionsInQuizzes(allQuizzes);

            return allQuizzes;
        }

        private void ShuffleQuestionsInQuizzes(List<Quiz> quizzes)
        {
            int seed = GenerateRandomSeed();
            Random rng = new Random(seed);

            foreach (var quiz in quizzes)
            {
                quiz.Questions = quiz.Questions.OrderBy(q => rng.Next()).ToList();

                foreach (var question in quiz.Questions)
                {
                    question.Answers = question.Answers.OrderBy(a => rng.Next()).ToList();
                }
            }
        }

        private int GenerateRandomSeed()
        {
            byte[] randomBytes = new byte[4];

            using (RandomNumberGenerator rngCrypto = RandomNumberGenerator.Create())
            {
                rngCrypto.GetBytes(randomBytes);
            }

            return BitConverter.ToInt32(randomBytes, 0);
        }

        public async Task<Quiz> GetQuizById(int id)
        {
            var quiz = await _context.Quizzes.Include(a => a.Questions)
             .FirstOrDefaultAsync(u => u.Id == id);
            if (quiz != null)
                return quiz;
            else return null!;
        }
    }

}

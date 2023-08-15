using System.Text.Json.Serialization;
using System.Text.Json;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompletedQuizController : ControllerBase
    {
        private readonly ICompletedQuizRepository _completedQuizRepo;
        private readonly IUserRepository _userRepo;
        private readonly IQuizRepository _quizRepo;


        public CompletedQuizController(ICompletedQuizRepository repo, IUserRepository userRepo
        , IQuizRepository quizRepo)
        {
            _completedQuizRepo = repo;
            _userRepo = userRepo;
            _quizRepo = quizRepo;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllCompletedQuizzes()
        {
            var completedQuizzes = await _completedQuizRepo.GetAllCompletedQuizzes();

            var json = System.Text.Json.JsonSerializer.Serialize(completedQuizzes, new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.IgnoreCycles,
                IncludeFields = true,
                WriteIndented = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            return Ok(json);
        }

        [HttpPost]
        public async Task<ActionResult<CompletedQuiz>> AddCompletedQuiz(int userId,
        List<SelectedAnswerDTO> selectedAnswerDTO)
        {
            var user = await _userRepo.GetUserById(userId);
            var quiz = await _quizRepo.GetQuizById(1);

            var existingCompletedQuiz = await _completedQuizRepo.HasCompletedQuiz(userId, quiz.Id);
            if (existingCompletedQuiz)
            {
                return BadRequest(new { message = "You have already completed this quiz.", StatusCode = 400 });
            }

            var completedQuiz = new CompletedQuiz
            {
                UserId = user.Id,
                QuizId = quiz.Id,
                User = user,
                Quiz = quiz,
                Score = 0
            };

            completedQuiz.Score = await CalculateScore(selectedAnswerDTO);

            var newCompletedQuiz = await _completedQuizRepo.AddCompletedQuiz(completedQuiz, selectedAnswerDTO);

            return Ok(new { message = $"The quiz was completed successfully with a score of {completedQuiz.Score}", StatusCode = 200 });
        }

        private async Task<int> CalculateScore(List<SelectedAnswerDTO> selectedAnswerDTO)
        {
            int score = 0;

            foreach (var selectedAnswer in selectedAnswerDTO)
            {
                var answer = await _completedQuizRepo.GetAnswerById(selectedAnswer.AnswerId);
                if (answer != null && answer.IsCorrect)
                {
                    var question = await _completedQuizRepo.GetQuestionById(answer.QuestionId);
                    if (question != null && question.QuestionTypeId == 1) // SELECT ONE
                    {
                        score += 100;
                    }
                    else if (question != null && question.QuestionTypeId == 2) // SELECT MANY
                    {
                        var numberOfCorrectAnswers = await _completedQuizRepo.GetNumberOfCorrectAnswers(question.Id);
                        score += 100 / numberOfCorrectAnswers;
                    }
                }
            }

            return score;
        }



        [HttpGet("HasCompletedQuiz")]
        public async Task<ActionResult<bool>> HasCompletedQuiz(int userId, int quizId)
        {
            var existingCompletedQuiz = await _completedQuizRepo.HasCompletedQuiz(userId, quizId);
            if (existingCompletedQuiz)
            {
                return BadRequest(new { message = "You have already completed this quiz.", StatusCode = 400 });
            }

            else return Ok(new { message = "Quiz completed successfully", StatusCode = 200 });
        }
    }
}
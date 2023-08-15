using System.Text.Json.Serialization;
using System.Text.Json;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuizController : ControllerBase
    {
        private readonly IQuizRepository _repo;

        public QuizController(IQuizRepository repo)
        {
            _repo = repo;
        }
        [HttpGet]
        public async Task<ActionResult> GetAllQuizzes()
        {
            var quizzes = await _repo.GetAllQuizzes();

            var json = System.Text.Json.JsonSerializer.Serialize(quizzes, new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.IgnoreCycles,
                IncludeFields = true,
                WriteIndented = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            return Ok(json);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult> GetQuizById(int id)
        {
            var quiz = await _repo.GetQuizById(id);

            if (quiz == null)
            {
                return NotFound($"Id: '{id}' not exist");
            }

            return Ok(quiz);
        }
    }
}
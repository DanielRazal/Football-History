using System.Security.Claims;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        public class PlayerRatingResponseDTO
        {
            public int UserId { get; set; }
            public int PlayerId { get; set; }
            public int Rank { get; set; }
        }

        public class UserPlayerRatingDTO
        {
            public int PlayerId { get; set; }
            public int Rank { get; set; }
        }


        private readonly IPlayerRepository _repo;
        private readonly IUserRepository _repoUser;

        public PlayerController(IPlayerRepository repo, IUserRepository repoUser)
        {
            _repo = repo;
            _repoUser = repoUser;
        }

        [HttpGet()]
        public async Task<ActionResult> GetAllPlayers()
        {
            var users = await _repo.GetAllPlayers();

            var json = JsonConvert.SerializeObject(users, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                Formatting = Formatting.Indented,
                ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new CamelCaseNamingStrategy()
                }
            });
            return Ok(json);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetPlayerById(int id)
        {
            var player = await _repo.GetPlayerById(id);

            if (player == null)
            {
                return NotFound($"Id: '{id}' not exist");
            }

            var json = JsonConvert.SerializeObject(player, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                Formatting = Formatting.Indented,
                ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new CamelCaseNamingStrategy()
                }
            });

            return Ok(json);
        }

        [HttpGet("{firstId}/{secondId}")]
        public async Task<ActionResult> ComparePlayers(int firstId, int secondId)
        {
            var compare = await _repo.ComparePlayers(firstId, secondId);
            var player1 = await _repo.GetPlayerById(firstId);
            var player2 = await _repo.GetPlayerById(secondId);

            if (player1 == null && player2 == null)
            {
                return NotFound($"First '{firstId}' and Second '{secondId}' id's not exist");
            }

            if (player1 == null)
            {
                return NotFound($"First id: '{firstId}' not exist");
            }

            if (player2 == null)
            {
                return NotFound($"Second id: '{secondId}' not exist");
            }
            if (player1 != null && player2 != null)
            {
                if (firstId == secondId)
                {
                    return BadRequest("The same player cannot be compared");
                }
            }

            return Ok(compare);
        }
    }
}

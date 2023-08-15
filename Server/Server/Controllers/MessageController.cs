using System.Security.Claims;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IMessageRepository _messageRepo;
        private readonly IUserRepository _userRepo;


        public MessageController(IMessageRepository messageRepo, IUserRepository userRepo)
        {
            _messageRepo = messageRepo;
            _userRepo = userRepo;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllMessages()
        {
            var messages = await _messageRepo.GetAllMessages();

            var json = System.Text.Json.JsonSerializer.Serialize(messages, new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.IgnoreCycles,
                IncludeFields = true,
                WriteIndented = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            return Ok(json);
        }



        // [HttpPost]
        // public async Task<ActionResult<Message>> AddMessage(MessageDto messageDto, int id)
        // {
        //     var _user = await _userRepo.GetUserById(id);

        //     if (_user == null)
        //     {
        //         return Unauthorized("Not Authorized");
        //     }

        //     var message = new Message
        //     {
        //         Content = messageDto.Content,
        //         UserId = _user.Id,
        //         User = _user,
        //         ReceiverId = 2
        //     };

        //     if (message == null)
        //     {
        //         return BadRequest();
        //     }

        //     var messages = await _messageRepo.AddMessage(message);
        //     var json = System.Text.Json.JsonSerializer.Serialize(messages, new JsonSerializerOptions
        //     {
        //         ReferenceHandler = ReferenceHandler.IgnoreCycles,
        //         IncludeFields = true,
        //         WriteIndented = true,
        //         PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        //     });

        //     return Ok(json);

        // }

        [HttpPost("{senderId}/{receiverId}")]
        public async Task<ActionResult<Message>> AddMessage(int senderId, int receiverId, [FromBody] MessageDto messageDto)
        {
            var _sender = await _userRepo.GetUserById(senderId);
            var _receiver = await _userRepo.GetUserById(receiverId);

            if (_sender == null || _receiver == null)
            {
                return Unauthorized("Not Authorized");
            }

            var message = new Message
            {
                Content = messageDto.Content,
                UserId = _sender.Id,
                User = _sender,
                ReceiverId = _receiver.Id
            };

            if (message == null)
            {
                return BadRequest();
            }

            var addedMessage = await _messageRepo.AddMessage(message);

            var json = System.Text.Json.JsonSerializer.Serialize(addedMessage, new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.IgnoreCycles,
                IncludeFields = true,
                WriteIndented = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            return Ok(json);
        }

    }
}

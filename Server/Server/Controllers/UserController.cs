using System.Text.Json;
using System.Text.Json.Serialization;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _repo;
        private readonly IJwtTokenGenerator _token;
        private readonly IUploadPhotos _uploads;
        private readonly IEmailSender _emailSender;
        private readonly IConfiguration _config;

        public UserController(IUserRepository repo, IJwtTokenGenerator token,
         IUploadPhotos uploads, IEmailSender emailSender, IConfiguration config)
        {
            _repo = repo;
            _token = token;
            _uploads = uploads;
            _emailSender = emailSender;
            _config = config;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllUsers()
        {
            var users = await _repo.GetAllUsers();
            var json = System.Text.Json.JsonSerializer.Serialize(users, new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.IgnoreCycles,
                IncludeFields = true,
                WriteIndented = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            return Ok(json);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult> GetUserById(int id)
        {
            var _user = await _repo.GetUserById(id);

            if (_user == null)
            {
                return NotFound($"Id: '{id}' not exist");
            }

            var json = System.Text.Json.JsonSerializer.Serialize(_user, new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.IgnoreCycles,
                IncludeFields = true,
                WriteIndented = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            return Ok(json);
        }

        [HttpPost("registration")]
        public async Task<ActionResult<User>> AddUser([FromForm] UserViewModel viewModel)
        {
            string defaultPhotoUrl = "https://www.kindpng.com/picc/m/24-248253_user-profile-default-image-png-clipart-png-download.png";

            var _user = new User
            {
                FirstName = viewModel.UserDTO.FirstName,
                LastName = viewModel.UserDTO.LastName,
                UserName = viewModel.UserDTO.UserName,
                Password = viewModel.UserDTO.Password,
                Email = viewModel.UserDTO.Email,
            };

            if (viewModel.Photo != null && !string.IsNullOrEmpty(viewModel.Photo.FileName))
            {
                _user = await _uploads.UploadFile(_user, viewModel.Photo!);
            }
            else
            {
                _user.PhotoUrl = defaultPhotoUrl;
            }

            bool userNameExists = await _repo.UserNameExists(_user.UserName);
            bool emailExists = await _repo.EmailExists(_user.Email);

            if (userNameExists && emailExists)
            {
                return Unauthorized($"Username {_user.UserName} and Email {_user.Email} already exist");
            }
            else if (userNameExists)
            {
                return Unauthorized($"Username {_user.UserName} already exists");
            }
            else if (emailExists)
            {
                return Unauthorized($"Email {_user.Email} already exists");
            }

            var newUser = await _repo.AddUser(_user);

            await _emailSender.SendEmail(newUser, _config["SendGrid:SubjectForRegister"]!,
                _config["SendGrid:ContentForRegister"]!);

            return Ok(new { message = "You have successfully registered", StatusCode = 200 });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser(int id)
        {
            var _user = await _repo.DeleteUser(id);

            if (_user == null)
            {
                return NotFound($"Id: '{id}' does not exist");
            }

            var filePath = Path.GetFileName(_user.PhotoUrl);
            await _uploads.DeleteFile(filePath!);

            return Ok(_user);
        }


        [HttpPost("login")]
        public async Task<ActionResult> Login(Login login)
        {
            var user = await _repo.GetUserByUserName(login.UserName);

            if (user == null)
            {
                return Unauthorized("Invalid username or password");

            }

            var _user = new User
            {
                UserName = login.UserName,
                Password = login.Password,
                PhotoUrl = user.PhotoUrl,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Id = user.Id
            };

            var isAuthenticated = await _repo.Login(_user);

            if (isAuthenticated)
            {
                var token = await _token.GenerateToken(_user);
                return Ok(new { token, _user, message = "Login successful", StatusCode = 200 });
            }
            else
            {
                return Unauthorized("Invalid username or password");
            }
        }

        [HttpPost("details")]
        public async Task<ActionResult<object>> SendDetailsToEmail(string email)
        {
            var emailExists = await _repo.EmailExists(email);

            if (!emailExists)
            {
                return BadRequest($"Email {email} does not exist");
            }

            var user = await _repo.GetUserByEmail(email);


            await _emailSender.SendEmail(user, _config["SendGrid:SubjectForForgotPassword"]!,
                string.Format(_config["SendGrid:ContentForForgotPassword"]!, user.UserName, user.Password));

            return Ok(new { message = "Your Username and Password have been sent to your email", StatusCode = 200 });
        }


        [HttpGet("{firstId}/{secondId}")]
        public async Task<IActionResult> Get2UsersByIds(int firstId, int secondId)
        {
            try
            {
                var (user1, user2) = await _repo.Get2IdsByUsers(firstId, secondId);

                if (user1 == null || user2 == null)
                {
                    return NotFound("One or both users not found.");
                }

                var json = System.Text.Json.JsonSerializer.Serialize((user1, user2), new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.IgnoreCycles,
                    IncludeFields = true,
                    WriteIndented = true,
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                });

                return Ok(json);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }

}
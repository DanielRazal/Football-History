namespace Server.Repositories.Outside
{
    public class UserRepository : IUserRepository
    {
        private readonly ServerContext _context;
        public UserRepository(ServerContext context)
        {
            _context = context;
        }

        public async Task<User> AddUser(User user)
        {
            if (!await _context.Users.AnyAsync())
            {
                _context.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('User', RESEED, 1)");
            }
            user.Id = 0;
            await _context.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }


        public async Task<User> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                var completedQuizzes = await _context.CompletedQuizzes
                    .Include(cq => cq.SelectedAnswers)
                    .Where(cq => cq.UserId == id)
                    .ToListAsync();

                _context.CompletedQuizzes.RemoveRange(completedQuizzes);
                _context.Users.Remove(user);

                await _context.SaveChangesAsync();
                return user;
            }
            else
            {
                return null!;
            }
        }



        public async Task<bool> EmailExists(string email)
        {
            var _user = await _context.Users.AnyAsync(x => x.Email == email.ToLower());
            return _user;
        }

        public async Task<List<User>> GetAllUsers()
        {
            return await _context.Users
                .Include(u => u.Messages)
                .ToListAsync();
        }

        public async Task<User> GetUserByEmail(string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user != null)
            {
                return user!;
            }
            else return null!;
        }

        public async Task<User> GetUserById(int id)
        {
            var user = await _context.Users
                .Include(u => u.Messages).FirstOrDefaultAsync(u => u.Id == id);
            if (user != null)
                return user;
            else return null!;
        }

        public async Task<User> GetUserByUserName(string userName)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == userName);

            if (user != null)
            {
                return user!;
            }
            else return null!;
        }

        public async Task<bool> Login(User user)
        {
            var _user = await _context.Users
                .FirstOrDefaultAsync(x => x.UserName == user.UserName && x.Password == user.Password);
            if (_user != null)
                return true;
            else
                return false;
        }

        public async Task<bool> UserNameExists(string userName)
        {
            var _user = await _context.Users.AnyAsync(x => x.UserName == userName.ToLower());
            return _user;
        }

        // public async Task<(User, User)> Get2IdsByUsers(int firstId, int secondId)
        // {
        //     if (firstId == secondId)
        //     {
        //         throw new ArgumentException("The two user IDs should be different.");
        //     }

        //     var user1 = await _context.Users.Include(u => u.Messages).FirstOrDefaultAsync(u => u.Id == firstId);
        //     var user2 = await _context.Users.Include(u => u.Messages).FirstOrDefaultAsync(u => u.Id == secondId);

        //     if (user1 == null || user2 == null)
        //     {
        //         throw new Exception("One or both users not found.");
        //     }

        //     return (user1, user2);
        // }

        public async Task<(User, User)> Get2IdsByUsers(int firstId, int secondId)
        {
            if (firstId == secondId)
            {
                throw new ArgumentException("The two user IDs should be different.");
            }

            var user1 = await _context.Users
                .Include(u => u.Messages)
                .FirstOrDefaultAsync(u => u.Id == firstId);

            var user2 = await _context.Users
                .Include(u => u.Messages)
                .FirstOrDefaultAsync(u => u.Id == secondId);

            if (user1 == null || user2 == null)
            {
                throw new Exception("One or both users not found.");
            }

            user1.Messages = user1.Messages.Where(m => m.UserId == firstId && m.ReceiverId == secondId || m.UserId == secondId && m.ReceiverId == firstId).ToList();
            user2.Messages = user2.Messages.Where(m => m.UserId == firstId && m.ReceiverId == secondId || m.UserId == secondId && m.ReceiverId == firstId).ToList();

            return (user1, user2);
        }


    }
}
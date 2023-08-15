namespace Server.Repositories.Inside
{
    public class MessageRepository : IMessageRepository
    {
        private readonly ServerContext _context;
        public MessageRepository(ServerContext context)
        {
            _context = context;
        }

        // public async Task<Message> AddMessage(Message message)
        // {
        //     message.Id = 0;
        //     await _context.Messages.AddAsync(message);
        //     await _context.SaveChangesAsync();

        //     return message;
        // }

        public async Task<Message> AddMessage(Message message)
        {
            var _message = new Message
            {
                Id = message.Id,
                Content = message.Content,
                UserId = message.UserId,
                ReceiverId = message.ReceiverId
            };

            await _context.Messages.AddAsync(_message);
            await _context.SaveChangesAsync();

            return _message;
        }


        public async Task<List<Message>> GetAllMessages()
        {
            return await _context.Messages.Include(m => m.User).ToListAsync();
        }


    }
}

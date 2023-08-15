namespace Server.Models.Inside
{
    public class Message
    {
        public int Id { get; set; }
        public string Content { get; set; } = string.Empty;
        // SENDER
        public int UserId { get; set; }
        // Receiverd
        public int ReceiverId { get; set; }
        public virtual User User { get; set; } = null!;
    }
}
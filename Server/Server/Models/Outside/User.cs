namespace Server.Models.Outside
{
    [Table("User")]
    public class User
    {

        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhotoUrl { get; set; } = string.Empty;
        public virtual ICollection<Message> Messages { get; set; } = null!;
    }
}

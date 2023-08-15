namespace Server.Models.Inside
{
    public class Quiz
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public virtual ICollection<Question> Questions { get; set; } = null!;
    }
}

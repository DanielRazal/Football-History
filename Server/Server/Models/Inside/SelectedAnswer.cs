namespace Server.Models.Inside
{
    public class SelectedAnswer
    {
        public int Id { get; set; }
        public int AnswerId { get; set; }
        public Answer Answer { get; set; } = null!;
    }

}
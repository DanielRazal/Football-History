namespace Server.Models.Inside
{
    public class CompletedQuiz
    {
        public int Id { get; set; }
        public int Score { get; set; }
        public int UserId { get; set; }
        public int QuizId { get; set; }
        public User User { get; set; } = null!;
        public Quiz Quiz { get; set; } = null!;
        public virtual ICollection<SelectedAnswer> SelectedAnswers { get; set; } = null!;
    }
}

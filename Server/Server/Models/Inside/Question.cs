namespace Server.Models.Inside
{
    public class Question
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public int QuestionTypeId { get; set; }
        public int QuizId { get; set; }
        public virtual QuestionType QuestionType { get; set; } = null!;
        public virtual ICollection<Answer> Answers { get; set; } = null!;
        public virtual Quiz Quiz { get; set; } = null!;

    }
}

namespace QuizMVC.Models
{
    public class Quiz
    {
        public int Id { get; set; }
        public string Question { get; set; } = null!;
        public string AAnswer { get; set; } = null!;
        public string BAnswer { get; set; } = null!;
        public string CAnswer { get; set; } = null!;
        public string DAnswer { get; set; } = null!;
        public string RightAnswer { get; set; } = null!;
    }
}

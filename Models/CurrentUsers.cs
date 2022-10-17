namespace QuizMVC.Models
{
    public static class CurrentUsers
    {
        public static Dictionary<string, Quiz[]> quizzSets { get; private set; } = new();
        public static Dictionary<string, string[]> Answers { get; private set; } = new();
        public static void cleanUserInfo(string? userId, HttpContext context)
        {
            if (userId != null)
            {
                quizzSets.Remove(userId);
                Answers.Remove(userId);
                foreach (var cookie in context.Request.Cookies)
                {
                    context.Response.Cookies.Delete(cookie.Key);
                }
            }
        }
    }
}
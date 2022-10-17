using Microsoft.EntityFrameworkCore;
using QuizMVC.Models;


namespace QuizMVC.Data
{
    public class TestContext : DbContext
    {
        public DbSet<Quiz> Quizzes { get; set; } = null!;
        public TestContext(DbContextOptions<TestContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}

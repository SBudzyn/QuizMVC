using QuizMVC.Models;
using Microsoft.AspNetCore.Mvc;
using QuizMVC.Data;


namespace QuizMVC.Controllers
{
    public class QuizController : Controller
    {

        private readonly TestContext _context;
        public QuizController(TestContext context)
        {
            _context = context; 
        }
        
        public IActionResult Index()
        {
            
            string? currentId = HttpContext.Request.Cookies["userId"];
            CurrentUsers.cleanUserInfo(currentId, HttpContext);
            
            return View();
        }
        [HttpPost]
        public IActionResult Blitz(int id, string answers, string transitTo)
        {
            if (answers != null)
            {
                CurrentUsers.Answers[HttpContext.Request.Cookies["userId"]!][id] = answers;
            }
            return Redirect($"~/Quizz/Blitz/{transitTo}");
        }
        public IActionResult Exit()
        {
			string? currentId = HttpContext.Request.Cookies["userId"];
            CurrentUsers.cleanUserInfo(currentId, HttpContext);
            return Redirect("~/Quizz");
        }
        public IActionResult BlitzResult()
        {
            return View();
        }
        public IActionResult Blitz(int id)
        {
            Random rnd = new Random();
            
            if (!HttpContext.Request.Cookies.ContainsKey("userId"))
            {
                int userId = rnd.Next(0, 30_000);
                HttpContext.Response.Cookies.Append("userId", userId.ToString());
                List<Quiz> quizzSet = new();
                var contextQuizzes = _context.Quizzes.ToList();
                while (quizzSet.Count() != 10)
                {
                    var quizz = contextQuizzes[rnd.Next(0, _context.Quizzes.Count())];

                    if (!quizzSet.Any(item => item.Id == quizz.Id))
                    {
                        quizzSet.Add(quizz);
                    }

                }
                CurrentUsers.quizzSets[userId.ToString()] = quizzSet.ToArray();
                CurrentUsers.Answers[userId.ToString()] = new string[10];
                
                return Redirect("~/Quizz/Blitz");
            }
            else
            {
                string cookieId = HttpContext.Request.Cookies["userId"]!;
                
                if (!CurrentUsers.quizzSets.ContainsKey(cookieId))
                {
                    
                    //Initialize data (? bring to separate method)
                    List<Quiz> quizzSet = new();
                    var contextQuizzes = _context.Quizzes.ToList();
                    while (quizzSet.Count() != 10)
                    {
                        var quizz = contextQuizzes[rnd.Next(0, _context.Quizzes.Count())];

                        if (!quizzSet.Any(item => item.Id == quizz.Id))
                        {
                            quizzSet.Add(quizz);
                        }

                    }
                    CurrentUsers.quizzSets[HttpContext.Request.Cookies["userId"]!.ToString()] = quizzSet.ToArray();

                    
                }
                
                return View(CurrentUsers.quizzSets[HttpContext.Request.Cookies["userId"]!.ToString()][id]);
            }
            
            
        }
        
    }
}
    
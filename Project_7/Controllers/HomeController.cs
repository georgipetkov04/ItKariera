using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Project_7.Data;
using Project_7.Models;
using System.Diagnostics;

namespace Project_7.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context, UserManager<AppUser> userManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            int usersCount = _userManager.Users.Count();
            int toDoListsCount = _context.ToDoList.Count();
            int toDoItemsCount = _context.ToDoItem.Count();
            ViewData["UsersCount"] = usersCount;
            ViewData["ToDoListsCount"] = toDoListsCount;
            ViewData["ToDoItemsCount"] = toDoItemsCount;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
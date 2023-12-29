using HomeApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace HomeApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
		public IActionResult About()
		{
			return View();
		}
		public IActionResult blog()
		{
			return View();
		}
		public IActionResult blog_detail()
		{
			return View();
		}
		public IActionResult services()
		{
			return View();
		}
		public IActionResult team()
		{
			return View();
		}
		public IActionResult team_detail()
		{
			return View();
		}
		public IActionResult contact()
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
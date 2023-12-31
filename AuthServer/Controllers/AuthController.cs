using Microsoft.AspNetCore.Mvc;

namespace AuthServer.Controllers
{
	public class AuthController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}

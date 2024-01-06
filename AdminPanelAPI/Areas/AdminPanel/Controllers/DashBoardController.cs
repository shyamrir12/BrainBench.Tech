using Microsoft.AspNetCore.Mvc;

namespace AdminPanelAPI.Areas.AdminPanel.Controllers
{
	public class DashBoardController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}

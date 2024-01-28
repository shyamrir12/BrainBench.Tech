using Microsoft.AspNetCore.Mvc;

namespace IntigrationWeb.Controllers
{
    public class PaymentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

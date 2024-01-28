using Microsoft.AspNetCore.Mvc;

namespace IntigrationWeb.Areas.Payment.Controllers
{
    public class PaymentRequestController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

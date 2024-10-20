using IntegrationModels;
using IntigrationWeb.Models;
using IntigrationWeb.Models.UserAndErrorService;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace IntigrationWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        LogEntry objLogEntry = new LogEntry();
        private readonly IUserAndErrorSubscriber _userAndErrorSubscriber;
        public HomeController(ILogger<HomeController> logger, IUserAndErrorSubscriber userAndErrorSubscriber)
        {
            _logger = logger;
            _userAndErrorSubscriber= userAndErrorSubscriber;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            // Retrieve the exception Details
            var exceptionHandlerPathFeature =HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            objLogEntry.Action = exceptionHandlerPathFeature.GetType().FullName;
            objLogEntry.Controller = exceptionHandlerPathFeature.Path;
            objLogEntry.ReturnType = "Return";
            objLogEntry.StackTrace = exceptionHandlerPathFeature.Error.StackTrace;
            objLogEntry.ErrorMessage = exceptionHandlerPathFeature.Error.Message;
            _userAndErrorSubscriber.AddExceptionData(objLogEntry);
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
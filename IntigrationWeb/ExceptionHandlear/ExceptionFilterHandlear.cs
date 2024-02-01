using IntegrationModels;
using IntigrationWeb.Models.UserAndErrorService;
using Microsoft.AspNetCore.Mvc.Filters;

namespace IntigrationWeb.ExceptionHandlear
{
    public class ExceptionFilterHandlear : ExceptionFilterAttribute
    {
        LogEntry objLogEntry = new LogEntry();
        private readonly IUserAndErrorSubscriber _userAndErrorSubscriber;
        private readonly ILogger<ExceptionFilterHandlear> _logger;
        public ExceptionFilterHandlear(IUserAndErrorSubscriber userAndErrorSubscriber, ILogger<ExceptionFilterHandlear> logger)
        {
            _userAndErrorSubscriber = userAndErrorSubscriber;
            _logger = logger;
        }
        public override void OnException(ExceptionContext context)
        {
            try
            {
               
                objLogEntry.Action = context.ActionDescriptor.DisplayName;
                objLogEntry.Controller = context.HttpContext.Request.Path;
                objLogEntry.ReturnType = "Integration";
                objLogEntry.StackTrace = context.Exception.StackTrace;
                objLogEntry.ErrorMessage = context.Exception.Message;

                _userAndErrorSubscriber.AddExceptionData(objLogEntry);

            }
            catch (Exception ex)
            {

                _logger.LogError(ex, ex.Message);
            }
        }
    }
}

using IntegrationApi.Data.ExceptionDataServices;
using IntegrationModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IntegrationApi.Controllers
{
	[Route("api/{controller}/{action}")]
	[ApiController]
	public class ExceptionDataController : ControllerBase
	{
		private readonly IExceptionDataProvider _objIExceptionProvider;
		
		public ExceptionDataController(IExceptionDataProvider objIExceptionProvider)
		{
			_objIExceptionProvider = objIExceptionProvider;
		}
	
		public string AddException(LogEntry objLogEntry)
		{
			return _objIExceptionProvider.ErrorList(objLogEntry);
		}
		
	}
}

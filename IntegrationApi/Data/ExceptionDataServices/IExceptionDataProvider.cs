using IntegrationModels;
using System;

namespace IntegrationApi.Data.ExceptionDataServices
{
	public interface IExceptionDataProvider
	{
		string ErrorList(LogEntry objLogEntry);
		string ErrorLoger(Exception exception,HttpContext context);

	}
}

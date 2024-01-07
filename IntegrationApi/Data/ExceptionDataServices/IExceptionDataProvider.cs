using IntegrationModels;

namespace IntegrationApi.Data.ExceptionDataServices
{
	public interface IExceptionDataProvider
	{
		string ErrorList(LogEntry objLogEntry);
	}
}

using AdminPanelModels;

namespace AdminPanelAPI.Data.ExceptionDataServices
{
    public interface IExceptionDataProvider
    {
        string ErrorList(LogEntry objLogEntry);
    }
}

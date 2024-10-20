using IntegrationModels;
using LoginModels;

namespace IntegrationApi.Data.NewDSCServices
{
    public interface INewDscProvider
    {
        Task<MessageEF> SaveDSCFilePath(NewDSCRequest objParams);
    }
}

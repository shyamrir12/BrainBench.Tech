using IntegrationModels;
using LoginModels;

namespace IntigrationWeb.Areas.NewDSC.Data
{
    public interface INewDSCSubscriber
    {
        MessageEF SaveDSCFilePath(NewDSCRequest objParams);
    }
}

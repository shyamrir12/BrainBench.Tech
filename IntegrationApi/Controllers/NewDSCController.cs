using IntegrationApi.Data.NewDSCServices;
using IntegrationModels;
using LoginModels;
using Microsoft.AspNetCore.Mvc;

namespace IntegrationApi.Controllers
{
    [Route("api/{controller}/{action}")]
    [ApiController]
    public class NewDSCController : Controller
    {
        private readonly INewDscProvider iNewDscProvider;

        public NewDSCController(INewDscProvider _iNewDscProvider)
        {
            iNewDscProvider = _iNewDscProvider;
        }
        [HttpPost]
        public async Task<MessageEF> SaveDSCFilePath(NewDSCRequest newDSC)
        {
            return await iNewDscProvider.SaveDSCFilePath(newDSC);
        }
    }
}

using AdminPanelAPI.Areas.AdminPanel.Data.Adduser_rightsServices;
using AdminPanelModels;
using LoginModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AdminPanelAPI.Areas.AdminPanel.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class Adduser_rightsController : ControllerBase
	{
		private readonly IAdduser_rightsProvider _provider;
		public Adduser_rightsController(IAdduser_rightsProvider provider)
		{
			_provider = provider;
		}
        public Task<Result<List<MenuItem>>> GetMenuListFormate(CommanRequest model)
        {
            return _provider.GetMenuListFormate(model);
        }
        public Task<Result<List<MenuItem>>> GetMenuListByRole(CommanRequest model)
        {
            return _provider.GetMenuListByRole(model);
        }
        public Task<Result<MessageEF>> UpdateMenuByID(CommanRequest model)
        {
            return _provider.UpdateMenuByID(model);
        }
      
    }
}

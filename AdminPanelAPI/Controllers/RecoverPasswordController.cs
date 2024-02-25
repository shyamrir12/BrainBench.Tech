using AdminPanelAPI.Data.RecoverPasswordServices;
using AdminPanelModels;
using LoginModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AdminPanelAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class RecoverPasswordController : ControllerBase
	{
		private readonly IRecoverPasswordProvider _irecoverPasswordProvider;
		public RecoverPasswordController(IRecoverPasswordProvider irecoverPasswordProvider)
		{
            _irecoverPasswordProvider=irecoverPasswordProvider;

        }
        public Task<Result<MessageEF>> GetRecoverPassword(RecoverPassword model)
        {
            return _irecoverPasswordProvider.GetRecoverPassword(model);
        }
    }
}

using Microsoft.AspNetCore.Mvc;

namespace AdminPanelAPI.Controllers
{
	[Route("[controller]")]
	[ApiController]
	public class ValuesController : ControllerBase
	{
		[HttpGet]
		public ActionResult<IEnumerable<string>> Get()
		{

			return new string[] { "value1", "value2" };
		}
	}
}

using IntigrationWeb.Models.UserAndErrorService;
using LoginModels;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;
using System.Globalization;
using System.Security.Claims;
using System.Text;

namespace IntigrationWeb.IntegrationWeb
{
    public class SessionActionFilter : IActionFilter
    {
        private readonly IUserAndErrorSubscriber _objIExceptionProvider;     
      
        private readonly IConfiguration _configuration;
        double seconds = 0;

        public SessionActionFilter(IUserAndErrorSubscriber objIExceptionProvider, IConfiguration configuration)
        {
            _configuration = configuration;
            _objIExceptionProvider = objIExceptionProvider;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            var claimsIdentity = context.HttpContext.User.Identity as ClaimsIdentity;
            //#region Token
            //string jwt = claimsIdentity.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Authentication)?.Value;
            //if (jwt != null)
            //{
            //    //getting the secret key
            //    string secretKey = _configuration["Audience:Secret"];
            //    var key = Encoding.ASCII.GetBytes(secretKey);
            //    //preparing the validation parameters
            //    var tokenValidationParameters = new TokenValidationParameters
            //    {
            //        ValidateIssuerSigningKey = true,
            //        IssuerSigningKey = new SymmetricSecurityKey(key),
            //        ValidateIssuer = false,
            //        ValidateAudience = false
            //    };
            //    var tokenHandler = new JwtSecurityTokenHandler();

            //    var jwtSecurityToken = tokenHandler.ReadJwtToken(jwt);
            //    DateTime validTo = jwtSecurityToken.Payload.ValidTo;
            //    DateTime currentUTC = DateTime.UtcNow;
            //    var diffInSeconds = (validTo - currentUTC).TotalSeconds;
            //    seconds = diffInSeconds;

            //}
            //#endregion
            var controllerActionDescriptor = context.ActionDescriptor as ControllerActionDescriptor;

            // Check if the attribute exists on the action method
            if (controllerActionDescriptor.MethodInfo?.GetCustomAttributes(inherit: true)?.Any(a => a.GetType().Equals(typeof(PerimeterDescriptionAttribute))) ?? false)
            {
                //
                #region decrypt encrypted url
                var dataProtectionProvider = DataProtectionProvider.Create(new DirectoryInfo(_configuration.GetValue<string>("PaymentKeyList:EncryptionPath")));
                var protector = dataProtectionProvider.CreateProtector("KhanijEncryptDecryptQuery.QueryStrings");

                Dictionary<string, object> decryptedParameters = new Dictionary<string, object>();
                if (!string.IsNullOrEmpty(context.HttpContext.Request.Query["q"].ToString()))
                {
                    string decrptedString = protector.Unprotect(context.HttpContext.Request.Query["q"].ToString());
                    string[] getRandom = decrptedString.Split('|');

                    var format = new CultureInfo("en-GB");
                    var dateCheck = Convert.ToDateTime(getRandom[2], format);

                    TimeSpan diff = Convert.ToDateTime(DateTime.Now, format) - dateCheck;

                    /* For Development it is been commented */
                    if (diff.Minutes > 30)
                    {
                        context.Result = new RedirectToRouteResult(new RouteValueDictionary(new { action = "Error", controller = "Error" }));
                    }

                    string[] paramsArrs = getRandom[1].Split('?');

                    for (int i = 0; i < paramsArrs.Length; i++)
                    {
                        string[] paramArr = paramsArrs[i].Split('=');
                        decryptedParameters.Add(paramArr[0], Convert.ToString(paramArr[1]));
                    }
                }

                if (decryptedParameters.Count > 0)
                {
                    for (int i = 0; i < decryptedParameters.Count; i++)
                    {

                        context.HttpContext.Items[decryptedParameters.Keys.ElementAt(i)] = decryptedParameters.Values.ElementAt(i);
                       // context.ActionArguments[decryptedParameters.Keys.ElementAt(i)] = decryptedParameters.Values.ElementAt(i);
                    }
                }
                #endregion
            }
            //Check if the attribute exists on the action method
            if (controllerActionDescriptor.MethodInfo?.GetCustomAttributes(inherit: true)?.Any(a => a.GetType().Equals(typeof(SkipPerimeterDescriptionAttribute))) ?? false)
            {

            }
            //else if (claimsIdentity.IsAuthenticated && seconds > 0)
            //{

            //    UserLoginSession objUserLoginSession = context.HttpContext.Session.Get<UserLoginSession>(KeyHelper.UserKey);
            //    if (objUserLoginSession == null)
            //    {
            //        LoginEF ObjloginEF = new LoginEF();
            //        ObjloginEF.UserID = Convert.ToInt32(claimsIdentity.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
            //        ObjloginEF.UserName = claimsIdentity.Claims.FirstOrDefault(c => c.Type == ClaimTypes.UserData)?.Value;
            //        UserLoginSession objUserLoginSession1 = _objIExceptionProvider.LoginUser(ObjloginEF);
            //        if (objUserLoginSession1.UserName != null)
            //        {
            //            List<MenuEF> Listmenu = new List<MenuEF>();
            //            objUserLoginSession1.UserLoginId = Convert.ToInt32(claimsIdentity.Claims.FirstOrDefault(c => c.Type == ClaimTypes.PrimarySid).Value);
            //            menuonput objmenu = new menuonput();
            //            objmenu.UserID = Convert.ToInt32(objUserLoginSession1.UserId);
            //            objmenu.MineralId = Convert.ToInt32(objUserLoginSession1.MineralId);
            //            objmenu.MineralName = objUserLoginSession1.MineralName;
            //            Listmenu = _objIExceptionProvider.MenuList(objmenu);
            //            objUserLoginSession1.Listmenu = Listmenu;
            //        }
            //        context.HttpContext.Session.Set<UserLoginSession>(KeyHelper.UserKey, objUserLoginSession1);

            //    }
            //    else if (Convert.ToInt32(claimsIdentity.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value) != objUserLoginSession.UserId)
            //    {
            //        LoginEF ObjloginEF = new LoginEF();
            //        ObjloginEF.UserID = Convert.ToInt32(claimsIdentity.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
            //        ObjloginEF.UserName = claimsIdentity.Claims.FirstOrDefault(c => c.Type == ClaimTypes.UserData)?.Value;
            //        UserLoginSession objUserLoginSession1 = _objIExceptionProvider.LoginUser(ObjloginEF);
            //        if (objUserLoginSession1.UserName != null)
            //        {
            //            List<MenuEF> Listmenu = new List<MenuEF>();
            //            objUserLoginSession1.UserLoginId = Convert.ToInt32(claimsIdentity.Claims.FirstOrDefault(c => c.Type == ClaimTypes.PrimarySid).Value);
            //            menuonput objmenu = new menuonput();
            //            objmenu.UserID = Convert.ToInt32(objUserLoginSession1.UserId);
            //            objmenu.MineralId = Convert.ToInt32(objUserLoginSession1.MineralId);
            //            objmenu.MineralName = objUserLoginSession1.MineralName;
            //            Listmenu = _objIExceptionProvider.MenuList(objmenu);
            //            objUserLoginSession1.Listmenu = Listmenu;
            //        }
            //        context.HttpContext.Session.Set<UserLoginSession>(KeyHelper.UserKey, objUserLoginSession1);

            //    }

            //}
            //else
            //{
            //    context.HttpContext.Session.Clear();
            //    context.Result = new RedirectResult(_objKeyList.Value.LoginUrl);
            //}
           
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            throw new NotImplementedException();
        }
    }
    public class PerimeterDescriptionAttribute : Attribute { }
    public class SkipPerimeterDescriptionAttribute : Attribute { }

}

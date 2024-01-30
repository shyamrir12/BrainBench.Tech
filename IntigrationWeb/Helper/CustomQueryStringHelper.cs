using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Options;
using System.Globalization;

namespace IntigrationWeb.Helper
{
    public static class CustomQueryStringHelper
    {
        private static  IConfiguration _configuration;
        public static void CustomQueryStringHelperEncryptPath(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public static string EncryptString(string areas, string actionName, string controllerName, object routeValues)
        {

            var dataProtectionProvider = DataProtectionProvider.Create(new DirectoryInfo(_configuration.GetValue<string>("PaymentKeyList:EncryptionPath")));
            var protector = dataProtectionProvider.CreateProtector("KhanijEncryptDecryptQuery.QueryStrings");

            string mainString;
            string queryString = string.Empty;
            var rvd = new RouteValueDictionary(routeValues);
            IList<string> strings = new List<string>();

            if (routeValues != null)
            {
                for (int i = 0; i < rvd.Keys.Count; i++)
                {
                    strings.Add(rvd.Keys.ElementAt(i) + "=" + rvd.Values.ElementAt(i));
                }
                queryString += string.Join("?", strings);

                var format = new CultureInfo("en-GB");
                var random = Guid.NewGuid().ToString("N").Substring(0, 10).ToUpper();
                var values = string.Join("|", random, queryString, DateTime.Now.ToString(format));

                if (String.IsNullOrEmpty(areas))
                {
                    mainString = $"/{controllerName}/{actionName}?q={protector.Protect(values)}";
                }
                else
                {
                    mainString = $"/{areas}/{controllerName}/{actionName}?q={protector.Protect(values)}";
                }

            }
            else
            {
                if (String.IsNullOrEmpty(areas))
                {
                    mainString = $"/{controllerName}/{actionName}";
                }
                else
                {
                    mainString = $"/{areas}/{controllerName}/{actionName}";
                }
            }

            return mainString;
        }
    }
}

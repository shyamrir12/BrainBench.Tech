using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Headers;
using System.Security.Claims;

namespace AdminPanelApp.Handlers
{
	public class CustomAuthorizationHandler : DelegatingHandler
	{
      
        public ILocalStorageService _localStorageService { get; set; }
        private readonly NavigationManager _navigationManager;
        private readonly AuthenticationStateProvider _authenticationStateProvider;
      
        double seconds = 0;
        int secondsInt = 0;
        public CustomAuthorizationHandler(ILocalStorageService localStorageService, NavigationManager navigationManager, AuthenticationStateProvider authenticationStateProvider)
		{
			//injecting local storage service
			_localStorageService = localStorageService;
            _authenticationStateProvider = authenticationStateProvider;
            _navigationManager = navigationManager;
        }
		protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
		{
			//getting token from the localstorage
			var jwtToken = await _localStorageService.GetItemAsync<string>("jwt_token");

			//adding the token in authorization header
			if (jwtToken != null)
			{
                var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
                var user = authState.User;
                if (user.Identity.IsAuthenticated)
                {
                    var identity = user.Identity as ClaimsIdentity;
                    var validToClaim = identity?.FindFirst(ClaimTypes.Expiration);
                    DateTime validTo = DateTime.Parse(validToClaim.Value);
                    DateTime currentUTC = DateTime.UtcNow;
                    var diffInSeconds = (validTo - currentUTC).TotalSeconds;
                    seconds = diffInSeconds;
                    secondsInt = (int)seconds;
                    // Get the value of the NameIdentifier claim
                    if (secondsInt > 0)
                    {
                       
                        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);
                    }
                    else
                    {
                        _navigationManager.NavigateTo("/", true);
                    }


                }
                else
                {
                    _navigationManager.NavigateTo("/", true);
                }             

            }
			

			//sending the request
			return await base.SendAsync(request, cancellationToken);
		}
	}
}

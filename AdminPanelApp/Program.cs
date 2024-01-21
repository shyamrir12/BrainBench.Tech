using AdminPanelApp;
using AdminPanelApp.Data.LoginServices;
using AdminPanelApp.Data.RegisterServices;
using AdminPanelApp.Data.UserSessionIndexDB;
using AdminPanelApp.Handlers;
using BlazorDB;
using Blazored.LocalStorage;
using Blazored.Toast;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;


var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddOptions();
builder.Services.AddAuthorizationCore();

var url = builder.Configuration.GetValue<string>("KeyList:WebApiurl");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(url) });



AddHttpClients(builder,url);

builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
builder.Services.AddBlazorDB(options =>
{
    options.Name = "BrainBanchDB";
    options.Version = 1;
    options.StoreSchemas = new List<StoreSchema>()
    {
        new StoreSchema()
        {
            Name = "Customer",      // Name of entity
            PrimaryKey = "Id",      // Primary Key of entity
            PrimaryKeyAuto = true,  // Whether or not the Primary key is generated
            Indexes = new List<string> { "Id" }
        }
    };
});
builder.Services.AddScoped<UserDb>();
builder.Services.AddBlazoredLocalStorage();

builder.Services.AddTransient<CustomAuthorizationHandler>();
builder.Services.AddBlazoredToast();
builder.Services.AddMudServices();

await builder.Build().RunAsync();

 void AddHttpClients(WebAssemblyHostBuilder builder,string baseurl)
{


	//authentication http clients RegisterUserServiceClient
	builder.Services.AddHttpClient<IRegisterSubscriber, RegisterSubscriber>
		("RegisterViewModelClient", client => client.BaseAddress = new Uri(baseurl));
	builder.Services.AddHttpClient<ILoginProvider, LoginProvider>
		("LoginViewModelClient", client => client.BaseAddress = new Uri(baseurl));


	//	builder.Services.AddHttpClient<IMedicineServiceClient, MedicineServiceClient>
	//	  ("MedicineServiceClient", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)).AddHttpMessageHandler<CustomAuthorizationHandler>(); ;

}




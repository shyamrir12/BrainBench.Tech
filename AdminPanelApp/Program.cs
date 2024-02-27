using AdminPanelApp;
using AdminPanelApp.Data.AdminPanelServices.Adduser_rightsServices;
using AdminPanelApp.Data.AdminPanelServices.AddUserServices;
using AdminPanelApp.Data.AdminPanelServices.ApplicationServices;
using AdminPanelApp.Data.AdminPanelServices.DashBoardServices;
using AdminPanelApp.Data.AdminPanelServices.LicenseServices;
using AdminPanelApp.Data.AdminPanelServices.OutletServices;
using AdminPanelApp.Data.AdminPanelServices.SMSTempateMasterServices;
using AdminPanelApp.Data.AdminPanelServices.UserMappingServices;
using AdminPanelApp.Data.AdminPanelServices.WorkspaceServices;
using AdminPanelApp.Data.AzureHelperServices;
using AdminPanelApp.Data.EncryptDecryptServices;
using AdminPanelApp.Data.LoginServices;
using AdminPanelApp.Data.MailSMSServices;
using AdminPanelApp.Data.RecoverPasswordServices;
using AdminPanelApp.Data.RegisterServices;
using AdminPanelApp.Data.UserSessionIndexDB;
using AdminPanelApp.Handlers;
using Blazor.SubtleCrypto;
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
builder.Services.AddSubtleCrypto(opt =>
    opt.Key = "ELE9xOyAyJHCsIPLMbbZHQ7pVy7WUlvZ60y5WkKDGMSw5xh5IM54kUPlycKmHF9VGtYUilglL8iePLwr" //Use another key
);
builder.Services.AddScoped<IEncryptDecrypt, EncryptDecrypt>();

await builder.Build().RunAsync();

 void AddHttpClients(WebAssemblyHostBuilder builder,string baseurl)
{


	//authentication http clients RegisterUserServiceClient
	builder.Services.AddHttpClient<IRegisterSubscriber, RegisterSubscriber>
		("RegisterViewModelClient", client => client.BaseAddress = new Uri(baseurl));
    builder.Services.AddHttpClient<IRecoverPasswordSubscriber, RecoverPasswordSubscriber>
        ("RecoverPasswordModelClient", client => client.BaseAddress = new Uri(baseurl));
    builder.Services.AddHttpClient<ILoginProvider, LoginProvider>
		("LoginViewModelClient", client => client.BaseAddress = new Uri(baseurl));
    builder.Services.AddHttpClient<IMailSMSSubscriber, MailSMSSubscriber>
        ("MAILSMSViewModelClient", client => client.BaseAddress = new Uri(baseurl));
    builder.Services.AddHttpClient<IAzureFileSubscriber, AzureFileSubscriber>
        ("FilesViewModelClient", client => client.BaseAddress = new Uri(baseurl));

    builder.Services.AddHttpClient<IAddUserSubscriber, AddUserSubscriber>
      ("AddUserServiceClient", client => client.BaseAddress = new Uri(baseurl)).AddHttpMessageHandler<CustomAuthorizationHandler>(); ;
    builder.Services.AddHttpClient<IAdduser_rightsSubscriber, Adduser_rightsSubscriber>
     ("Adduser_rightServiceClient", client => client.BaseAddress = new Uri(baseurl)).AddHttpMessageHandler<CustomAuthorizationHandler>(); ;
    builder.Services.AddHttpClient<IUserMappingSubscriber, UserMappingSubscriber>
        ("UserMappingServiceClient", client => client.BaseAddress = new Uri(baseurl)).AddHttpMessageHandler<CustomAuthorizationHandler>(); ;
    builder.Services.AddHttpClient<IApplicationSubscriber, ApplicationSubscriber>
     ("ApplicationServiceClient", client => client.BaseAddress = new Uri(baseurl)).AddHttpMessageHandler<CustomAuthorizationHandler>(); ;
    builder.Services.AddHttpClient<IWorkspaceSubscriber, WorkspaceSubscriber>
   ("WorkspaceServiceClient", client => client.BaseAddress = new Uri(baseurl)).AddHttpMessageHandler<CustomAuthorizationHandler>(); ;
    builder.Services.AddHttpClient<IOutletSubscriber, OutletSubscriber>
   ("OutletServiceClient", client => client.BaseAddress = new Uri(baseurl)).AddHttpMessageHandler<CustomAuthorizationHandler>(); ;
    builder.Services.AddHttpClient<ILicenseSubscriber, LicenseSubscriber>
   ("LicenseServiceClient", client => client.BaseAddress = new Uri(baseurl)).AddHttpMessageHandler<CustomAuthorizationHandler>(); ;
    builder.Services.AddHttpClient<ISMSTempateMasterSubscriber, SMSTempateMasterSubscriber>
   ("SMSTempateMasterServiceClient", client => client.BaseAddress = new Uri(baseurl)).AddHttpMessageHandler<CustomAuthorizationHandler>(); ;
    builder.Services.AddHttpClient<IDashboardSubscriber, DashboardSubscriber>
       ("DashboardServiceClient", client => client.BaseAddress = new Uri(baseurl)).AddHttpMessageHandler<CustomAuthorizationHandler>(); ;
   
}




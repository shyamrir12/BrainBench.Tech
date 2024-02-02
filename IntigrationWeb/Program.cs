using IntigrationWeb.Areas.NewDSC.Data;
using IntigrationWeb.Areas.OldDSC.Data;
using IntigrationWeb.Areas.Payment.Data;
using IntigrationWeb.ExceptionHandlear;
using IntigrationWeb.IntegrationWeb;
using IntigrationWeb.Models.AzureHelperServices;
using IntigrationWeb.Models.MailSMSServices;
using IntigrationWeb.Models.PaymentResponsesService;
using IntigrationWeb.Models.sbiIncriptDecript;
using IntigrationWeb.Models.UserAndErrorService;
using IntigrationWeb.Models.Utility;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
//add cross
var Clint1 = builder.Configuration.GetSection("KeyList")["Clint1"];
var Clint2 = builder.Configuration.GetSection("KeyList")["Clint2"];
builder.Services.AddCors();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    builder.WithOrigins(Clint1, Clint2)
           .AllowAnyMethod()
           .AllowAnyHeader());
});
//add exception filter
builder.Services.AddMvc(
               config =>
               {
                   config.Filters.Add(typeof(ExceptionFilterHandlear));
                   config.Filters.Add(typeof(SessionActionFilter));
               }

                          )//.AddNewtonsoftJson().SetCompatibilityVersion(CompatibilityVersion.Latest)

             .AddJsonOptions(options =>
             {
                 options.JsonSerializerOptions.IgnoreNullValues = true;
                 options.JsonSerializerOptions.WriteIndented = true;
                 options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
             });

builder.Services.AddScoped<IAzureFileSubscriber, AzureFileSubscriber>();
builder.Services.AddScoped<IMailSMSSubscriber, MailSMSSubscriber>();
builder.Services.AddScoped<IPaymentResponsesSubscriber, PaymentResponsesSubscriber>();
builder.Services.AddScoped<IsbiIncriptDecript, sbiIncriptDecript>();

builder.Services.AddScoped<IUserAndErrorSubscriber, UserAndErrorSubscriber>();
builder.Services.AddScoped<IHttpWebClients, HttpWebClients>();

builder.Services.AddScoped<INewDSCSubscriber, NewDSCSubscriber>();
builder.Services.AddScoped<IDSCResponseVerifySubscriber, DSCResponseVerifySubscriber>();
builder.Services.AddScoped<IPaymentRequestSubscriber, PaymentRequestSubscriber>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();
//from 5.0 auth app
app.UseHttpsRedirection();
app.UseCors();
//from 5.0 auth app
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

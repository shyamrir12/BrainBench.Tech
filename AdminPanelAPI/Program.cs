
using AdminPanelAPI.Factory;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using AdminPanelAPI.Data.RegisterServices;
using AdminPanelAPI.Data.ExceptionDataServices;
using AdminPanelAPI.Data.RecoverPasswordServices;
using AdminPanelAPI.Areas.AdminPanel.Data.AddUserServices;
using AdminPanelAPI.Areas.AdminPanel.Data.Adduser_rightsServices;
using AdminPanelAPI.Areas.AdminPanel.Data.UserMappingServices;
using AdminPanelAPI.Areas.AdminPanel.Data.DmsIssuedByServices;
using AdminPanelAPI.Areas.AdminPanel.Data.DepartmentServices;
using AdminPanelAPI.Areas.AdminPanel.Data.DmsHECategoryServices;
using AdminPanelAPI.Areas.AdminPanel.Data.LicenseServices;
using AdminPanelAPI.Areas.AdminPanel.Data.SMSTempateMasterServices;
using AdminPanelAPI.Areas.AdminPanel.Data.DashBoardServices;
using AdminPanelAPI.Areas.AdminPanel.Data.GenericReportServices;
using AdminPanelAPI.Data.RabitMQServices;

var builder = WebApplication.CreateBuilder(args);
//from 5.0 auth app
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

//add auth
var audienceConfig = builder.Configuration.GetSection("Audience");
var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(audienceConfig["Secret"]));

builder.Services.AddAuthentication(options =>
{
    //options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
               .AddJwtBearer(jwtBearerOptions =>
               {
                   jwtBearerOptions.RequireHttpsMetadata = true;
                   jwtBearerOptions.SaveToken = true;
                   jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters
                   {
                       ValidateIssuerSigningKey = true,
                       IssuerSigningKey = signingKey,
                       ValidateIssuer = false,
                       ValidateAudience = false,
                       ClockSkew = TimeSpan.Zero
                   };
               });

//from 5.0 auth app
IConnectionFactory connectionFactoryAuthDB = new ConnectionFactory(builder.Configuration.GetConnectionString("AuthDBConnectionString"));
builder.Services.AddSingleton(connectionFactoryAuthDB);

builder.Services.AddScoped<IRegisterProvider, RegisterProvider>();
builder.Services.AddScoped<IExceptionDataProvider, ExceptionDataProvider>();
builder.Services.AddScoped<IAddUserProvider, AddUserProvider>();
builder.Services.AddScoped<IAdduser_rightsProvider, Adduser_rightsProvider>();
builder.Services.AddScoped<IUserMappingProvider, UserMappingProvider>();
builder.Services.AddScoped<IDmsIssuedByProvider, DmsIssuedByProvider>();
builder.Services.AddScoped<IDepartmentProvider, DepartmentProvider>();
builder.Services.AddScoped<IDmsHECategoryProvider, DmsHECategoryProvider>();
builder.Services.AddScoped<ILicenseProvider, LicenseProvider>();
builder.Services.AddScoped<ISMSTempateMasterProvider, SMSTempateMasterProvider>();
builder.Services.AddScoped<IDashBoardProvider, DashBoardProvider>();
builder.Services.AddScoped<IRecoverPasswordProvider, RecoverPasswordProvider>();
//builder.Services.AddScoped<IGenericReportProvider, GenericReportProvider>();

// Add services to the container.
builder.Services.AddScoped<IRabitMQService, RabitMQService>();

builder.Services.AddControllers();

var app = builder.Build();

//from 5.0 auth app
app.UseHttpsRedirection();
app.UseCors();
app.UseRouting();
app.UseAuthentication();
//from 5.0 auth app

app.UseAuthorization();

app.MapControllers();


app.Run();

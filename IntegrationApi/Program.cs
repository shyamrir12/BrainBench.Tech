using IntegrationApi.Data.AzureHelperServices;
using IntegrationApi.Data.DSCResponseVerifyServices;
using IntegrationApi.Data.EncryptionServices;
using IntegrationApi.Data.ExceptionDataServices;
using IntegrationApi.Data.MailServices;
using IntegrationApi.Data.SchedulerMailSMSServices;
using IntegrationApi.Data.SMSServices;
using IntegrationApi.ExceptionFilter;
using IntegrationApi.Factory;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Text;

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
//add exception filter
//builder.Services.AddMvc(
//			   config =>
//			   {
//				   config.Filters.Add(typeof(ExceptionFilterHandlear));
//			   }

//						  )//.AddNewtonsoftJson().SetCompatibilityVersion(CompatibilityVersion.Latest)

//			 .AddJsonOptions(options =>
//			 {
//				 options.JsonSerializerOptions.IgnoreNullValues = true;
//				 options.JsonSerializerOptions.WriteIndented = true;
//			 });
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
builder.Services.AddScoped<IAzureHelperProvider, AzureHelperProvider>();
builder.Services.AddScoped<IDSCResponseVerifyProvider, DSCResponseVerifyProvider>();
builder.Services.AddScoped<IEncryptionProvider, EncryptionProvider>();
builder.Services.AddScoped<IExceptionDataProvider, ExceptionDataProvider>();

builder.Services.AddScoped<IMailProvider, MailProvider>();
builder.Services.AddScoped<ISchedulerMailSMSProvider, SchedulerMailSMSProvider>();
builder.Services.AddScoped<ISMSProvider, SMSProvider>();
// Add services to the container.

builder.Services.AddControllers();

var app = builder.Build();

var loggerFactory = app.Services.GetService<ILoggerFactory>();
loggerFactory.AddFile(builder.Configuration["Logging:LogFilePath"].ToString());

// Configure the HTTP request pipeline.

//from 5.0 auth app
app.UseHttpsRedirection();
app.UseCors();
app.UseRouting();
app.UseAuthentication();
//from 5.0 auth app

app.UseAuthorization();

app.MapControllers();

//app.UseMiddleware<ExceptionHandlingMiddleware>();
app.Run();

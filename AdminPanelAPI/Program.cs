using AdminPanelAPI.Factory;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using AdminPanelAPI.Data.RegisterServices;

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
// Add services to the container.

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

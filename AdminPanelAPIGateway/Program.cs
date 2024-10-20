using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
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
var authenticationProviderKey = "Bearer";
builder.Services.AddAuthentication(options =>
{
	//options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
	options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
	options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
			   .AddJwtBearer(authenticationProviderKey,jwtBearerOptions =>
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
// Add services to the container.
builder.Services.AddRazorPages();

//ocelot
builder.Services.AddEndpointsApiExplorer();

builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);
builder.Services.AddOcelot(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Error");
}
app.UseStaticFiles();

app.UseRouting();

//from 5.0 auth app
app.UseHttpsRedirection();
app.UseCors();
app.UseRouting();
app.UseAuthentication();
//from 5.0 auth app


app.UseAuthorization();


await app.UseOcelot();
app.Run();

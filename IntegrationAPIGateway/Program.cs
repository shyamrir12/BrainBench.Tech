using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

//add cross
string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddCors(options =>
{

	options.AddPolicy(MyAllowSpecificOrigins,
	builder =>
	{
		builder.AllowAnyOrigin()
   .AllowAnyMethod()
		.AllowAnyHeader();
	});
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
//ocelot
builder.Services.AddEndpointsApiExplorer();

builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);
builder.Services.AddOcelot(builder.Configuration);

var app = builder.Build();
app.UseStaticFiles();

app.UseRouting();

//from 5.0 auth app
app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseRouting();
app.UseAuthentication();
//from 5.0 auth app


app.UseAuthorization();


await app.UseOcelot();


app.Run();

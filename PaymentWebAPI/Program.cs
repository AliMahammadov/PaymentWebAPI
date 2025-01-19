using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.RegularExpressions;  // Add this using for regex
using PaymentWebData.Extensions;
using PaymentWebService.Extensions;
using PaymentWebService.Services.Concrete;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
    });

// Add Authentication
var key = builder.Configuration["Jwt:Key"];
if (string.IsNullOrEmpty(key))
{
    throw new ArgumentNullException("Jwt:Key is not provided");
}

var issuer = builder.Configuration["Jwt:Issuer"];
if (string.IsNullOrEmpty(issuer))
{
    throw new ArgumentNullException("Jwt:Issuer is not provided");
}

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidIssuer = issuer,
            ValidAudience = issuer,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
        };
    });

// Add Authorization
builder.Services.AddScoped<TokenService>();
builder.Services.AddAuthorization();

// Password validation logic: Add this before processing the password
builder.Services.AddScoped<IPasswordValidator, PasswordValidator>();

// Load custom layers
builder.Services.LoadDataLayerExtension(builder.Configuration);
builder.Services.LoadServiceLayerExtension();

// Configure Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();  // Add this line to enable authentication
app.UseAuthorization();

app.MapControllers();

// Configure error handling for non-development environments
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();
app.UseRouting();
app.UseRequestLocalization();

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=User}/{action=GetUserByIdAsync}/{id?}"
);

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=User}/{action=GetUserByIdAsync}/{id?}");

app.Run();

// Password Validator Class
public class PasswordValidator : IPasswordValidator
{
    public bool Validate(string password)
    {
        // Regex for password validation: at least one lowercase, one uppercase, one number, and minimum 6 characters
        string pattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{6,}$";
        return Regex.IsMatch(password, pattern);
    }
}

public interface IPasswordValidator
{
    bool Validate(string password);
}

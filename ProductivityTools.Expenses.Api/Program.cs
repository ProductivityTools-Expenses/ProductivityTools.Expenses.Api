using ProductivityTools.Expenses.Database;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = "https://securetoken.google.com/ptexpensesprod";
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = "https://securetoken.google.com/ptexpensesprod",
            ValidateAudience = true,
            ValidAudience = "ptexpensesprod",
            ValidateLifetime = true
        };
    });

builder.Services.AddCors(options =>
{
    options.AddPolicy(MyAllowSpecificOrigins,
    builder =>
    {
        builder.WithOrigins("http://localhost:3000", "https://localhost:3000", "https://transfersweb.z16.web.core.windows.net").AllowAnyMethod().AllowAnyHeader();
    });
});
// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<ExpensesContext>();

var app = builder.Build();
app.UseCors(MyAllowSpecificOrigins);

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseAuthentication();

app.MapControllers();

app.Run();

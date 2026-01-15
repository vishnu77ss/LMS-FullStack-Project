using LMS.Api.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Controllers
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Database
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    )
);

// CORS – allow frontend (Netlify / any)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

var app = builder.Build();

// Enable CORS
app.UseCors("AllowAll");

// Swagger – ENABLE ALWAYS (important)
app.UseSwagger();
app.UseSwaggerUI();

// No HTTPS redirection (Render handles it)
app.UseAuthorization();

app.MapControllers();

app.Run();

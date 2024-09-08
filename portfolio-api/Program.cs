using Microsoft.EntityFrameworkCore;
using portfolio_api.Models;
using portfolio_api;
using Microsoft.AspNetCore.Authentication;
using portfolio_api.Services;

var builder = WebApplication.CreateBuilder(args);




// Add InMemory database for users
builder.Services.AddDbContext<UserDbContext>(options =>
    options.UseInMemoryDatabase("UserDb"));

// Register the registration service
builder.Services.AddScoped<IAuthService, AuthService>();

// Add Swagger services
builder.Services.AddEndpointsApiExplorer(); // Automatically generates Swagger documentation
builder.Services.AddSwaggerGen(); // Adds the Swagger generation service

builder.Services.AddCors(options =>
{
  options.AddPolicy("AllowLocalhost",
         policy =>
         {
           policy.WithOrigins("http://localhost:3000") // Specify React app URL
                .AllowAnyHeader()
                .AllowAnyMethod();
         });
});

var app = builder.Build();

// Use CORS before routing
app.UseCors("AllowLocalhost");

app.UseRouting();

app.UseSwaggerInDevelopment();

app.AuthenticationEndpoint();

app.MapGet("/health", () => "healthy!");

app.Run();

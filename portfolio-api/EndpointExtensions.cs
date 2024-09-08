using System.Text.Json;
using portfolio_api.Models;
using portfolio_api.Services;

namespace portfolio_api
{
  public static class EndpointExtensions
  {
    public static void AuthenticationEndpoint(this WebApplication app)
    {
      app.MapPost("/register", async (Users user, IAuthService authService) =>
      {
        var result = await authService.RegisterAsync(user);

        // Check the status of the AuthResult to determine the response
        if (result.Status != AuthResultStatus.Success)
        {
          return Results.BadRequest(result);
        }

        return Results.Ok(result);
      });

      app.MapPost("/login", async (HttpContext httpContext, IAuthService authService) =>
      {
    
        var requestBody = await new StreamReader(httpContext.Request.Body).ReadToEndAsync();
        var loginRequest = JsonSerializer.Deserialize<LoginRequest>(requestBody, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        if (loginRequest == null || string.IsNullOrEmpty(loginRequest.Username) || string.IsNullOrEmpty(loginRequest.Password))
        {
          return Results.BadRequest(new AuthResult { Status = AuthResultStatus.InvalidCredentials, Message = "Username and password are required." });
        }

        var result = await authService.LoginAsync(loginRequest.Username, loginRequest.Password);

        if (result.Status != AuthResultStatus.Success)
        {
          return Results.BadRequest(result);
        }

        return Results.Ok(result);
      });
    }
  }
}

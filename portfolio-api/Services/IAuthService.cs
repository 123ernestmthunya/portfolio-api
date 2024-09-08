using portfolio_api.Models;

namespace portfolio_api.Services
{
  public interface IAuthService
  {
    Task<AuthResult> RegisterAsync(Users user);
    Task<AuthResult> LoginAsync(string username, string password);
  }
}

using Microsoft.EntityFrameworkCore;
using portfolio_api.Models;

namespace portfolio_api.Services
{
  public class AuthService : IAuthService
  {
    private readonly UserDbContext _context;

    public AuthService(UserDbContext context)
    {
      _context = context;
    }

    public async Task<AuthResult> RegisterAsync(Users user)
    {
      if (string.IsNullOrEmpty(user.Username) || string.IsNullOrEmpty(user.Password) || string.IsNullOrEmpty(user.Email))
      {
        return new AuthResult
        {
          Status = AuthResultStatus.Failure,
          Message = ApplicationConstants.AllFieldsRequired
        };
      }

      if (await _context.Users.AnyAsync(u => u.Username == user.Username))
      {
        return new AuthResult
        {
          Status = AuthResultStatus.UsernameAlreadyExists,
          Message = ApplicationConstants.UsernameAlreadyExists
        };
      }

      if (await _context.Users.AnyAsync(u => u.Email == user.Email))
      {
        return new AuthResult
        {
          Status = AuthResultStatus.EmailAlreadyExists,
          Message = ApplicationConstants.EmailAlreadyExists
        };
      }

      _context.Users.Add(user);
      await _context.SaveChangesAsync();

      return new AuthResult
      {
        Status = AuthResultStatus.Success,
        Message = ApplicationConstants.RegistrationSuccessful
      };
    }

    public async Task<AuthResult> LoginAsync(string username, string password)
    {
      var user = await _context.Users
          .FirstOrDefaultAsync(u => u.Username == username && u.Password == password);

      if (user == null)
      {
        return new AuthResult
        {
          Status = AuthResultStatus.InvalidCredentials,
          Message = ApplicationConstants.InvalidCredentials
        };
      }

      return new AuthResult
      {
        Status = AuthResultStatus.Success,
        Message = ApplicationConstants.LoginSuccessful
      };
    }
  }
}

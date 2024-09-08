namespace portfolio_api.Models
{
  public class AuthResult
  {
    public AuthResultStatus Status { get; set; }
    public string Message { get; set; }
  }

  public enum AuthResultStatus
  {
    Success,
    Failure,
    UsernameAlreadyExists,
    EmailAlreadyExists,
    InvalidCredentials
  }
}

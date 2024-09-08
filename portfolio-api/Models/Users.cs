namespace portfolio_api.Models
{
  public class Users
  {
    public int Id { get; set; }
    public string Username { get; set; }
    public string Password { get; set; } // Should be hashed in production
    public string Email { get; set; }
  }
}

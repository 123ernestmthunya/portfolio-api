using Microsoft.EntityFrameworkCore;
using portfolio_api.Models;

namespace portfolio_api
{
  // DbContext for Users
  public class UserDbContext : DbContext
  {
    public UserDbContext(DbContextOptions<UserDbContext> options) : base(options) { }
    public DbSet<Users> Users { get; set; }
  }
}

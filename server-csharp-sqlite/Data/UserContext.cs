using Microsoft.EntityFrameworkCore;
using server_csharp_sqlite.Models;

namespace server_csharp_sqlite.Data
{
  public class UserContext : DbContext
  {
    public UserContext(DbContextOptions<UserContext> opt) : base(opt)
    {

    }

    public DbSet<User> Users { get; set; }
  }
}
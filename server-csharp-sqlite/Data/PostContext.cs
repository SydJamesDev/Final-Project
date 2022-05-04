using Microsoft.EntityFrameworkCore;
using server_csharp_sqlite.Models;

namespace server_csharp_sqlite.Data
{
  public class PostContext : DbContext
  {
    public PostContext(DbContextOptions<PostContext> opt) : base(opt)
    {

    }

    public DbSet<Post> Posts { get; set; }

  }
}
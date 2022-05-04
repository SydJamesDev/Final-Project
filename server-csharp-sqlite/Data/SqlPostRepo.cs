using System;
using System.Collections.Generic;
using System.Linq;
using server_csharp_sqlite.Models;

namespace server_csharp_sqlite.Data
{
  public class SqlPostRepo : IPostRepo
  {
    private readonly PostContext _context;

    public SqlPostRepo(PostContext context)
    {
        _context = context;
    }

    public void CreatePost(Post post)
    {
      if(post == null)
      {
        throw new ArgumentNullException(nameof(post));
      }

      _context.Posts.Add(post);
    }

    public void DeletePost(Post post)
    {
      if(post == null)
      {
        throw new ArgumentNullException(nameof(post));
      }
      _context.Posts.Remove(post);
    }

    public IEnumerable<Post> GetAllPosts()
    {
      return _context.Posts.ToList();
    }

    public Post GetPostById(int id)
    {
      return _context.Posts.FirstOrDefault(p => p.Id == id);
    }

    public bool SaveChanges()
    {
      return (_context.SaveChanges() >= 0);
    }

    public void UpdatePost(Post post)
    {
      //Nothing
    }
  }
}
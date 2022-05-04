using System.Collections.Generic;
using server_csharp_sqlite.Models;

namespace server_csharp_sqlite.Data
{
  public interface IPostRepo
  {
    bool SaveChanges();
    IEnumerable<Post> GetAllPosts();
    Post GetPostById(int id);
    void CreatePost(Post post);
    void UpdatePost(Post post);
    void DeletePost(Post post);
  }
}
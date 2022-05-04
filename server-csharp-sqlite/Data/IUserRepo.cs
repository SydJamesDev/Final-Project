using System.Collections.Generic;
using server_csharp_sqlite.Models;

namespace server_csharp_sqlite.Data
{
  public interface IUserRepo
  {
    bool SaveChanges();
    IEnumerable<User> GetAllUsers();
    User GetUserById(int id);
    void CreateUser(User user);
    void DeleteUser(User user);
    void Login(User user);
  }
}
using System;
using System.Collections.Generic;
using System.Linq;
using server_csharp_sqlite.Models;

namespace server_csharp_sqlite.Data
{
  public class SqlUserRepo : IUserRepo
  {
    private readonly UserContext _context;

    public SqlUserRepo(UserContext context)
    {
      _context = context;
    }
    public void CreateUser(User user)
    {
      if(user == null)
      {
        throw new ArgumentNullException(nameof(user));
      }

      _context.Users.Add(user);
    }

    public void DeleteUser(User user)
    {
      if(user == null)
      {
        throw new ArgumentNullException(nameof(user));
      }
      _context.Users.Remove(user);
    }

    public IEnumerable<User> GetAllUsers()
    {
      return _context.Users.ToList();
    }

    public User GetUserById(int id)
    {
      return _context.Users.FirstOrDefault(u => u.Id == id);
    }

    public void Login(User user)
    {
      //Nothing
    }

    public bool SaveChanges()
    {
      return (_context.SaveChanges() >= 0);
    }
  }
}
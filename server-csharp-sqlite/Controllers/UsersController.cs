using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using server_csharp_sqlite.Data;
using server_csharp_sqlite.Models;
using server_csharp_sqlite.Services;

namespace server_csharp_sqlite.Controllers
{
  //api/users
  [Microsoft.AspNetCore.Mvc.Route("api/users")]
  [ApiController]
  public class UsersController : ControllerBase
  {
    private readonly IUserRepo _repository;
    private readonly IMapper _mapper;
    private readonly UserContext _context;

    public UsersController(IUserRepo repository, UserContext context)
    {
      _repository = repository;
      _context = context;
    }

    [HttpGet]
    public ActionResult<IEnumerable<User>> GetAllUsers()
    {
      var userItems = _repository.GetAllUsers();
      return Ok(userItems);
    }

    //GET api/users/{id}
    [HttpGet("{id}", Name="GetUserById")]
    public ActionResult<User> GetUserById(int id)
    {
      var userItem = _repository.GetUserById(id);
      if(userItem != null)
      {
        return Ok(userItem);
      }
      return NotFound();
    }

    //POST api/users
    [HttpPost]
    public ActionResult<User> CreateUser(User user)
    {
      user.Password = ManualAuth.Sha256(user.Password);
      _repository.CreateUser(user);
      _repository.SaveChanges();

      return CreatedAtRoute(nameof(GetUserById), new {Id = user.Id}, user);
    }

    //POST api/users
    [HttpPost]
    public ActionResult<User> Login(User user)
    {
      User GetUser = _context.Users.SingleOrDefault(u => u.UserName == user.UserName);
      if(GetUser != null)
      {
        if(ManualAuth.Sha256Check(user.Password, GetUser.Password))
        {
          return Ok();
        }
      }
      throw new ArgumentException("Incorrect Login!");
    }


    //DELETE api/users/{id}
    [HttpDelete("{id}")]
    public ActionResult DeleteUser(int id)
    {
      var userModelFromRepo = _repository.GetUserById(id);
      if(userModelFromRepo == null)
      {
        return NotFound();
      }
      _repository.DeleteUser(userModelFromRepo);
      _repository.SaveChanges();

      return NoContent();
    }
  }
}
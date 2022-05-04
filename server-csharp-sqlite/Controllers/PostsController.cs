using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using server_csharp_sqlite.Data;
using server_csharp_sqlite.Models;

namespace server_csharp_sqlite.Controllers
{
    //api/posts
    [Route("api/posts")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IPostRepo _repository;
        public PostsController(IPostRepo repository)
        {
            _repository = repository;
        }
        
        //GET api/posts
        [HttpGet]
        public ActionResult<IEnumerable<Post>> GetAllPosts()
        {
            var postItems = _repository.GetAllPosts();
            return Ok(postItems);
        }

        //GET api/posts/{id}
        [HttpGet("{id}", Name="GetPostById")]
        public ActionResult<Post> GetPostById(int id)
        {
            var postItem = _repository.GetPostById(id);
            if(postItem != null)
            {
                return Ok(postItem);
            }
            return NotFound();
        }

        //POST api/posts
        [HttpPost]
        public ActionResult<Post> CreatePost(Post post)
        {
            _repository.CreatePost(post);
            _repository.SaveChanges();

            return CreatedAtRoute(nameof(GetPostById), new {Id = post.Id}, post);
        }

        //PUT api/posts/{id}
        [HttpPut("{id}")]
        public ActionResult UpdatePost(int id, Post post)
        {
            var postFromRepo = _repository.GetPostById(id);
            if(postFromRepo == null)
            {
                return NotFound();
            }

            _repository.UpdatePost(postFromRepo);

            _repository.SaveChanges();

            return NoContent();
        }

        //PATCH api/posts/{id}
        [HttpPatch("{id}")]
        public ActionResult PartialPostUpdate(int id, JsonPatchDocument<Post> patchDoc)
        {
            var postFromRepo = _repository.GetPostById(id);
            if(postFromRepo == null)
            {
                return NotFound();
            }

            var postToPatch = postFromRepo;
            patchDoc.ApplyTo(postToPatch, ModelState);
            if(!TryValidateModel(postToPatch))
            {
                return ValidationProblem(ModelState);
            }

            _repository.UpdatePost(postFromRepo);

            _repository.SaveChanges();

            return NoContent();
        }

        //DELETE api/posts/{id}
        [HttpDelete("{id}")]
        public ActionResult DeletePost(int id)
        {
            var postModelFromRepo = _repository.GetPostById(id);
            if(postModelFromRepo == null)
            {
                return NotFound();
            }
            _repository.DeletePost(postModelFromRepo);
            _repository.SaveChanges();

            return NoContent();
        }
    }
}
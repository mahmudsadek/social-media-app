﻿using Microsoft.AspNetCore.Mvc;
using social_media_app.DTOs;
using social_media_app.Models;
using social_media_app.Repository;
using System.Collections.Generic;
using System.Linq;

namespace social_media_app.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IPostRepository _postRepository;

        public PostsController(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        // GET: api/posts
        [HttpGet]
        public ActionResult<IEnumerable<PostDTO>> GetPosts()
        {
            var posts = _postRepository.GetAll()
                .Select(post => new PostDTO
                {
                    Content = post.Content,
                    PostTime = post.PostTime,
                    PostImage = post.PostImage,
                    UserId = post.UserId
                })
                .ToList();

            return Ok(posts);
        }

        // GET: api/posts/5
        [HttpGet("{id}")]
        public ActionResult<PostDTO> GetPost(int id)
        {
            var post = _postRepository.Get(id);

            if (post == null)
            {
                return NotFound();
            }

            var postDTO = new PostDTO
            {
                Content = post.Content,
                PostTime = post.PostTime,
                PostImage = post.PostImage,
                UserId = post.UserId
            };

            return Ok(postDTO);
        }

        // POST: api/posts
        [HttpPost]
        public IActionResult CreatePost(PostDTO postDTO)
        {
            var post = new Post
            {
                Content = postDTO.Content,
                PostTime = (DateTime)postDTO.PostTime,
                PostImage = postDTO.PostImage,
                UserId = postDTO.UserId
            };

            _postRepository.Insert(post);
            _postRepository.Save();
            return CreatedAtAction(nameof(GetPost), new { id = post.Id }, postDTO);
        }

        // PUT: api/posts/5
        [HttpPut("{id}")]
        public IActionResult UpdatePost(int id, PostDTO postDTO)
        {
            var post = _postRepository.Get(id);

            if (post == null)
            {
                return NotFound();
            }

            post.Content = postDTO.Content;
            post.PostTime = (DateTime)postDTO.PostTime;
            post.PostImage = postDTO.PostImage;
            post.UserId = postDTO.UserId;

            _postRepository.Update(post);
            _postRepository.Save();
            return NoContent();
        }

        // DELETE: api/posts/5
        [HttpDelete("{id}")]
        public IActionResult DeletePost(int id)
        {
            var post = _postRepository.Get(id);

            if (post == null)
            {
                return NotFound();
            }

            _postRepository.Delete(post);
            _postRepository.Save();
            return NoContent();
        }
    }
}
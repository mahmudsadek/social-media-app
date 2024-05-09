using Microsoft.AspNetCore.Mvc;
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

        private readonly ICommentRepository _commentRepository;
        private readonly IReactRepository _reactRepository;

        public PostsController(IPostRepository postRepository, ICommentRepository commentRepository, IReactRepository reactRepository)
        {
            _postRepository = postRepository;
            _commentRepository = commentRepository;
            _reactRepository = reactRepository;
        }


        //public PostsController(IPostRepository postRepository)
        //{
        //    _postRepository = postRepository;
        //}

        // GET: api/posts
        [HttpGet]
        public ActionResult<IEnumerable<PostDTO>> GetPosts()
        {
            var posts = _postRepository.GetAll()
                .Select(post => new PostDTO
                {
                    Id = post.Id,
                    Content = post.Content,
                    PostTime = post.PostTime,
                    PostImage = post.PostImage,
                    UserId = post.UserId,
                    LikeCount = post.Reactions?.Count(r => r.Value == true),
                    DislikeCount = post.Reactions?.Count(r => r.Value == false)

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
                Id = post.Id,
                Content = post.Content,
                PostTime = post.PostTime,
                PostImage = post.PostImage,
                UserId = post.UserId
            };

            return Ok(postDTO);
        }







        [HttpGet("reactions/{id}")]
        public ActionResult<PostReactionsDTO> GetPostReactions(int id)
        {
            var post = _postRepository.Get(id);

            if (post == null)
            {
                return NotFound();
            }

            var postReactionsDTO = new PostReactionsDTO
            {
                PostId = id,
                LikeCount = _reactRepository.GetReactCount(id, true),
                DislikeCount = _reactRepository.GetReactCount(id, false)
            };

            return Ok(postReactionsDTO);
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

        //// DELETE: api/posts/5
        //[HttpDelete("{id}")]
        //public IActionResult DeletePost(int id)
        //{
        //    var post = _postRepository.Get(id);

        //    if (post == null)
        //    {
        //        return NotFound();
        //    }

        //    _postRepository.Delete(post);
        //    _postRepository.Save();
        //    return NoContent();
        //}


        // DELETE: api/posts/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
         
            var postToDelete = _postRepository.Get(id);

            if (postToDelete == null)
            {
                return NotFound();
            }

          
            _postRepository.DeleteWithRelatedEntities(postToDelete);

            return NoContent();
        }


        // GET: api/posts/user/{userId}
        [HttpGet("user/{userId}")]
        public ActionResult<IEnumerable<PostDTO>> GetPostsByUserId(string userId)
        {
            // Retrieve posts associated with the provided user ID
            var posts = _postRepository.Get(post => post.UserId == userId)
                .Select(post => new PostDTO
                {
                    Id = post.Id,
                    Content = post.Content,
                    PostTime = post.PostTime,
                    PostImage = post.PostImage,
                    UserId = post.UserId,
                    LikeCount = post.Reactions?.Count(r => r.Value == true),
                    DislikeCount = post.Reactions?.Count(r => r.Value == false)
                })
                .ToList();

            if (posts.Count == 0)
            {
                return NotFound($"No posts found for user with ID: {userId}");
            }

            return Ok(posts);
        }


    }
}
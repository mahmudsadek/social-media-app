using Microsoft.AspNetCore.Mvc;
using social_media_app.Models;
using social_media_app.Repository;

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
        public ActionResult<IEnumerable<Post>> GetPosts()
        {
            var posts = _postRepository.GetAll();
            return Ok(posts);
        }

        // GET: api/posts/5
        [HttpGet("{id}")]
        public ActionResult<Post> GetPost(int id)
        {
            var post = _postRepository.Get(id);

            if (post == null)
            {
                return NotFound();
            }

            return Ok(post);
        }

        // POST: api/posts
        [HttpPost]
        public IActionResult CreatePost(Post post)
        {
            _postRepository.Insert(post);
            _postRepository.Save();
            return CreatedAtAction(nameof(GetPost), new { id = post.Id }, post);
        }

        // PUT: api/posts/5
        [HttpPut("{id}")]
        public IActionResult UpdatePost(int id, Post post)
        {
            if (id != post.Id)
            {
                return BadRequest();
            }

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

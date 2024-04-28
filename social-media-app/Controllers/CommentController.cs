using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using social_media_app.Repository;
using social_media_app.Models;



namespace social_media_app.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {

        private IRepository<Comment> _repository;
        public CommentController(IRepository<Comment> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            List<Comment> comments = _repository.GetAll();
            return Ok(comments);
        }

        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            Comment comment = _repository.Get(id);
            return Ok(comment);
        }
        [HttpPut]
        public IActionResult Edit(int id, Comment comment)
        {
            Comment commentDB = _repository.Get(id);
            if (commentDB == null)
            {
                return BadRequest();
            }
            commentDB.Content= comment.Content;
            commentDB.CommentTime= comment.CommentTime;
            commentDB.PostId= comment.PostId;
            commentDB.Id= comment.Id;
            commentDB.UserId= comment.UserId;
            commentDB.Replays= comment.Replays;
            _repository.Save();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            try
            {
                Comment comment = _repository.Get(id);
                _repository.Delete(comment);
                _repository.Save();
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Add(Comment comment)
        {
            if (ModelState.IsValid == true)
            {
                _repository.Insert(comment);
                _repository.Save();
                return CreatedAtAction("Get", new { id = comment.Id }, comment);

            }
            return BadRequest(ModelState);
        }

    }
}

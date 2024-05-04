using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using social_media_app.Repository;
using social_media_app.Models;
using social_media_app.DTOs;



namespace social_media_app.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {

        private ICommentRepository _repository;
        private INotifyRepository _notifyRepository;
        public CommentController(ICommentRepository repository ,INotifyRepository notifyRepository)
        {
            _repository = repository;
            _notifyRepository = notifyRepository;
        }

        [HttpGet("post/{id:int}")]
        public IActionResult GetAllByPostId(int id)
        {
            List<Comment> comments = _repository.Get(e=>e.PostId ==id);
            return Ok(comments);
        }

        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            Comment comment = _repository.Get(id);
            return Ok(comment);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            List<Comment> comments = _repository.GetAll();
            return Ok(comments);
        }

        
        [HttpPut]
        public IActionResult Edit(int id, CommentDTO comment)
        {
            Comment commentDB = _repository.Get(id);
            if (commentDB == null)
            {
                return BadRequest();
            }
            commentDB.Content= comment.Content;
            commentDB.CommentTime= (DateTime)comment.CommentTime;
            commentDB.PostId= comment.PostId;
            commentDB.UserId= comment.UserId;
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
        public IActionResult Add(CommentDTO commentDTO)
        {
            if (ModelState.IsValid == true)
            {
                Comment comment = new();

                comment.Content= commentDTO.Content;
                comment.CommentTime= (DateTime) commentDTO.CommentTime;
                comment.PostId= commentDTO.PostId;
                comment.UserId= commentDTO.UserId;

                _repository.Insert(comment);
                _repository.Save();

                Comment comment2= _repository.GetAll(["User", "Post"]).Find(c=>c.Id==comment.Id);

                {
                    Notify notify = new Notify();
                    notify.Content = "There is new comment from "+comment2.User.UserName;
                    notify.PostedUserId= comment2.UserId;
                    notify.UserId=  comment2.Post.UserId;
                    _notifyRepository.Insert(notify);
                    _notifyRepository.Save();
                }
                return CreatedAtAction("Get", new { id = comment.Id }, comment);

            }
            return BadRequest(ModelState);
        }

    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using social_media_app.DTOs;
using social_media_app.Models;
using social_media_app.Repository;

namespace social_media_app.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotifyController : ControllerBase
    {
        private INotifyRepository _repository;
        public NotifyController(INotifyRepository repository)
        {
            _repository = repository;
        }


        [HttpGet("notify/{id:int}")]
        public IActionResult GetAllByUserId(string id)
        {
            List<Notify> notifies = _repository.GetAll(["PostedUser", "User"])
                .Where(n=>n.UserId==id).ToList();
            return Ok(notifies);
        }

        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            Notify notify = _repository.Get(id);
            return Ok(notify);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            List<Notify> notifies = _repository.GetAll();
            return Ok(notifies);
        }

        

    }
}

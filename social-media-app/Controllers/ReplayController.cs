using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using social_media_app.DTOs;
using social_media_app.Models;
using social_media_app.Repository;

namespace social_media_app.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReplayController : ControllerBase
    {

        private IReplayRepository _repository;
        public ReplayController(IReplayRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            List<Replay> replays = _repository.GetAll();
            return Ok(replays);
        }

        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            Replay replay = _repository.Get(id);
            return Ok(replay);
        }
        [HttpPut]
        public IActionResult Edit(int id, ReplayDTO replay)
        {
            Replay replayDB = _repository.Get(id);
            if (replayDB == null)
            {
                return BadRequest();
            }
            replayDB.PostId=replay.PostId;
            replayDB.CommentId=replay.CommentId;
            replayDB.Content = replay.Content;
            replayDB.UserId = replay.UserId;
            replayDB.ReplayTime=(DateTime)replay.ReplayTime;
            _repository.Save();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            try
            {
                Replay replay = _repository.Get(id);
                _repository.Delete(replay);
                _repository.Save();
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Add(ReplayDTO replay)
        {
            if (ModelState.IsValid == true)
            {
                Replay replayDB = new();

                replayDB.PostId = replay.PostId;
                replayDB.CommentId = replay.CommentId;
                replayDB.Content = replay.Content;
                replayDB.UserId = replay.UserId;
                replayDB.ReplayTime = (DateTime)replay.ReplayTime;

                _repository.Insert(replayDB);
                _repository.Save();
                return CreatedAtAction("Get", new { id = replayDB.Id }, replayDB);

            }
            return BadRequest(ModelState);
        }

    }
}

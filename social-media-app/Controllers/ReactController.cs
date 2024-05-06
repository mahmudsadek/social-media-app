using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using social_media_app.DTO;
using social_media_app.Models;
using social_media_app.Repository;

namespace social_media_app.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReactController : ControllerBase
    {
        private readonly IReactRepository reactRepository;

        //Ask
        public ReactController(IReactRepository _reactRepository)
        {
            reactRepository = _reactRepository;
        }

        //GET React by ID

        [HttpGet]
        public IActionResult GetReact(int postId, string userId)
        {
            if (reactRepository.CheckReactOnPost(postId, userId) == "Found")
            {
                React react = reactRepository.GetReact(postId, userId);

                ReactWithoutPostAndUserObj reactDto = new();

                reactDto.Id = react.Id;
                reactDto.UserId = react.UserId;
                reactDto.Value = react.Value;
                reactDto.PostId = react.PostId;

                return Ok(reactDto);

            }
            return NotFound();

        }


        //GET ALL Reacts

        [HttpGet("all")]
        public IActionResult GetAll(int postId)
        {
            return Ok(reactRepository.GetAll(postId));
        }

        [HttpPost]
        public IActionResult Add(ReactWithoutPostAndUserObj ReactDto)
        {
            if (ModelState.IsValid == true)
            {
                if(reactRepository.CheckReactOnPost(ReactDto.PostId, ReactDto.UserId) == "Not found")
                {
                    React react = new();

                    react.Id = ReactDto.Id;
                    react.Value = ReactDto.Value;
                    react.PostId = ReactDto.PostId;
                    react.UserId = ReactDto.UserId;

                    reactRepository.Insert(react);
                    reactRepository.Save();

                    return CreatedAtAction("GetReact", new { postId = react.PostId, userId = react.UserId }, react);
                }

                // RedirectToAction("RemoveReact", new { postId = ReactDto.PostId, userId = ReactDto.UserId });
                return RemoveReact(ReactDto.PostId, ReactDto.UserId);
            }

            return BadRequest(ModelState);
        }

        [HttpPut]
        public IActionResult ChangeReact(int id, ReactWithoutPostAndUserObj ChangeReact)
        {
            React react = reactRepository.Get(id);
            if (react != null)
            {
                if (react.Id == ChangeReact.Id)
                {
                    react.Value = ChangeReact.Value;

                    reactRepository.Update(react);
                    reactRepository.Save();

                    return NoContent();
                }
            }
            return BadRequest("Invalid ID");
        }

        [HttpDelete]
        public IActionResult RemoveReact(int postId, string userId)
        {
            try
            {
                reactRepository.Delete(reactRepository.GetReact(postId, userId));
                reactRepository.Save();

                return NoContent();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }

    
}

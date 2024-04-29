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

        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            return Ok(reactRepository.Get(id));
        }


        //GET ALL Reacts

        [HttpGet("all")]
        public IActionResult GetAll()
        {
            return Ok(reactRepository.GetAll());
        }

        [HttpPost]
        public IActionResult Add(ReactWithoutPostAndUserObj ReactDto)
        {
            if (ModelState.IsValid == true)
            {
                React react = new();

                react.Id = ReactDto.Id;
                react.Value = ReactDto.Value;
                react.PostId = ReactDto.PostId;
                react.UserId = ReactDto.UserId;

                reactRepository.Insert(react);
                reactRepository.Save();

                return CreatedAtAction("GetById", new { id = react.Id }, react);
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
        public IActionResult RemoveReact(int id)
        {
            try
            {
                reactRepository.Delete(reactRepository.Get(id));
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

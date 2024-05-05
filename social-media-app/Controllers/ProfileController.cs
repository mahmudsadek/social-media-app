using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using social_media_app.DTOs;
using social_media_app.Models;

namespace social_media_app.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly IWebHostEnvironment webHost;
        private readonly UserManager<User> userManager;

        public ProfileController(IWebHostEnvironment webHost, UserManager<User> userManager)
        {
            this.webHost = webHost;
            this.userManager = userManager;
        }

        [HttpPost("uploadimg")]
        public async Task<IActionResult> ImageUplaud()
        {
            var res = false;
            try
            {
                var UploadedFiles = Request.Form.Files;
                foreach (var file in UploadedFiles)
                {
                    string filename = file.FileName;
                    string filepath = getFilepath(filename);
                    if (!System.IO.Directory.Exists(filepath))
                    {
                        System.IO.Directory.CreateDirectory(filepath);
                    }
                    string imagepath = filepath + "\\" + filename;
                    if (System.IO.File.Exists(imagepath))
                    {
                        System.IO.File.Delete(imagepath);
                    }

                    using (FileStream stream = System.IO.File.Create(imagepath))
                    {
                        await file.CopyToAsync(stream);
                        res = true;
                    }
                }
            }
            catch
            {

            }
            if (res)
            {
                return Ok(new
                {
                    status = "ok",
                    massage = "works!"
                });
            }
            else
            {
                return BadRequest();
            }
        }
        [NonAction]
        public string getFilepath(string name)
        {
            return this.webHost.WebRootPath + "\\user\\" + name;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> MyProfile(string id)
        {
            User? user = await userManager.FindByIdAsync(id);
            if(user != null)
            {
                MyProfileDTO myProfile = new()
                { 
                    Id = id,
                    Email = user.Email, 
                    Name = user.Name, 
                    UserName = user.UserName, 
                    Bio = user.Bio, 
                    ProfileImage = user.ProfileImage, 
                    CoverImage = user.CoverImage 
                };
                return Ok(
                    new
                    {
                        status = 200,
                        data = myProfile
                    }
                );
            }
            else
            {
                return NotFound(
                    new
                    {
                        status = 404,
                        message = "wrong id!"
                    }
                );
            }
        }
    }
}

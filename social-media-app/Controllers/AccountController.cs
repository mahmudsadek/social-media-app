using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using social_media_app.DTOs;
using social_media_app.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace social_media_app.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<User> userManager;
        private readonly IConfiguration configuration;

        public AccountController(UserManager<User> userManager, IConfiguration configuration)
        {
            this.userManager = userManager;
            this.configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register(UserResgisterDTO NewUser)
        {
            if (ModelState.IsValid)
            {
                //create acc
                User user = new User()
                {
                    Email = NewUser.Email,
                    UserName = NewUser.UserName,
                    PasswordHash = NewUser.Password
                };
                IdentityResult result = await userManager.CreateAsync(user, NewUser.Password);
                if (result.Succeeded)
                {
                    return Ok("Account Created");
                }
                return BadRequest(result.Errors);

            }
            return BadRequest(ModelState);
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login(UserLoginDTO LoginUser)
        {
            if (ModelState.IsValid)
            {
                User? userDb = await userManager.FindByEmailAsync(LoginUser.Email);
                if (userDb != null)
                {
                    //check pass
                    bool IsPassCorrect = await userManager.CheckPasswordAsync(userDb, LoginUser.Password);
                    if (IsPassCorrect)
                    {
                        //create token
                        SymmetricSecurityKey key = new(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]));
                        SigningCredentials credentials = new(key, SecurityAlgorithms.HmacSha256);
                        JwtHeader JWTHeader = new(credentials);
                        List<Claim> MyClaims = new();
                        MyClaims.Add(new Claim(ClaimTypes.Name, userDb.UserName));
                        MyClaims.Add(new Claim(ClaimTypes.NameIdentifier, userDb.Id));
                        MyClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, DateTime.Now.ToString()));
                        JwtPayload pyload = new(
                            issuer: configuration["JWT:ValidIssuer"],
                            audience: configuration["JWT:ValidAudience"],
                            claims: MyClaims,
                            expires: DateTime.Now.AddDays(30),
                            notBefore: null
                            );
                        JwtSecurityToken token = new(JWTHeader, pyload);
                        return Ok(
                            new
                            {
                                token = new JwtSecurityTokenHandler().WriteToken(token),
                            }
                        );
                    }
                }
                return BadRequest("Invalid User Name");
            }
            return BadRequest(ModelState);
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult> EditProfile(EditProfileDTO profileDTO)
        {
            string UserName = User.Identity.Name;
            User UserDB = await userManager.FindByNameAsync(UserName);
            UserDB.Name = profileDTO.Name;
            UserDB.Location = profileDTO.Location;
            UserDB.ProfileImage = profileDTO.ProfileIamge;
            UserDB.CoverImage = profileDTO.CoverImage;
            UserDB.Bio = profileDTO.Bio;
            await userManager.UpdateAsync(UserDB);
            return Ok();

        }
    }
}

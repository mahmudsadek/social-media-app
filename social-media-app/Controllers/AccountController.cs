using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using social_media_app.DTOs;
using social_media_app.Models;
using social_media_app.Repository;
using social_media_app.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
//using System.Web.Http;

namespace social_media_app.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<User> userManager;
        private readonly IUserFollowerRepository userFollowerRepository;
        private readonly IConfiguration configuration;
        private readonly ISenderEmail senderEmail;

        public AccountController
            (
            UserManager<User> userManager, 
            IUserFollowerRepository userFollowerRepository ,
            IConfiguration configuration,
            ISenderEmail senderEmail
            )
        {
            this.userManager = userManager;
            this.userFollowerRepository = userFollowerRepository;
            this.configuration = configuration;
            this.senderEmail = senderEmail;
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
                    await SendConfirmationEmail(NewUser.Email, user);
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
        [HttpPatch]
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
        [HttpGet("suggestions")]
        public async Task<ActionResult> GetFollowersSuggestion(string id)
        {
            var Users = userManager.Users;
            List < UserFollower > Followers = userFollowerRepository.GetAll();
            List<FollowersSuggestionDTO> followersSuggestions = new();
            foreach (var user in Users)
            {
                if(user.Id != id) 
                {
                    bool followed = false;
                    foreach (var follower in Followers)
                    {
                        if (user.Id == follower.UserID && id == follower.FollowerID)
                            followed = true;
                    }
                    if(!followed)
                    {
                        FollowersSuggestionDTO follower = new() 
                        { 
                            Id = user.Id, Email = user.Email, UserName = user.UserName, 
                            UserImage = user.ProfileImage, CoverImage = user.CoverImage 
                        };

                        followersSuggestions.Add(follower);
                    }
                }
            }

            return Ok(followersSuggestions);
        }
        [HttpGet("follow")]
        public ActionResult FollowSomeone([FromQuery] string id, string SomeoneIWillFollowId) 
        {
            UserFollower userFollower = new() { UserID = SomeoneIWillFollowId, FollowerID = id };
            userFollowerRepository.Insert(userFollower);
            userFollowerRepository.Save();
            return NoContent();
        }

        [HttpGet("unfollow")]
        public ActionResult UnFollowSomeone([FromQuery] string id, string SomeoneIWillUnFollowId)
        {
            UserFollower userFollower = userFollowerRepository.Get(f => f.UserID == SomeoneIWillUnFollowId && f.FollowerID == id).FirstOrDefault();
            userFollowerRepository.Delete(userFollower);
            userFollowerRepository.Save();
            return NoContent();
        }

        [HttpGet("userId")]
        public async Task<ActionResult> GetUserId([FromQuery] string email)
        {
            User user = await userManager.FindByEmailAsync(email);
            if(user != null) 
            {
                string UserId = user.Id;
                return Ok(new
                {
                    status=200,
                    userId=UserId
                });
            }
            else
            {
                return NotFound(new
                {
                    status = 404,
                    message = "Invalid Email"
                });
            }
        }

        [HttpGet("ConfirmEmail")]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string UserId, string Token)
        {
            if (UserId == null || Token == null)
            {
                return BadRequest("The link is Invalid or Expired");
            }
            //Find the User By Id
            var user = await userManager.FindByIdAsync(UserId);
            if (user == null)
            {
                return BadRequest( $"The User ID {UserId} is Invalid");
                
            }
            //Call the ConfirmEmailAsync Method which will mark the Email as Confirmed
            var result = await userManager.ConfirmEmailAsync(user, Token);
            if (result.Succeeded)
            {
                return Ok("Thank you for confirming your email");
                
            }
            return Ok("Email confirm Successfuly");
        }
        private async Task SendConfirmationEmail(string? email, User? user)
        {
            //Generate the Token
            var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
            //Build the Email Confirmation Link which must include the Callback URL
            var ConfirmationLink = Url.Action("ConfirmEmail", "Account",
            new { UserId = user.Id, Token = token }, protocol: HttpContext.Request.Scheme);
            //Send the Confirmation Email to the User Email Id
            await senderEmail.SendEmailAsync(email, "Confirm Your Email", $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(ConfirmationLink)}'>clicking here</a>.", true);
        }
    }
}

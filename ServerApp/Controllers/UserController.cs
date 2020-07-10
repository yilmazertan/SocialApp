using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ServerApp.DTO;
using ServerApp.Model;

namespace ServerApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController:ControllerBase
    {
        private UserManager<User> _userManager;
       private SignInManager<User> _signinManager;

        public IConfiguration _configuration;

        public UserController(UserManager<User> userManager, SignInManager<User> signinManager,IConfiguration configuration)
        {
            _userManager=userManager;
            _signinManager=signinManager;
            _configuration = configuration;
        }


[HttpPost("register")]
        public async Task<ActionResult> Register(UserForRegisterDTO model){

                var user = new User{
                    UserName=model.UserName,
                    Email=model.Email,
                    Name=model.Name,
                    Gender=model.Gender
                };

                var result = await _userManager.CreateAsync(user,model.Password);

                if(result.Succeeded){
                    return StatusCode(201);
                }

                return BadRequest(result.Errors);

        }
[HttpPost("login")]
        public async Task<IActionResult> Login (UserForLoginDTO model){

            var user=await _userManager.FindByNameAsync(model.UserName);

            if(user==null){
                return BadRequest(new { message="kullanıcı bulunamadı"});
                            }
                            var result= await _signinManager.CheckPasswordSignInAsync(user,model.Password,false);

                            if(result.Succeeded){
                                    return Ok(new {
                                        token=TokenOlustur(user),
                                        username=user.UserName

                                    });
                            }

                            return Unauthorized();

        }

        private string TokenOlustur(User user)
        {
            var tokenhandler = new JwtSecurityTokenHandler();
            var key=Encoding.ASCII.GetBytes(_configuration.GetSection("AppSettings:Secret").Value);
           
            var tokenDescripter= new SecurityTokenDescriptor
            {
                Subject= new ClaimsIdentity(new Claim[]{
                    new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                    new Claim(ClaimTypes.Name,user.UserName.ToString())
                }),

                Expires=DateTime.UtcNow.AddDays(1),
                SigningCredentials= new SigningCredentials( new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)


            };

            var token= tokenhandler.CreateToken(tokenDescripter);
            return tokenhandler.WriteToken(token);

            

        }
    }
}
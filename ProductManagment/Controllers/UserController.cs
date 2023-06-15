using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using NSwag.Annotations;
using Product.Common.DTOs.Common;
using Product.Common.DTOs.Security;
using Product.Services.Contracts.Security;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ProductManagment.Controllers
{
    [Route("[controller]")]
    [ApiController]
    internal class UserController : BaseApiController
    {
        private readonly IConfiguration _config;
        private IUserBiz _userBiz;
        public UserController(IConfiguration config, IUserBiz userBiz)
        {
            _config = config;
            _userBiz = userBiz;
        }


        [HttpPost(Name = "Login")]
        [AllowAnonymous]
        public IActionResult Login()
        {
            var result = _userBiz.Login();

            //generate token
            return GetToken(result.Item1, result.Item2);
        }


        private IActionResult GetToken(UserDTO user, List<Claim> claims)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_config.GetSection("AppSettings:Secret").Value);
            var expires = DateTime.Now.AddDays(1);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = expires,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return Ok(new BaseResponse()
            {
                Succeed = true,
                Data = new
                {
                    User = user,
                    Token = tokenString,
                    ExpireDate = expires
                }
            });
        }


    }
}

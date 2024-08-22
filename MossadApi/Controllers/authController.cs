using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using MossadApi.Models;
namespace MossadApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class authController : ControllerBase
    {


        private string GenerateToken(string userIP)
        {
            // token handler can create token
            var tokenHandler = new JwtSecurityTokenHandler();

            string secretKey = "1234dyi5fjthgjdndfadsfgdsjfgj464twiyyd5ntyhgkdrue74hsf5ytsusefh55678"; //TODO: remove this from code
            byte[] key = Encoding.ASCII.GetBytes(secretKey);

            // token descriptor describe HOW to create the token
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                // things to include in the token
                Subject = new ClaimsIdentity(
                    new Claim[]
                    {
                new Claim(ClaimTypes.Name, userIP),
                    }
                ),

                Expires = DateTime.Now.AddMinutes(2),

                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature
                    )
            };

            // creating the token
            var token = tokenHandler.CreateToken(tokenDescriptor);
            // converting the token to string
            var tokenString = tokenHandler.WriteToken(token);

            return tokenString;

        }

        [HttpPost("login")]
        public IActionResult Login(Login loginObject)
        {

            if (loginObject.Username == "aaa" &&
                loginObject.Password == "123")
            {

                string userIP = HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();

                return StatusCode(200
                    , new { token = GenerateToken(userIP) }
                    );
            }
            return StatusCode(StatusCodes.Status401Unauthorized,
                    new { error = "invalid credentials" });
        }
    }
}

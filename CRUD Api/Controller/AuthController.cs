using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IConfiguration _config;

    public AuthController(IConfiguration config)
    {
        _config = config;
    }

    //[HttpPost("login")]
    //public IActionResult Login([FromBody] LoginDTO login)
    //{
    //    if (login.Username == "admin" && login.Password == "admin123") // hardcoded example
    //    {
    //        var claims = new[]
    //        {
    //            new Claim(ClaimTypes.Name, login.Username)
    //        };

    //            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
    //            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

    //            var token = new JwtSecurityToken(
    //                issuer: _config["Jwt:Issuer"],
    //                audience: _config["Jwt:Audience"],
    //                claims: claims,
    //                expires: DateTime.Now.AddMinutes(30),
    //                signingCredentials: creds
    //            );

    //            return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
    //        }

    //        return Unauthorized();
    //    }
    //}
    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginDTO login)
    {
        if (login.Username == "admin" && login.Password == "admin123")

        {
            var claims = new[] { new Claim(ClaimTypes.Name, login.Username) };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);

            return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
        }
        return Unauthorized();
    }
}

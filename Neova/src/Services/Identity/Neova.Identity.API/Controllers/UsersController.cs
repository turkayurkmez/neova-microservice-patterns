using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Neova.Identity.API.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;


namespace Neova.Identity.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        private readonly UserService _userService;

        public UsersController(UserService userService)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        [HttpPost]
        public IActionResult Login(UserLoginRequest userLoginRequest)
        {
            // Burada kullanıcıları veritabanından çekip dönebilirsiniz.
            // Örnek olarak basit bir liste döndürüyoruz.

            var user = _userService.ValidateUser(userLoginRequest.Username, userLoginRequest.Password);
            if (user == null)
            {
                return Unauthorized("Kullanıcı adı veya şifre yanlış.");
            }

            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("burasi_guvenlik_icin_cok_kritik_en_az_128_bit")); // Güvenlik anahtarınızı buraya girin
            SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName),
                new Claim(JwtRegisteredClaimNames.Email, user.Email ?? string.Empty),
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(ClaimTypes.Role, "Admin") // Kullanıcı rolü ekleniyor


            };
            var tokenOptions = new JwtSecurityToken(
                issuer: "Neova.Identity.API",
                audience: "catalog",
                claims: claims,
                expires: DateTime.Now.AddMinutes(30), // Token geçerlilik süresi
                signingCredentials: signingCredentials
            );


            return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(tokenOptions) });
        }


    }

    public record UserLoginRequest(string Username, string Password);
}

using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Security.Cryptography;
using MyStudentApiProject.DTO_s;
using Student.Business.Interfaces;
using Student.Data.Entities;

namespace MyStudentApiProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IstudenttService _Service;
        private readonly IRefreshTokenService _RefreshToken;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IstudenttService service, IRefreshTokenService refreshtoken, ILogger<AuthController> logger)
        {
            _Service = service;
            _RefreshToken = refreshtoken;
            _logger = logger;
        }


        private static string GenerateRefreshToken()
        {
            var bytes = new byte[64];

            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(bytes);

            return Convert.ToBase64String(bytes);
        }


        [HttpPost("Login")]
        [EnableRateLimiting("AuthLimiter")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO request)
        {

            Studentt student = await _Service.GetStudentByEmail(request.Email);

            if (student == null)
            {
               _logger.LogWarning("Failed Login Attempt (Email Not Found). Email={Email}, IP:14241", request.Email);
                return Unauthorized("Invalid Credentials. ");
            }

            bool IsPasswordValid = BCrypt.Net.BCrypt.Verify(request.Password, student.Password);

            if (!IsPasswordValid)
            {
                _logger.LogWarning("Failed Login Attempt (Email Not Found). Email={Email}, IP:14241", request.Email);

                return Unauthorized("Invalid Credentials. ");
            }

            var claims = new[]
            {
                    new Claim(ClaimTypes.NameIdentifier, student.Id.ToString()),
                    new Claim(ClaimTypes.Email , student.Email),
                    new Claim(ClaimTypes.Role, student.Role)
            };


            var secretkey = Environment.GetEnvironmentVariable("JWT_SECRET_KEY");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretkey));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "StudentApi",
                audience: "StudentApiUsers",
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds
                );


            var AccessToken = new JwtSecurityTokenHandler().WriteToken(token);

            var GeneratedRefreshToken = GenerateRefreshToken();

            RefreshToken refreshToken = new RefreshToken
            {
                Token = BCrypt.Net.BCrypt.HashPassword(GeneratedRefreshToken),
                UserID = student.Id,
                CreatedAt = DateTime.Now,
                ExpiresAt = DateTime.Now.AddDays(7),
                Revoked = false,
                Used = false
            };

             await _RefreshToken.AddNewRefreshToken(refreshToken);

            return Ok(new TokenResponse
            {
                AccessToken = AccessToken,
                RefreshToken = GeneratedRefreshToken
            });

        }

        [HttpPost("refresh")]
        [EnableRateLimiting("AuthLimiter")]
        public async Task<IActionResult> Refresh([FromBody] RefreshRequest request)
        {
            Studentt student = await _Service.GetStudentByEmail(request.Email);


            if (student == null)
            {
                _logger.LogWarning("Invalid refresh attempt (email not found). Email={Email}, IP=123432", request.Email);
                return Unauthorized("Invalid refresh request emm");
            }

            RefreshToken refreshToken = await _RefreshToken.GetRefreshTokenAsync(student.Id);

            if(refreshToken == null)
                return Unauthorized("Invalid refresh request");

            if (refreshToken.ExpiresAt <= DateTime.UtcNow)
            {

                _logger.LogWarning("Token Expired (). Email={Email}, IP=123432", request.Email);
                return Unauthorized("Refresh token expired");
            }

            bool refreshValid = BCrypt.Net.BCrypt.Verify(request.RefreshToken, refreshToken.Token);
            if (!refreshValid)
            {

                _logger.LogWarning("Ivalid Refresh Token (). Email={Email}, IP=123432", request.Email);
                return Unauthorized("Invalid refresh token");
            }



            var claims = new[]
            {
                     new Claim(ClaimTypes.NameIdentifier, student.Id.ToString()),
                     new Claim(ClaimTypes.Email, student.Email),
                     new Claim(ClaimTypes.Role, student.Role)
            };


            var secretkey = Environment.GetEnvironmentVariable("JWT_SECRET_KEY");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretkey));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var jwt = new JwtSecurityToken(
                issuer: "StudentApi",
                audience: "StudentApiUsers",
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(30),
                signingCredentials: creds
            );

            var newAccessToken = new JwtSecurityTokenHandler().WriteToken(jwt);

            
            var GeneratedRefreshToken = GenerateRefreshToken();
            var NewRefreshToken = new RefreshToken
            {
                Token = BCrypt.Net.BCrypt.HashPassword(GeneratedRefreshToken),
                UserID = refreshToken.UserID,
                CreatedAt = DateTime.Now,
                ExpiresAt = DateTime.Now.AddDays(7),
                Revoked = false,
                Used = false
            };

            await _RefreshToken.AddNewRefreshToken(NewRefreshToken);

            refreshToken.Revoked = true;
            refreshToken.Used = true;

            await _RefreshToken.UpdateRefreshToken(refreshToken);

            return Ok(new TokenResponse
            {
                AccessToken = newAccessToken,
                RefreshToken = GeneratedRefreshToken
            });
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout([FromBody] LogoutRequest request)
        {
            List<Studentt> students = await _Service.GetAllStudentsAsync();
            var student = students.FirstOrDefault(x => x.Email == request.Email);

            if (student == null)
                return Ok(); // Do not reveal if user exists

            RefreshToken refreshToken = await _RefreshToken.GetRefreshTokenAsync(student.Id);

            if (refreshToken == null)
                return Unauthorized("Invalid refresh request");

            bool refreshValid = BCrypt.Net.BCrypt.Verify(request.RefreshToken, refreshToken.Token);
            if (!refreshValid)
                return Ok();

            refreshToken.Revoked = true;
            await _RefreshToken.UpdateRefreshToken(refreshToken);

            return Ok("Logged out successfully");
        }
    }
}


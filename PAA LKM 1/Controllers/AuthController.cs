using Dapper;
using Microsoft.AspNetCore.Mvc;
using PAA_LKM_1.Data;
using PAA_LKM_1.DTOs;
using PAA_LKM_1.Helpers;

namespace PAA_LKM_1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ConnectionFactory _factory;

        public AuthController(ConnectionFactory factory)
        {
            _factory = factory;
        }

        // REGISTER
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            using var conn = _factory.CreateConnection();

            if (string.IsNullOrEmpty(dto.Email) || string.IsNullOrEmpty(dto.Password))
            {
                return BadRequest(ApiResponse<object>.Error("Data tidak lengkap"));
            }

            // cek email sudah ada
            var existing = await conn.QueryFirstOrDefaultAsync(
                "SELECT * FROM users WHERE email = @email",
                new { email = dto.Email });

            if (existing != null)
            {
                return BadRequest(ApiResponse<object>.Error("Email sudah digunakan"));
            }

            await conn.ExecuteAsync(
                @"INSERT INTO users (name, email, password)
                  VALUES (@Name, @Email, @Password)",
                dto);

            return Ok(ApiResponse<object>.Success(null, "Register berhasil"));
        }

        // LOGIN
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            using var conn = _factory.CreateConnection();

            if (string.IsNullOrEmpty(dto.Email) || string.IsNullOrEmpty(dto.Password))
            {
                return BadRequest(ApiResponse<object>.Error("Email dan password wajib diisi"));
            }

            var user = await conn.QueryFirstOrDefaultAsync(
                @"SELECT * FROM users 
                  WHERE email = @Email AND password = @Password",
                dto);

            if (user == null)
            {
                return Unauthorized(ApiResponse<object>.Error("Email atau password salah"));
            }

            return Ok(ApiResponse<object>.Success(user, "Login berhasil"));
        }
    }
}
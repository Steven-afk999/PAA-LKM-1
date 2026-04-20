using Microsoft.AspNetCore.Mvc;
using PAA_LKM_1.Helpers;

namespace PAA_LKM_1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        [HttpPost("login")]
        public IActionResult Login()
        {
            return Ok(ApiResponse<object>.Success(null, "Login berhasil (dummy)"));
        }

        [HttpPost("register")]
        public IActionResult Register()
        {
            return Ok(ApiResponse<object>.Success(null, "Register berhasil (dummy)"));
        }
    }
}
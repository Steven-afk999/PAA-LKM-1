using Dapper;
using Microsoft.AspNetCore.Mvc;
using PAA_LKM_1.Data;
using PAA_LKM_1.Helpers;

namespace PAA_LKM_1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private readonly ConnectionFactory _factory;

        public GamesController(ConnectionFactory factory)
        {
            _factory = factory;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            using var conn = _factory.CreateConnection();

            var data = await conn.QueryAsync("SELECT * FROM games");

            return Ok(ApiResponse<object>.Success(data, "Berhasil ambil data game"));
        }
    }
}
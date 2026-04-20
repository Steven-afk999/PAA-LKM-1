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

        // GET ALL + jumlah produk
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            using var conn = _factory.CreateConnection();

            var query = @"SELECT g.id, g.name,
                          COUNT(p.id) AS total_products
                          FROM games g
                          LEFT JOIN products p ON p.game_id = g.id
                          GROUP BY g.id, g.name
                          ORDER BY g.id";

            var data = await conn.QueryAsync(query);

            return Ok(ApiResponse<object>.Success(data, "Berhasil ambil data game"));
        }

        // GET BY ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            using var conn = _factory.CreateConnection();

            var data = await conn.QueryFirstOrDefaultAsync(
                "SELECT * FROM games WHERE id = @id",
                new { id });

            if (data == null)
                return NotFound(ApiResponse<object>.Error("Game tidak ditemukan"));

            return Ok(ApiResponse<object>.Success(data));
        }
    }
}
using Dapper;
using Microsoft.AspNetCore.Mvc;
using PAA_LKM_1.Data;
using PAA_LKM_1.Helpers;

namespace PAA_LKM_1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ConnectionFactory _factory;

        public ProductsController(ConnectionFactory factory)
        {
            _factory = factory;
        }

        // GET ALL + join game
        [HttpGet]
        public async Task<IActionResult> GetAll(int? game_id)
        {
            using var conn = _factory.CreateConnection();

            var query = @"SELECT p.id, p.name, p.price, g.name AS game_name
                          FROM products p
                          JOIN games g ON p.game_id = g.id";

            // FILTER
            if (game_id.HasValue)
            {
                query += " WHERE p.game_id = @game_id";
            }

            var data = await conn.QueryAsync(query, new { game_id });

            return Ok(ApiResponse<object>.Success(data, "Berhasil ambil produk"));
        }

        // GET BY ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            using var conn = _factory.CreateConnection();

            var data = await conn.QueryFirstOrDefaultAsync(
                @"SELECT p.id, p.name, p.price, g.name AS game_name
                  FROM products p
                  JOIN games g ON p.game_id = g.id
                  WHERE p.id = @id",
                new { id });

            if (data == null)
                return NotFound(ApiResponse<object>.Error("Produk tidak ditemukan"));

            return Ok(ApiResponse<object>.Success(data));
        }
    }
}
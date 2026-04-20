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

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            using var conn = _factory.CreateConnection();

            var query = @"SELECT p.id, p.name, p.price, g.name as game_name
                          FROM products p
                          JOIN games g ON p.game_id = g.id";

            var data = await conn.QueryAsync(query);

            return Ok(ApiResponse<object>.Success(data, "Berhasil ambil produk"));
        }
    }
}
using Dapper;
using Microsoft.AspNetCore.Mvc;
using PAA_LKM_1.Data;
using PAA_LKM_1.DTOs;
using PAA_LKM_1.Helpers;

namespace PAA_LKM_1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly ConnectionFactory _factory;

        public TransactionsController(ConnectionFactory factory)
        {
            _factory = factory;
        }

        // GET ALL
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            using var conn = _factory.CreateConnection();

            var query = @"SELECT t.id, u.name as UserName, p.name as ProductName,
                          t.quantity, t.total_price as TotalPrice
                          FROM transactions t
                          JOIN users u ON t.user_id = u.id
                          JOIN products p ON t.product_id = p.id";

            var data = await conn.QueryAsync<TransactionResponseDto>(query);

            return Ok(ApiResponse<object>.Success(data, "Berhasil ambil data"));
        }

        // GET BY ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            using var conn = _factory.CreateConnection();

            var data = await conn.QueryFirstOrDefaultAsync<TransactionResponseDto>(
                @"SELECT t.id, u.name as UserName, p.name as ProductName,
                  t.quantity, t.total_price as TotalPrice
                  FROM transactions t
                  JOIN users u ON t.user_id = u.id
                  JOIN products p ON t.product_id = p.id
                  WHERE t.id = @id", new { id });

            if (data == null)
                return NotFound(ApiResponse<object>.Error("Data tidak ditemukan"));

            return Ok(ApiResponse<object>.Success(data));
        }

        // POST
        [HttpPost]
        public async Task<IActionResult> Create(TransactionCreateDto dto)
        {
            using var conn = _factory.CreateConnection();

            var price = await conn.QuerySingleAsync<int>(
                "SELECT price FROM products WHERE id = @id",
                new { id = dto.ProductId });

            var total = price * dto.Quantity;

            await conn.ExecuteAsync(
                @"INSERT INTO transactions (user_id, product_id, quantity, total_price)
                  VALUES (@UserId, @ProductId, @Quantity, @TotalPrice)",
                new
                {
                    dto.UserId,
                    dto.ProductId,
                    dto.Quantity,
                    TotalPrice = total
                });

            return StatusCode(201, ApiResponse<object>.Success(null, "Transaksi berhasil dibuat"));
        }

        // PUT
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, TransactionUpdateDto dto)
        {
            using var conn = _factory.CreateConnection();

            var price = await conn.QuerySingleAsync<int>(
                "SELECT price FROM products WHERE id = @id",
                new { id = dto.ProductId });

            var total = price * dto.Quantity;

            var result = await conn.ExecuteAsync(
                @"UPDATE transactions
                  SET user_id = @UserId,
                      product_id = @ProductId,
                      quantity = @Quantity,
                      total_price = @TotalPrice,
                      updated_at = CURRENT_TIMESTAMP
                  WHERE id = @Id",
                new
                {
                    Id = id,
                    dto.UserId,
                    dto.ProductId,
                    dto.Quantity,
                    TotalPrice = total
                });

            if (result == 0)
                return NotFound(ApiResponse<object>.Error("Data tidak ditemukan"));

            return Ok(ApiResponse<object>.Success(null, "Berhasil diupdate"));
        }

        // DELETE
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            using var conn = _factory.CreateConnection();

            var result = await conn.ExecuteAsync(
                "DELETE FROM transactions WHERE id = @id",
                new { id });

            if (result == 0)
                return NotFound(ApiResponse<object>.Error("Data tidak ditemukan"));

            return Ok(ApiResponse<object>.Success(null, "Berhasil dihapus"));
        }
    }
}
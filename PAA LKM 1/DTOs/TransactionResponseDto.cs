namespace PAA_LKM_1.DTOs
{
    public class TransactionResponseDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public int TotalPrice { get; set; }
    }
}
namespace PAA_LKM_1.Helpers
{
    public class ApiResponse<T>
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }

        public static ApiResponse<T> Success(T data, string message = "Success")
        {
            return new ApiResponse<T>
            {
                Status = "success",
                Message = message,
                Data = data
            };
        }

        public static ApiResponse<T> Error(string message)
        {
            return new ApiResponse<T>
            {
                Status = "error",
                Message = message
            };
        }
    }
}
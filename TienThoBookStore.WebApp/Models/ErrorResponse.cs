namespace TienThoBookStore.WebApp.Models
{
    public class ErrorResponse
    {
        public string Message { get; set; } = "";
        public bool CanResend { get; set; }
    }
}

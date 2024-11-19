namespace StockApp.Application.DTOs
{
    public class TokenResponseDTO
    {
        public string Token { get; set; } = string.Empty;
        public DateTime ExpiredAt { get; set; } = new DateTime();

    }
}

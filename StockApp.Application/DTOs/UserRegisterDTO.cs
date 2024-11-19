using StockApp.Domain.Enums;

namespace StockApp.Application.DTOs
{
    public class UserRegisterDTO
    {
        public string NomeDoUsuario { get; set; } = string.Empty;
        public string Senha { get; set; } = string.Empty;
        public ERole Role { get; set; } = ERole.User;
        public string Email { get; set; } = string.Empty;

    }
}

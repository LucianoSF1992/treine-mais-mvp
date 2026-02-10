namespace TreineMais.Api.DTOs
{
    public class RegisterRequestDto
    {
        public string? Nome { get; set; }
        public string? Email { get; set; }
        public string? Senha { get; set; }
        public string? Tipo { get; set; }
    }
}

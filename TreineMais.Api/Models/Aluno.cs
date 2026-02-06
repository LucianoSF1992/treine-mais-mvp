namespace TreineMais.Api.Models
{
    public class Aluno
    {
        public int Id { get; set; }

        // FK para User
        public int UserId { get; set; }
        public User? User { get; set; }

        // FK para Instrutor (User)
        public int InstrutorId { get; set; }

        public int? Idade { get; set; }
        public string? Objetivo { get; set; }
    }
}

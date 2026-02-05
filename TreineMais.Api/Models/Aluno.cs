namespace TreineMais.Api.Models
{
    public class Aluno
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int InstrutorId { get; set; }
        public int? Idade { get; set; }
        public string Objetivo { get; set; }
    }
}

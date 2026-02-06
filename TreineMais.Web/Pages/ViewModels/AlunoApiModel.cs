namespace TreineMais.Web.ViewModels
{
    public class AlunoApiModel
    {
        public int Id { get; set; }
        public int? Idade { get; set; }
        public string? Objetivo { get; set; }
        public UserApiModel User { get; set; } = new();
    }

    public class UserApiModel
    {
        public string? Nome { get; set; }
        public string? Email { get; set; }
    }
}

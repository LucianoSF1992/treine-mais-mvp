namespace TreineMais.Api.DTOs.Treinos;

public class CreateTreinoDto
{
    public string Nome { get; set; } = "";
    public string DiaSemana { get; set; } = "";
    public int AlunoId { get; set; }
}
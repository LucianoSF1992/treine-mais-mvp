public class TreinoDto
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string DiaSemana { get; set; }
    public List<ExercicioDto> Exercicios { get; set; }
}
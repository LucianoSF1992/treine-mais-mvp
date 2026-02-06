using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TreineMais.Web.Pages.Aluno
{
    public class IndexModel : PageModel
    {
        public string NomeAluno { get; set; } = "Aluno MVP";
        public string DiaSemana { get; set; } = DateTime.Now.DayOfWeek.ToString();

        [BindProperty]
        public List<ExercicioViewModel> Exercicios { get; set; } = new();

        public string Resumo =>
            $"{Exercicios.Count(e => e.Concluido)} de {Exercicios.Count} exercícios concluídos hoje 💪";

        public void OnGet()
        {
            // MVP — dados mockados
            Exercicios = new List<ExercicioViewModel>
            {
                new() {
                    Nome = "Supino reto",
                    GrupoMuscular = "Peito",
                    Series = 4,
                    Repeticoes = 10,
                    Descanso = 60
                },
                new() {
                    Nome = "Agachamento",
                    GrupoMuscular = "Pernas",
                    Series = 4,
                    Repeticoes = 12,
                    Descanso = 90
                }
            };
        }

        public void OnPost()
        {
            // MVP: apenas mantém o estado do formulário
        }
    }

    public class ExercicioViewModel
    {
        public string Nome { get; set; } = "";
        public string GrupoMuscular { get; set; } = "";
        public int Series { get; set; }
        public int Repeticoes { get; set; }
        public int Descanso { get; set; }
        public bool Concluido { get; set; }
    }
}

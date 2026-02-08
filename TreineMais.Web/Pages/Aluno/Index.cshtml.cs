using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Json;
using TreineMais.Web.ViewModels;



namespace TreineMais.Web.Pages.Aluno
{
    public class IndexModel : PageModel
    {
        public int AlunoId { get; set; } = 1; // MVP fixo

        public string NomeAluno { get; set; } = "Aluno MVP";
        public string DiaSemana { get; set; } = DateTime.Now.DayOfWeek.ToString();

        [BindProperty]
        public List<ExercicioViewModel> Exercicios { get; set; } = new();

        public List<HistoricoViewModel> Historico { get; set; } = new();


        public string Resumo =>
            $"{Exercicios.Count(e => e.Concluido)} de {Exercicios.Count} exercícios concluídos hoje 💪";

        public async Task OnGet()
        {
            // MVP — dados mockados COM ID
            Exercicios = new List<ExercicioViewModel>
            {
                new() {
                    Id = 1,
                    Nome = "Supino reto",
                    GrupoMuscular = "Peito",
                    Series = 4,
                    Repeticoes = 10,
                    Descanso = 60
                },
                new() {
                    Id = 2,
                    Nome = "Agachamento",
                    GrupoMuscular = "Pernas",
                    Series = 4,
                    Repeticoes = 12,
                    Descanso = 90
                }
            };

            using var http = new HttpClient();

            var historicoApi = await http.GetFromJsonAsync<List<HistoricoViewModel>>(
                "http://localhost:5129/api/historico/1");

            if (historicoApi != null)
                Historico = historicoApi;

        }

        public void OnPost()
        {
            // MVP: mantém estado do formulário
        }
    }
}

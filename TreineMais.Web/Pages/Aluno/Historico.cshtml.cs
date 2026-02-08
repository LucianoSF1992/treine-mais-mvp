using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Json;

namespace TreineMais.Web.Pages.Aluno
{
    public class HistoricoModel : PageModel
    {
        private readonly HttpClient _httpClient;

        public HistoricoModel(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient();
        }

        public List<HistoricoViewModel> Historico { get; set; } = new();

        public async Task OnGetAsync()
        {
            int alunoId = 1; // MVP fixo por enquanto

            var response = await _httpClient.GetAsync(
                $"http://localhost:5129/api/exercicio-conclusoes/aluno/{alunoId}");

            if (response.IsSuccessStatusCode)
            {
                Historico = await response.Content
                    .ReadFromJsonAsync<List<HistoricoViewModel>>() ?? new();
            }
        }
    }

    public class HistoricoViewModel
    {
        public string NomeExercicio { get; set; } = "";
        public string GrupoMuscular { get; set; } = "";
        public DateTime DataExecucao { get; set; }
        public bool Concluido { get; set; }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Json;
using TreineMais.Web.DTOs;

namespace TreineMais.Web.Pages.Aluno
{
    public class MeuTreinoModel : PageModel
    {
        private readonly HttpClient _http;

        public MeuTreinoModel(IHttpClientFactory factory)
        {
            _http = factory.CreateClient("Api");
        }

        public List<TreinoDto> Treinos { get; set; } = new();
        public List<ExercicioTreinoDto> Exercicios { get; set; } = new();

        public int AlunoId =>
            int.Parse(User.FindFirst("UserId")?.Value ?? "0");

        // ===============================
        // CARREGAR TREINOS
        // ===============================
        public async Task OnGetAsync()
        {
            // Buscar todos treinos do aluno
            Treinos = await _http.GetFromJsonAsync<List<TreinoDto>>(
                $"api/treinos/aluno/{AlunoId}"
            ) ?? new List<TreinoDto>();

            // Se existir treino → carrega exercícios do primeiro
            if (Treinos.Any())
            {
                var treinoId = Treinos.First().Id;

                Exercicios = await _http.GetFromJsonAsync<List<ExercicioTreinoDto>>(
                    $"api/exercicios/treino/{treinoId}/aluno/{AlunoId}"
                ) ?? new List<ExercicioTreinoDto>();
            }
        }

        // ===============================
        // CONCLUIR EXERCICIO
        // ===============================
        public async Task<IActionResult> OnPostConcluirAsync(int exercicioId)
        {
            var dto = new
            {
                AlunoId = AlunoId,
                ExercicioId = exercicioId
            };

            await _http.PostAsJsonAsync("api/exercicio-conclusoes", dto);

            return RedirectToPage();
        }
    }
}
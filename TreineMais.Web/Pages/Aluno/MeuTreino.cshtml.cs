using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Json;

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

        public int AlunoId => int.Parse(User.FindFirst("UserId").Value);

        // ===============================
        // CARREGAR TREINOS
        // ===============================
        public async Task OnGetAsync()
        {
            Treinos = await _http.GetFromJsonAsync<List<TreinoDto>>(
                $"api/treinos/aluno/{AlunoId}");

            Exercicios = await _http.GetFromJsonAsync<List<ExercicioTreinoDto>>(
    $"api/exercicios/treino/{TreinoId}/aluno/{AlunoId}");
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
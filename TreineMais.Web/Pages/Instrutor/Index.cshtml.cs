using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Json;

namespace TreineMais.Web.Pages.Instrutor
{
    public class IndexModel : PageModel
    {
        public List<AlunoViewModel> Alunos { get; set; } = new();

        // MVP: instrutor fixo (depois virá do login/JWT)
        private const int InstrutorId = 2;
        private const string ApiBaseUrl = "http://localhost:5129";

        public async Task OnGetAsync()
        {
            var client = new HttpClient();

            var dados = await client.GetFromJsonAsync<List<AlunoApiModel>>(
                $"{ApiBaseUrl}/api/alunos/instrutor/{InstrutorId}"
            );

            if (dados == null) return;

            Alunos = dados.Select(a => new AlunoViewModel
            {
                Id = a.Id,
                Nome = a.User.Nome,
                Email = a.User.Email,
                Idade = a.Idade,
                Objetivo = a.Objetivo
            }).ToList();
        }
    }

    // ===== ViewModels =====
    public class AlunoViewModel
    {
        public int Id { get; set; }
        public string? Nome { get; set; }
        public string? Email { get; set; }
        public int? Idade { get; set; }
        public string? Objetivo { get; set; }
    }

    // ===== API Model =====
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

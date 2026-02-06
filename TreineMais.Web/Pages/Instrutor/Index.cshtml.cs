using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Json;
using TreineMais.Web.ViewModels;


namespace TreineMais.Web.Pages.Instrutor
{
    public class IndexModel : PageModel
    {
        public List<AlunoViewModel> Alunos { get; set; } = new();

        private const int InstrutorId = 2; // MVP
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
}

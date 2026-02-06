using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Json;
using TreineMais.Web.ViewModels;

namespace TreineMais.Web.Pages.Instrutor
{
    public class DeleteModel : PageModel
    {
        [BindProperty] public int Id { get; set; }
        public string? Nome { get; set; }

        private const string ApiBaseUrl = "http://localhost:5129";

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var client = new HttpClient();

            var aluno = await client.GetFromJsonAsync<AlunoApiModel>(
                $"{ApiBaseUrl}/api/alunos/{id}"
            );

            if (aluno == null)
                return RedirectToPage("Index");

            Id = aluno.Id;
            Nome = aluno.User.Nome;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var client = new HttpClient();

            var response = await client.DeleteAsync(
                $"{ApiBaseUrl}/api/alunos/{Id}"
            );

            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError("", "Erro ao excluir aluno.");
                return Page();
            }

            return RedirectToPage("Index");
        }
    }
}

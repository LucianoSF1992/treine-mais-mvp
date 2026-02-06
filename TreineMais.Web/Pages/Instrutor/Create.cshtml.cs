using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Json;

namespace TreineMais.Web.Pages.Instrutor
{
    public class CreateModel : PageModel
    {
        [BindProperty] public string? Nome { get; set; }
        [BindProperty] public string? Email { get; set; }
        [BindProperty] public string? Senha { get; set; }
        [BindProperty] public int? Idade { get; set; }
        [BindProperty] public string? Objetivo { get; set; }

        // MVP: instrutor fixo (depois virá do login)
        private const int InstrutorId = 2;
        private const string ApiBaseUrl = "http://localhost:5129";

        public async Task<IActionResult> OnPostAsync()
        {
            if (string.IsNullOrEmpty(Nome) ||
                string.IsNullOrEmpty(Email) ||
                string.IsNullOrEmpty(Senha))
            {
                ModelState.AddModelError("", "Nome, Email e Senha são obrigatórios.");
                return Page();
            }

            var client = new HttpClient();

            var payload = new
            {
                InstrutorId = InstrutorId,
                Idade = Idade,
                Objetivo = Objetivo,
                User = new
                {
                    Nome = Nome,
                    Email = Email,
                    Senha = Senha
                }
            };

            var response = await client.PostAsJsonAsync(
                $"{ApiBaseUrl}/api/alunos",
                payload
            );

            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError("", "Erro ao cadastrar aluno.");
                return Page();
            }

            return RedirectToPage("Index");
        }
    }
}

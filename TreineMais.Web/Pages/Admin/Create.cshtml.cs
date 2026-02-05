using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Json;

namespace TreineMais.Web.Pages.Admin
{
    public class CreateModel : PageModel
    {
        [BindProperty]
        public string? Nome { get; set; }

        [BindProperty]
        public string? Email { get; set; }

        [BindProperty]
        public string? Senha { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (string.IsNullOrEmpty(Nome) ||
                || string.IsNullOrEmpty(Email)
                string.IsNullOrEmpty(Senha))
            {
                ModelState.AddModelError("", "Todos os campos são obrigatórios.");
                return Page();
            }

            var client = new HttpClient();

            var response = await client.PostAsJsonAsync(
                "http://localhost:5129/api/instrutores",
                new
                {
                    Nome = Nome,
                    Email = Email,
                    Senha = Senha
                }
            );

            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError("", "Erro ao cadastrar instrutor.");
                return Page();
            }

            return RedirectToPage("Index");
        }
    }
}

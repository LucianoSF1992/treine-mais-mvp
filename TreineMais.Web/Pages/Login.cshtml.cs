using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Json;

namespace TreineMais.Web.Pages
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public string? Email { get; set; }

        [BindProperty]
        public string? Senha { get; set; }

        public string? MensagemErro { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            var client = new HttpClient();

            var response = await client.PostAsJsonAsync(
                "http://localhost:5000/api/auth/login",
                new { Email, Senha }
            );

            if (!response.IsSuccessStatusCode)
            {
                MensagemErro = "Email ou senha inválidos.";
                return Page();
            }

            var usuario = await response.Content.ReadFromJsonAsync<dynamic>();

            if (usuario.tipo == "ADMIN")
                return RedirectToPage("/Admin/Index");

            if (usuario.tipo == "INSTRUTOR")
                return RedirectToPage("/Instrutor/Index");

            return RedirectToPage("/Aluno/Index");
        }
    }
}

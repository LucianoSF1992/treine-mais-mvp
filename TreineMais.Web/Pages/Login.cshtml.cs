using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TreineMais.Web.DTOs;

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
            if (string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Senha))
            {
                MensagemErro = "Informe email e senha.";
                return Page();
            }

            var client = new HttpClient();

            var response = await client.PostAsJsonAsync(
                "http://localhost:5129/api/auth/login",
                new { Email, Senha }
            );

            if (!response.IsSuccessStatusCode)
            {
                MensagemErro = "Email ou senha inválidos.";
                return Page();
            }

            var usuario = await response.Content.ReadFromJsonAsync<LoginResponseDto>();

            if (usuario == null || string.IsNullOrEmpty(usuario.Tipo))
            {
                MensagemErro = "Erro ao processar login.";
                return Page();
            }

            if (usuario.Tipo == "ADMIN")
                return RedirectToPage("/Admin/Index");

            if (usuario.Tipo == "INSTRUTOR")
                return RedirectToPage("/Instrutor/Index");

            return RedirectToPage("/Aluno/Index");
        }
    }
}

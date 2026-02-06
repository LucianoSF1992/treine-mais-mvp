using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Json;

namespace TreineMais.Web.Pages.Instrutor
{
    public class EditModel : PageModel
    {
        [BindProperty] public int Id { get; set; }

        [BindProperty] public string? Nome { get; set; }
        [BindProperty] public string? Email { get; set; }
        [BindProperty] public string? Senha { get; set; }
        [BindProperty] public int? Idade { get; set; }
        [BindProperty] public string? Objetivo { get; set; }

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
            Email = aluno.User.Email;
            Idade = aluno.Idade;
            Objetivo = aluno.Objetivo;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (string.IsNullOrEmpty(Nome) ||
                string.IsNullOrEmpty(Email))
            {
                ModelState.AddModelError("", "Nome e Email são obrigatórios.");
                return Page();
            }

            var client = new HttpClient();

            var payload = new
            {
                Idade = Idade,
                Objetivo = Objetivo,
                User = new
                {
                    Nome = Nome,
                    Email = Email,
                    Senha = Senha
                }
            };

            var response = await client.PutAsJsonAsync(
                $"{ApiBaseUrl}/api/alunos/{Id}",
                payload
            );

            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError("", "Erro ao atualizar aluno.");
                return Page();
            }

            return RedirectToPage("Index");
        }
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

using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TreineMais.Web.DTOs;

namespace TreineMais.Web.Pages.Admin
{
    public class EditModel : PageModel
    {
        [BindProperty]
        public int Id { get; set; }

        [BindProperty]
        public string? Nome { get; set; }

        [BindProperty]
        public string? Email { get; set; }

        [BindProperty]
        public string? Senha { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var client = new HttpClient();

            var instrutor = await client.GetFromJsonAsync<LoginResponseDto>(
                $"http://localhost:5129/api/instrutores/{id}"
            );

            if (instrutor == null)
                return RedirectToPage("Index");

            Id = instrutor.Id;
            Nome = instrutor.Nome;
            Email = instrutor.Email;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (string.IsNullOrEmpty(Nome) || string.IsNullOrEmpty(Email))
            {
                ModelState.AddModelError("", "Nome e Email são obrigatórios.");
                return Page();
            }

            var client = new HttpClient();

            var response = await client.PutAsJsonAsync(
                $"http://localhost:5129/api/instrutores/{Id}",
                new
                {
                    Nome,
                    Email,
                    Senha,
                }
            );

            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError("", "Erro ao atualizar instrutor.");
                return Page();
            }

            return RedirectToPage("Index");
        }
    }
}

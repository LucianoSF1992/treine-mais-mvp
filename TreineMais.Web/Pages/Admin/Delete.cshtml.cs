using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Json;
using TreineMais.Web.DTOs;

namespace TreineMais.Web.Pages.Admin
{
    public class DeleteModel : PageModel
    {
        [BindProperty]
        public int Id { get; set; }

        public string? Nome { get; set; }
        public string? Email { get; set; }

        private const string ApiBaseUrl = "http://localhost:5129";

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var client = new HttpClient();

            var instrutor = await client.GetFromJsonAsync<LoginResponseDto>(
                $"{ApiBaseUrl}/api/instrutores/{id}"
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
            var client = new HttpClient();

            var response = await client.DeleteAsync(
                $"{ApiBaseUrl}/api/instrutores/{Id}"
            );

            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError("", "Erro ao excluir instrutor.");
                return Page();
            }

            return RedirectToPage("Index");
        }
    }
}

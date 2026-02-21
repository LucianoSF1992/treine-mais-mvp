using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Json;

namespace TreineMais.Web.Pages.Instrutor
{
    public class TreinosModel : PageModel
    {
        private readonly HttpClient _http;

        public TreinosModel(IHttpClientFactory factory)
        {
            _http = factory.CreateClient("Api");
        }

        [BindProperty]
        public string Nome { get; set; } = "";

        [BindProperty]
        public string DiaSemana { get; set; } = "";

        [BindProperty]
        public int AlunoId { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            var dto = new
            {
                Nome,
                DiaSemana,
                AlunoId
            };

            await _http.PostAsJsonAsync("api/treinos", dto);

            return RedirectToPage("/Instrutor");
        }
    }
}
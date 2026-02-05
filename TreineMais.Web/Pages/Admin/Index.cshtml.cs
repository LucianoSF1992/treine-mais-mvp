using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TreineMais.Web.DTOs;

namespace TreineMais.Web.Pages.Admin
{
    public class IndexModel : PageModel
    {
        public List<LoginResponseDto> Instrutores { get; set; } = new();

        public async Task OnGetAsync()
        {
            var client = new HttpClient();
            var dados = await client.GetFromJsonAsync<List<LoginResponseDto>>(
                "http://localhost:5129/api/instrutores"
            );

            if (dados != null)
                Instrutores = dados;
        }
    }
}

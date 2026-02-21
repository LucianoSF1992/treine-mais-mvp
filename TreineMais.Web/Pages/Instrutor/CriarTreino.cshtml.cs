using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

public class CriarTreinoModel : PageModel
{
    private readonly HttpClient _http;

    public CriarTreinoModel(IHttpClientFactory factory)
    {
        _http = factory.CreateClient("Api");
    }

    [BindProperty]
    public string Nome { get; set; } = "";

    [BindProperty]
    public string? Descricao { get; set; }

    [BindProperty]
    public int AlunoId { get; set; }

    public List<SelectListItem> AlunosSelect { get; set; } = new();

    public async Task OnGet()
    {
        var alunos = await _http.GetFromJsonAsync<List<AlunoDto>>("api/alunos");

        AlunosSelect = alunos!
            .Select(a => new SelectListItem
            {
                Value = a.Id.ToString(),
                Text = a.Nome
            }).ToList();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var dto = new
        {
            Nome,
            Descricao,
            AlunoId,
            DiaSemana = "Segunda",
            Exercicios = new List<object>()
        };

        await _http.PostAsJsonAsync("api/treinos/completo", dto);

        return RedirectToPage("Index");
    }
}
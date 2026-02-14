using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

public class AlterarSenhaModel : PageModel
{
    [BindProperty]
    public string SenhaAtual { get; set; }

    [BindProperty]
    public string NovaSenha { get; set; }

    public string Mensagem { get; set; }

    public void OnPost()
    {
        // depois vamos integrar com API
        Mensagem = "Senha alterada (mock)";
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SiteContagem.Logging;

namespace SiteContagem.Pages;

public class IndexModel : PageModel
{
    private static readonly Contador _CONTADOR = new();

    public void OnGet([FromServices] ILogger<IndexModel> logger,
        [FromServices] IConfiguration configuration)
    {
        int valorAtual;
        lock (_CONTADOR)
        {
            _CONTADOR.Incrementar();
            valorAtual = _CONTADOR.ValorAtual;
        }

        logger.LogValorAtual(valorAtual);

        // FIXME: trecho criado para simular falhas e permitir
        // a análise de problemas com a ferramenta k6s
        if (valorAtual % 4 == 0)
            throw new Exception("Simulação de falha");

        TempData["Contador"] = valorAtual;
        TempData["Local"] = _CONTADOR.Local;
        TempData["Kernel"] = _CONTADOR.Kernel;
        TempData["Framework"] = _CONTADOR.Framework;
        TempData["MensagemFixa"] = "Teste";
        TempData["MensagemVariavel"] = configuration["MensagemVariavel"];
    }
}
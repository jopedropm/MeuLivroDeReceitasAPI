using System.Globalization;

namespace MyRecipeBook.API.Middleware;

public class CultureMiddleware
{
    private readonly RequestDelegate _next;

    //Parte essencial do middleware, é no RequestDelegate next que pedimos para o codigo seguir em frente
    public CultureMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        //Busca as linguagens suportadas pelo .net
        var supportedLanguages = CultureInfo.GetCultures(CultureTypes.AllCultures);

        //Busca qual o idioma que o http esta usando
        var acceptLanguageHeader = context.Request.Headers.AcceptLanguage.FirstOrDefault();
        var requestedCulture = !string.IsNullOrWhiteSpace(acceptLanguageHeader)
            ? acceptLanguageHeader.Split(',')[0]
            : null;


        //Passamos com idioma padrão o inglês
        var cultureInfo = new CultureInfo("en");

        //Se requestCulture nao estiver vazio e for uma linguagem suportada pelo .net
        if (string.IsNullOrWhiteSpace(requestedCulture) == false && supportedLanguages.Any(language => language.Name.Equals(requestedCulture)))
        {
            //Passamos o idioma encontrado pelo requestCulture, caso o if nao de certo continua o idioma padrão inglês
            cultureInfo = new CultureInfo(requestedCulture);
        }

        //Define o idioma principal da página
        CultureInfo.CurrentCulture = cultureInfo;
        CultureInfo.CurrentUICulture = cultureInfo;

        //Pode seguir o fluxo
        await _next(context);
    }
}

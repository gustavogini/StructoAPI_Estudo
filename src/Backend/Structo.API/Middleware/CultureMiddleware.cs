using System.Globalization;

namespace Structo.API.Middleware
{
    public class CultureMiddleware
    {
        private readonly RequestDelegate _next;
        public CultureMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var supportedLanguages = CultureInfo.GetCultures(CultureTypes.AllCultures);

            var requestedCulture = context.Request.Headers.AcceptLanguage.FirstOrDefault();//solicitar cultura do cabeçalho da requisição

            var cultureInfo = new CultureInfo("pt-BR");//definir cultura padrão

            if(string.IsNullOrWhiteSpace(requestedCulture) == false && supportedLanguages.Any(culture => culture.Name.Equals(requestedCulture)))
            {
                cultureInfo = new CultureInfo(requestedCulture);//definir cultura padrão como portugues Brasil se não for especificada
            }

            CultureInfo.CurrentCulture = cultureInfo;
            CultureInfo.CurrentUICulture = cultureInfo;

            await _next(context);
        }
    }
}

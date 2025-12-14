using Structo.Domain.Extensions;
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
            var supportedLanguages = CultureInfo.GetCultures(CultureTypes.AllCultures).ToList();

            var requestedCulture = context.Request.Headers.AcceptLanguage.FirstOrDefault();//solicitar cultura do cabeçalho da requisição

            var cultureInfo = new CultureInfo("en");//definir cultura padrão

            if (requestedCulture.NotEmpty() && supportedLanguages.Exists(culture => culture.Name.Equals(requestedCulture)))
            {
                cultureInfo = new CultureInfo(requestedCulture);//definir cultura padrão como portugues Brasil se não for especificada
            }

            CultureInfo.CurrentCulture = cultureInfo;
            CultureInfo.CurrentUICulture = cultureInfo;

            await _next(context);
        }
    }
}

using Structo.Domain.Security.Tokens;

namespace Structo.API.Token
{
    public class HttpContextTokenValue : ITokenProvider
    {
        private readonly IHttpContextAccessor _contextAccessor; //somente esse projeto de API tem acesso a essa variável


        public HttpContextTokenValue(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }


        public string Value()
        {
            var authentication = _contextAccessor.HttpContext!.Request.Headers.Authorization.ToString();

            return authentication["Bearer ".Length..].Trim();

        }



    }
}

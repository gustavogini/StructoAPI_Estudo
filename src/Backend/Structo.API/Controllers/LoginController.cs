using Microsoft.AspNetCore.Mvc;
using Structo.Application.UseCases.Login.DoLogin;
using Structo.Communication.Requests;
using Structo.Communication.Responses;

namespace Structo.API.Controllers
{

    public class LoginController : StructoBaseController
    {
        [HttpPost]
        [ProducesResponseType(typeof(ResponseRegisteredUserJson), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status401Unauthorized)]


        public async Task<IActionResult> Login([FromServices] IDoLoginUseCase useCase, [FromBody] RequestLoginJson request)
        {
            var response = await useCase.Execute(request);

            return Ok(response);
        }

    }
}

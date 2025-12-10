using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Structo.Application.UseCases.User.Register;
using Structo.Communication.Requests;
using Structo.Communication.Responses;

namespace Structo.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(typeof(ResponseRegisteredUserJson), StatusCodes.Status201Created)]
        public async Task<IActionResult> Register(
            [FromServices] IRegisterUserUseCase useCase, 
            [FromBody] RequestRegisterUserJson request) //Register a new user
        {
            var result = await useCase.Execute(request);

            return Created(string.Empty, result); // Returns a 201 Created response 
        }
    }
}

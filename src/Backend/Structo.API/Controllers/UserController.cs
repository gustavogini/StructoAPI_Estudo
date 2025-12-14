using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Structo.API.Attributes;
using Structo.Application.UseCases.User.ChangePassword;
using Structo.Application.UseCases.User.Profile;
using Structo.Application.UseCases.User.Register;
using Structo.Application.UseCases.User.Update;
using Structo.Communication.Requests;
using Structo.Communication.Responses;

namespace Structo.API.Controllers
{
    
    public class UserController : StructoBaseController
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

        [HttpGet]
        [ProducesResponseType(typeof(ResponseUserProfileJson), StatusCodes.Status200OK)]
        [AuthenticatedUser] // em toda controller que quiser que o usuario esteja autenticado devera ter esse atributo
        public async Task<IActionResult> GetUserProfile([FromServices] IGetUserProfileUseCase useCase) // Get the profile of the authenticated user
        {
            var result = await useCase.Execute();
            return Ok(result); // Returns a 200 OK response
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ResponseUserProfileJson), StatusCodes.Status400BadRequest)]
        [AuthenticatedUser] // em toda controller que quiser que o usuario esteja autenticado devera ter esse atributo

        public async Task<IActionResult> Update(
            [FromServices] IUpdateUserUseCase useCase,
            [FromBody] RequestUpdateUserJson request) // Update the profile of the authenticated user
        {
            await useCase.Execute(request);
            return NoContent(); // Returns a 204 No Content response
        }

        [HttpPut("change-password")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ResponseUserProfileJson), StatusCodes.Status400BadRequest)]
        [AuthenticatedUser] // em toda controller que quiser que o usuario esteja autenticado devera ter esse atributo

        public async Task<IActionResult> ChangePassword(
            [FromServices] IChangePasswordUseCase useCase,
            [FromBody] RequestChangePasswordJson request) // Change the password of the authenticated user
        {
            await useCase.Execute(request);

            return NoContent(); // Returns a 204 No Content response
        }
    }
}

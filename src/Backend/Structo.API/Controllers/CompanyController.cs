using Microsoft.AspNetCore.Mvc;
using Structo.API.Attributes;
using Structo.API.Binders;
using Structo.Application.UseCases.Company.Delete;
using Structo.Application.UseCases.Company.Filter;
using Structo.Application.UseCases.Company.GetById;
using Structo.Application.UseCases.Company.Register;
using Structo.Application.UseCases.Company.Update;
using Structo.Communication.Requests;
using Structo.Communication.Responses;

namespace Structo.API.Controllers
{
    [AuthenticatedUser]
    public class CompanyController : StructoBaseController
    {
        [HttpPost]
        [ProducesResponseType(typeof(ResponseRegisteredCompanyJson), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register(
            [FromServices] IRegisterCompanyUseCase useCase,
            [FromBody] RequestCompanyJson request)
        {
            var response = await useCase.Execute(request);

            return Created(string.Empty, response);
        }

        
        [HttpPost("filter")]
        [ProducesResponseType(typeof(ResponseCompaniesJson), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Filter(
        [FromServices] IFilterCompanyUseCase useCase,
        [FromBody] RequestFilterCompanyJson request)
        {
            var response = await useCase.Execute(request);

            if (response.Companies.Any())
                return Ok(response);

            return NoContent();
        }

        
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(ResponseCompanyJson), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(
        [FromServices] IGetCompanyByIdUseCase useCase,
        [FromRoute][ModelBinder(typeof(StructoIdBinder))] long id)
        {
            var response = await useCase.Execute(id);

            return Ok(response);
        }

        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType( StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(
        [FromServices] IDeleteCompanyUseCase useCase,
        [FromRoute][ModelBinder(typeof(StructoIdBinder))] long id)
        {
            await useCase.Execute(id);

            return NoContent();
        }
                
        [HttpPut]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(
        [FromServices] IUpdateCompanyUseCase useCase,
        [FromRoute][ModelBinder(typeof(StructoIdBinder))] long id,
        [FromBody] RequestCompanyJson request)
        {
            await useCase.Execute(id, request);

            return NoContent();
        }
    }
}

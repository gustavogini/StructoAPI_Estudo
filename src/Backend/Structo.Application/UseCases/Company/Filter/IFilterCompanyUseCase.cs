using Structo.Communication.Requests;
using Structo.Communication.Responses;

namespace Structo.Application.UseCases.Company.Filter
{
    public interface IFilterCompanyUseCase
    {
        Task<ResponseCompaniesJson> Execute(RequestFilterCompanyJson request);
    }
}

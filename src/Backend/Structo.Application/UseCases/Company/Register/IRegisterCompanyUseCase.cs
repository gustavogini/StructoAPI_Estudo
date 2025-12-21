using Structo.Communication.Requests;
using Structo.Communication.Responses;

namespace Structo.Application.UseCases.Company.Register
{
    public interface IRegisterCompanyUseCase
    {
        public Task<ResponseRegisteredCompanyJson> Execute(RequestCompanyJson request); //essa TAsk faz com que o metodo Execute seja assincrono e retorne uma ResponseRegisteredCompanyJson e receba um RequestCompanyJson como parametro
    }
}

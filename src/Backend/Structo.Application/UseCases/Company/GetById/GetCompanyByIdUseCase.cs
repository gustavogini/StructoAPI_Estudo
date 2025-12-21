using AutoMapper;
using Structo.Communication.Responses;
using Structo.Domain.Repositories.Company;
using Structo.Domain.Services.LoggedUser;
using Structo.Exceptions;
using Structo.Exceptions.ExceptionsBase;

namespace Structo.Application.UseCases.Company.GetById
{
    public class GetCompanyByIdUseCase : IGetCompanyByIdUseCase
    {
        private readonly IMapper _mapper;
        private readonly ILoggedUser _loggedUser;
        private readonly ICompanyReadOnlyRepository _repository;

        public GetCompanyByIdUseCase(
            IMapper mapper,
            ILoggedUser loggedUser,
            ICompanyReadOnlyRepository repository)
        {
            _mapper = mapper;
            _loggedUser = loggedUser;
            _repository = repository;
        }

        public async Task<ResponseCompanyJson> Execute(long companyId)
        {
            var loggedUser = await _loggedUser.User();

            var company = await _repository.GetById(companyId);

            if (company is null)
            {
                throw new NotFoundException(ResourceMessagesException.COMPANY_NOT_FOUND);
            }

            return _mapper.Map<ResponseCompanyJson>(company);
        }
    }
}

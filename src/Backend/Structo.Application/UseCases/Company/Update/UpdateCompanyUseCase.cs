using AutoMapper;
using Structo.Communication.Requests;
using Structo.Domain.Extensions;
using Structo.Domain.Repositories;
using Structo.Domain.Repositories.Company;
using Structo.Domain.Services.LoggedUser;
using Structo.Exceptions;
using Structo.Exceptions.ExceptionsBase;

namespace Structo.Application.UseCases.Company.Update
{
    public class UpdateCompanyUseCase : IUpdateCompanyUseCase
    {
        private readonly ICompanyUpdateOnlyRepository _repository;
        private readonly ILoggedUser _loggedUser;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateCompanyUseCase(ILoggedUser loggedUser,
            IUnitOfWork unitOfWork,
            ICompanyUpdateOnlyRepository repository,
            IMapper mapper
            )
        {
            _loggedUser = loggedUser;
            _unitOfWork = unitOfWork;
            _repository = repository;
            _mapper = mapper;
        }


        public async Task Execute(long companyId, RequestCompanyJson request)
        {
            Validate(request);

            var loggedUser = await _loggedUser.User();

            var company = await _repository.GetById(companyId);

            if (company is null)
            {
                throw new NotFoundException(ResourceMessagesException.COMPANY_NOT_FOUND);
            }

            _mapper.Map(request, company);

            _repository.Update(company);

            await _unitOfWork.Commit();
        }

        private static void Validate(RequestCompanyJson request)
        {
            var result = new CompanyValidator().Validate(request);

            if (result.IsValid.IsFalse())
            {
                throw new ErrorOnValidationException(result.Errors.Select(e => e.ErrorMessage).Distinct().ToList());
            }
        }

    }
}

using AutoMapper;
using Structo.Communication.Requests;
using Structo.Communication.Responses;
using Structo.Domain.Extensions;
using Structo.Domain.Repositories;
using Structo.Domain.Repositories.Company;
using Structo.Domain.Services.LoggedUser;
using Structo.Exceptions.ExceptionsBase;

namespace Structo.Application.UseCases.Company.Register
{
    public class RegisterCompanyUseCase : IRegisterCompanyUseCase
    {
        private readonly ICompanyWriteOnlyRepository _repository;
        private readonly ILoggedUser _loggedUser;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RegisterCompanyUseCase(
            ILoggedUser loggedUser,
            ICompanyWriteOnlyRepository repository,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _repository = repository;
            _loggedUser = loggedUser;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ResponseRegisteredCompanyJson> Execute(RequestCompanyJson request)
        {
            Validate(request);

            //var loggedUser = await _loggedUser.User();

            var company = _mapper.Map<Domain.Entities.Company>(request);
            //companie.CompanyId = loggedUser.Id; //preciso do ID do usuario logado que criou o cadastro da empresa? se sim, é necessario incluir isso no cadastro da empresa.

            /*var instructions = request.Employees
                .Where(e => e.IsLegalRepresentative)
                .Select(e => new Domain.Entities.EmployeeInstruction
                {
                    EmployeeName = e.EmployeeName,
                    Cpf = e.Cpf,
                    IsLegalRepresentative = e.IsLegalRepresentative,
                    Companie = companie
                }).ToList();*/

            //aqui ainda nao tem uma lista para realizar alguma validacao, pode ser que necessite e terá que atualizar aqui

            await _repository.Add(company);

            await _unitOfWork.Commit();

            return _mapper.Map<ResponseRegisteredCompanyJson>(company);

        }


        private void Validate(RequestCompanyJson request)
        {
            var result = new CompanyValidator().Validate(request);

            if (result.IsValid.IsFalse())
            {
                throw new ErrorOnValidationException(result.Errors.Select(e => e.ErrorMessage).Distinct().ToList());// aqui a exception tem que ser corrigida para Company
            }
        }
    }
}

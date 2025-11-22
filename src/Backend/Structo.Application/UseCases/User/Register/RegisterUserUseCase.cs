using AutoMapper;
using Structo.Application.Services.Cryptography;
using Structo.Communication.Requests;
using Structo.Communication.Responses;
using Structo.Domain.Repositories;
using Structo.Domain.Repositories.User;
using Structo.Exceptions;
using Structo.Exceptions.ExceptionsBase;

namespace Structo.Application.UseCases.User.Register
{
    public class RegisterUserUseCase : IRegisterUserUseCase
    {
        private readonly IUserWriteOnlyRepository _writeOnlyRepository;
        private readonly IUserReadOnlyRepository _readOnlyRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly PasswordEncripter _passwordEncripter;

        public RegisterUserUseCase(IUserWriteOnlyRepository writeOnlyRepository,
                                   IUserReadOnlyRepository readOnlyRepository,
                                   IUnitOfWork unitOfWork,
                                   PasswordEncripter passwordEncripter,
                                   IMapper mapper)
        {
            _writeOnlyRepository = writeOnlyRepository;
            _readOnlyRepository = readOnlyRepository;
            _unitOfWork = unitOfWork;
            _passwordEncripter = passwordEncripter;
            _mapper = mapper;
        }

        public async Task<ResponseRegisteredUserJson> Execute(RequestRegisterUserJson request)
        {
            await ValidateRequest(request);

            var user = _mapper.Map<Domain.Entities.User>(request);

            user.Password = _passwordEncripter.Encrypt(request.Password);

            await _writeOnlyRepository.Add(user);

            await _unitOfWork.Commit(); //aqui ela que realmente confirma a transação (salva no banco de dados)

            return new ResponseRegisteredUserJson 
            { 
                Name = user.Username //dado que retorna do banco de dados após salvar a entidade
            };
        }

        private async Task ValidateRequest(RequestRegisterUserJson request)
        {
            var validator = new RegisterUserValidator();

            var result = validator.Validate(request);

            var emailExist = await _readOnlyRepository.ExistActiveUserWtihEmail(request.Email);
            if (emailExist)
            {
                result.Errors.Add(new FluentValidation.Results.ValidationFailure(string.Empty, ResourceMessagesException.EMAIL_ALREADY_REGISTERED));
            }

            if (result.IsValid == false)
            {
                var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList();

                throw new ErrorOnValidationException(errorMessages);
            }
        }
    }
}

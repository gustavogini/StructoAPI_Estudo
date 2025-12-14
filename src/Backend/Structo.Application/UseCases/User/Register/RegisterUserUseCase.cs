using AutoMapper;
using Structo.Communication.Requests;
using Structo.Communication.Responses;
using Structo.Domain.Repositories;
using Structo.Domain.Repositories.User;
using Structo.Domain.Security.Cryptography;
using Structo.Domain.Security.Tokens;
using Structo.Exceptions;
using Structo.Exceptions.ExceptionsBase;

namespace Structo.Application.UseCases.User.Register
{
    public class RegisterUserUseCase : IRegisterUserUseCase
    {
        private readonly IUserWriteOnlyRepository _writeOnlyRepository; // interface de repositório para operações de escrita para usuários
        private readonly IUserReadOnlyRepository _readOnlyRepository; // interface de repositório para operações de leitura para usuários
        private readonly IUnitOfWork _unitOfWork; // interface para gerenciar transações de banco de dados
        private readonly IMapper _mapper; // interface do AutoMapper para mapear objetos de um tipo para outro param de DTOs e entidades
        private readonly IPasswordEncripter _passwordEncripter; // serviço para encriptar senhas de usuários no momento do registro
        private readonly IAccessTokenGenerator _accessTokenGenerator; // serviço para gerar tokens de acesso para autenticação

        public RegisterUserUseCase(IUserWriteOnlyRepository writeOnlyRepository,
                                   IUserReadOnlyRepository readOnlyRepository,
                                   IUnitOfWork unitOfWork,
                                   IPasswordEncripter passwordEncripter,
                                   IAccessTokenGenerator accessTokenGenerator,
                                   IMapper mapper) // injeção de dependências via construtor para os repositórios, unidade de trabalho, serviço de encriptação e mapper
        {
            _writeOnlyRepository = writeOnlyRepository;
            _readOnlyRepository = readOnlyRepository;
            _unitOfWork = unitOfWork;
            _passwordEncripter = passwordEncripter;
            _mapper = mapper;
            _accessTokenGenerator = accessTokenGenerator;
        }

        public async Task<ResponseRegisteredUserJson> Execute(RequestRegisterUserJson request)
        {
            await Validate(request);

            var user = _mapper.Map<Domain.Entities.User>(request);
            user.Password = _passwordEncripter.Encrypt(request.Password);
            user.UserIdentifier = Guid.NewGuid();

            await _writeOnlyRepository.Add(user);

            await _unitOfWork.Commit(); //aqui ela que realmente confirma a transação (salva no banco de dados)

            return new ResponseRegisteredUserJson 
            { 
                Name = user.Username, //dado que retorna do banco de dados após salvar a entidade
                Tokens = new ResponseTokensJson
                {
                    AccessToken = _accessTokenGenerator.Generate(user.UserIdentifier)
                }
            };
        }

        private async Task Validate(RequestRegisterUserJson request)
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

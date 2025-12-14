using Structo.Communication.Requests;
using Structo.Communication.Responses;
using Structo.Domain.Repositories.User;
using Structo.Domain.Security.Cryptography;
using Structo.Domain.Security.Tokens;
using Structo.Exceptions.ExceptionsBase;

namespace Structo.Application.UseCases.Login.DoLogin
{
    public class DoLoginUseCase : IDoLoginUseCase
    {
        private readonly IUserReadOnlyRepository _repository;
        private readonly IPasswordEncripter _passwordEncripter;
        private readonly IAccessTokenGenerator _accessTokenGenerator;
        public DoLoginUseCase(IUserReadOnlyRepository repository, IPasswordEncripter passwordEncripter, IAccessTokenGenerator accessTokenGenerator)
        {
            _repository = repository;
            _passwordEncripter = passwordEncripter;
            _accessTokenGenerator = accessTokenGenerator;
        }

        public async Task<ResponseRegisteredUserJson> Execute(RequestLoginJson request)
        {
            var encryptedPassword = _passwordEncripter.Encrypt(request.Password);

            var user = await _repository.GetByEmailAndPassword(request.Email, encryptedPassword) ?? throw new InvalidLoginException(); // o que esta a esqquerda da ?? so e avaliado se o que esta a direita nao for nulo

            return new ResponseRegisteredUserJson()
            {
                Name = user.Username,
                Tokens = new ResponseTokensJson
                {
                    AccessToken = _accessTokenGenerator.Generate(user.UserIdentifier)
                }
            };
        }
    }
}

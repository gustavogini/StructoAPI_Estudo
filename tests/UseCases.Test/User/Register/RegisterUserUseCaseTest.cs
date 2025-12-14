using CommonTestUtilities.Cryptography;
using CommonTestUtilities.Mapper;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Requests;
using CommonTestUtilities.Tokens;
using FluentAssertions;
using Structo.Application.UseCases.User.Register;
using Structo.Exceptions;
using Structo.Exceptions.ExceptionsBase;

namespace UseCases.Test.User.Register
{
    public class RegisterUserUseCaseTest
    {
        [Fact]
        public async Task Success()
        {
            var request = RequestRegisterUserJsonBuilder.Build();

            var useCase = CreateUseCase();

            var result = await useCase.Execute(request);

            result.Should().NotBeNull();
            result.Tokens.Should().NotBeNull();
            result.Name.Should().Be(request.Username);
            result.Tokens.AccessToken.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public async Task Error_Email_Already_Registered()
        {
            var request = RequestRegisterUserJsonBuilder.Build();
            var useCase = CreateUseCase();

            Func<Task> act = async () =>  await useCase.Execute(request); // essa é uma variavel Func com nome act. ela chama a funcao usecase e armazena a funcao na variavel act

            (await act.Should().ThrowAsync<ErrorOnValidationException>()) // aqui o act deve lançar a exceção. neste momento que ele executa a funcao armazenada na variavel act
                .Where(e => e.ErrorMessages.Count == 1 && e.ErrorMessages.Contains(ResourceMessagesException.EMAIL_ALREADY_REGISTERED));
        }

        [Fact]
        public async Task Error_Name_Empty()
        {
            var request = RequestRegisterUserJsonBuilder.Build();
            request.Username = string.Empty;

            var useCase = CreateUseCase();

            Func<Task> act = async () =>  await useCase.Execute(request); // essa é uma variavel Func com nome act. ela chama a funcao usecase e armazena a funcao na variavel act

            (await act.Should().ThrowAsync<ErrorOnValidationException>()) // aqui o act deve lançar a exceção. neste momento que ele executa a funcao armazenada na variavel act
                .Where(e => e.ErrorMessages.Count == 1 && e.ErrorMessages.Contains(ResourceMessagesException.NAME_EMPTY));
        }

        private static RegisterUserUseCase CreateUseCase(string? email = null)//dessa forma o email pode ser opcional e nao afeta o useCase que nao passa email
        {
            var mapper = MapperBuilder.Build();
            var passwordEncripter = PassordEncripterBuilder.Build();
            var writerRepository = UserWriteOnlyRepositoryBuilder.Build();
            var unitOfWork = UnitOfWorkBuilder.Build();
            var readRepositoryBuilder = new UserReadOnlyRepositoryBuilder(); //nesse caso precisa ser instanciado(new) pq não é estático
            var accessTokenGenerator = JwtTokenGeneratorBuilder.Build();

            if (string.IsNullOrEmpty(email) == false)
            {
                readRepositoryBuilder.ExistActiveUserWtihEmail(email);
            }

            return new RegisterUserUseCase(writerRepository, readRepositoryBuilder.Build(), unitOfWork, passwordEncripter, accessTokenGenerator, mapper);
        }
    }
}

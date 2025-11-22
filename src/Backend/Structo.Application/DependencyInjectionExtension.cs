using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Structo.Application.Services.Automapper;
using Structo.Application.Services.Cryptography;
using Structo.Application.UseCases.User.Register;


namespace Structo.Application
{
    public static class DependencyInjectionExtension
    {
        public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            // Adicione aqui a configuração de injeção de dependências para a camada de aplicação
            AddPasswordEncrypter(services, configuration);
            AddAutoMapper(services);
            AddUseCases(services);
        }

        private static void AddAutoMapper(IServiceCollection services)
        {
            var autoMapper = new AutoMapper.MapperConfiguration(options =>
            {
                options.AddProfile(new AutoMapping());
            }).CreateMapper();

            services.AddScoped(option => autoMapper);
        }

        private static void AddUseCases(IServiceCollection services)
        {
            // Configuração dos casos de uso
            services.AddScoped<IRegisterUserUseCase, RegisterUserUseCase>();


        }


        private static void AddPasswordEncrypter(IServiceCollection services, IConfiguration configuration)
        {
            // Configuração dos casos de uso
            var additionalKey = configuration.GetValue<string>("Settings:Password:AdditionalKey");

            services.AddScoped(options => new PasswordEncripter(additionalKey!));


        }
    }
}

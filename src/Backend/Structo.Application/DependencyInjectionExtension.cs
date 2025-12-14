using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Structo.Application.Services.Automapper;
using Structo.Application.UseCases.Login.DoLogin;
using Structo.Application.UseCases.User.Profile;
using Structo.Application.UseCases.User.Register;
using Structo.Application.UseCases.User.Update;


namespace Structo.Application
{
    public static class DependencyInjectionExtension
    {
        public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            // Adicione aqui a configuração de injeção de dependências para a camada de aplicação
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
            services.AddScoped<IDoLoginUseCase, DoLoginUseCase>();
            services.AddScoped<IGetUserProfileUseCase, GetUserProfileUseCase>();
            services.AddScoped<IUpdateUserUseCase, UpdateUserUseCase>();


        }


        
    }
}

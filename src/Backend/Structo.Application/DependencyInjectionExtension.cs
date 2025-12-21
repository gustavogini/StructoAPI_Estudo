using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sqids;
using Structo.Application.Services.Automapper;
using Structo.Application.UseCases.Company.Delete;
using Structo.Application.UseCases.Company.Filter;
using Structo.Application.UseCases.Company.GetById;
using Structo.Application.UseCases.Company.Register;
using Structo.Application.UseCases.Company.Update;
using Structo.Application.UseCases.Login.DoLogin;
using Structo.Application.UseCases.User.ChangePassword;
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
            AddIdEncoder(services, configuration);
            AddUseCases(services);
        }

        private static void AddAutoMapper(IServiceCollection services)
        {
            services.AddScoped(option => new AutoMapper.MapperConfiguration(autoMapperOptions =>
            {
                var sqids = option.GetService<SqidsEncoder<long>>()!;

                autoMapperOptions.AddProfile(new AutoMapping(sqids));
            }).CreateMapper());
        }

        private static void AddIdEncoder(IServiceCollection services, IConfiguration configuration)
        {
            var sqids = new SqidsEncoder<long>(new()
            {
                MinLength = 3,
                Alphabet = configuration.GetValue<string>("Settings:IdCryptographyAlphabet")!
            });

            services.AddSingleton(sqids);
        }

        private static void AddUseCases(IServiceCollection services)
        {
            // Configuração dos casos de uso
            services.AddScoped<IRegisterUserUseCase, RegisterUserUseCase>();
            services.AddScoped<IDoLoginUseCase, DoLoginUseCase>();
            services.AddScoped<IGetUserProfileUseCase, GetUserProfileUseCase>();
            services.AddScoped<IUpdateUserUseCase, UpdateUserUseCase>();
            services.AddScoped<IChangePasswordUseCase, ChangePasswordUseCase>();
            services.AddScoped<IRegisterCompanyUseCase, RegisterCompanyUseCase>();
            services.AddScoped<IFilterCompanyUseCase, FilterCompanyUseCase>();
            services.AddScoped<IGetCompanyByIdUseCase, GetCompanyByIdUseCase>();
            services.AddScoped<IDeleteCompanyUseCase, DeleteCompanyUseCase>();
            services.AddScoped<IUpdateCompanyUseCase, UpdateCompanyUseCase>();

            
        }


        
    }
}

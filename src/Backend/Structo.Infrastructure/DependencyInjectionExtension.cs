using FluentMigrator.Runner;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Structo.Domain.Repositories;
using Structo.Domain.Repositories.Company;
using Structo.Domain.Repositories.User;
using Structo.Domain.Security.Cryptography;
using Structo.Domain.Security.Tokens;
using Structo.Domain.Services.LoggedUser;
using Structo.Infrastructure.DataAccess;
using Structo.Infrastructure.DataAccess.Repositories;
using Structo.Infrastructure.Extensions;
using Structo.Infrastructure.Security.Cryptography;
using Structo.Infrastructure.Security.Tokens.Access.Generator;
using Structo.Infrastructure.Security.Tokens.Access.Validator;
using Structo.Infrastructure.Services.LoggedUser;
using System.Reflection;

namespace Structo.Infrastructure
{
    public static class DependencyInjectionExtension
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            // Adicione aqui a configuração de injeção de dependências para a camada de infraestrutura
            AddPasswordEncrypter(services, configuration);
            AddRepositories(services);
            AddLoggerUser(services);
            AddTokens(services, configuration);

            if (configuration.IsUnitTestEnviroment())
            {
                return; // Se estiver em ambiente de teste unitário, não configure o DbContext e FluentMigrator
            }
            

            AddDbContext_Postgres(services, configuration);
            AddFluentMigrator_Postgres(services, configuration);
        }

        private static void AddDbContext_Postgres(IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.ConnectionString();
            // Configuração do DbContext
            services.AddDbContextPool<StructoDbContext>(dbContextOptions =>
            {
                dbContextOptions.UseNpgsql(connectionString); //aqui ja esta para postgresql
            });
        }

        private static void AddRepositories(IServiceCollection services)
        {
            // Configuração dos repositórios
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUserWriteOnlyRepository, UserRepository>();
            services.AddScoped<IUserReadOnlyRepository, UserRepository>();
            services.AddScoped<IUserUpdateOnlyRepository, UserRepository>();
            services.AddScoped<ICompanyWriteOnlyRepository, CompanyRepository>();
            services.AddScoped<ICompanyReadOnlyRepository, CompanyRepository>();
            services.AddScoped<ICompanyUpdateOnlyRepository, CompanyRepository>();
        }

        private static void AddFluentMigrator_Postgres(IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.ConnectionString();
            // Configuração do FluentMigrator para PostgreSQL
            services.AddFluentMigratorCore()
                .ConfigureRunner(options =>
                {
                    options
                    .AddPostgres()
                    .WithGlobalConnectionString(connectionString)
                    .ScanIn(Assembly.Load("Structo.Infrastructure")).For.All();
                });
        }

        private static void AddTokens(IServiceCollection services, IConfiguration configuration)
        {
            var expirationTimeMinutes = configuration.GetValue<uint>("Settings:Jwt:ExpirationTimeMinutes");
            var signingKey = configuration.GetValue<string>("Settings:Jwt:SigningKey");

            services.AddScoped<IAccessTokenGenerator>(option => new JwtTokenGenerator(expirationTimeMinutes, signingKey!));
            services.AddScoped<IAccessTokenValidator>(option => new JwtTokenValidator(signingKey!));

        }

        private static void AddLoggerUser(IServiceCollection services)
        {
            services.AddScoped<ILoggedUser, LoggedUser>();
        }

        private static void AddPasswordEncrypter(IServiceCollection services, IConfiguration configuration)
        {
            // Configuração dos casos de uso
            var additionalKey = configuration.GetValue<string>("Settings:Password:AdditionalKey");

            services.AddScoped<IPasswordEncripter>(options => new Sha512Encripter(additionalKey!));


        }

    }
}

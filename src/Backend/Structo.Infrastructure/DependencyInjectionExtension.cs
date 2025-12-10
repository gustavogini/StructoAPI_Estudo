using FluentMigrator.Runner;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Structo.Domain.Repositories;
using Structo.Domain.Repositories.User;
using Structo.Infrastructure.DataAccess;
using Structo.Infrastructure.DataAccess.Repositories;
using Structo.Infrastructure.Extensions;
using System.Reflection;

namespace Structo.Infrastructure
{
    public static class DependencyInjectionExtension
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            // Adicione aqui a configuração de injeção de dependências para a camada de infraestrutura
            AddRepositories(services);
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

    }
}

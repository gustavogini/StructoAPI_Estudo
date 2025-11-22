using Dapper;
using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;

namespace Structo.Infrastructure.Migrations
{
    public class DatabaseMigration
    {
        public static void Migrate(string connectionString, IServiceProvider serviceProvider)
        {
            EnsureDatabaseCreated(connectionString);
            MigrationDatabase(serviceProvider);
        }   

        private static void EnsureDatabaseCreated(string connectionString)
        {
            var connectionStringBuilder = new NpgsqlConnectionStringBuilder(connectionString);

            var databaseName = connectionStringBuilder.Database;

            connectionStringBuilder.Remove("Database");

            using var dbConnection = new NpgsqlConnection(connectionStringBuilder.ConnectionString);
            

            var parameters = new DynamicParameters();
            parameters.Add("name", databaseName);

            var records  = dbConnection.Query("SELECT * FROM information_schema.schemata WHERE schema_name = @name", parameters); 

            if (records.Any() == false)
            {
                dbConnection.Execute($"CREATE DATABASE {databaseName}"); //converter para postgresql
            }
        }

        private static void MigrationDatabase(IServiceProvider serviceProvider)
        {
            var runner = serviceProvider.GetRequiredService<IMigrationRunner>();
            runner.ListMigrations();
            runner.MigrateUp();
        }

    }
}

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
            
            EnsureDatabaseCreated(connectionString); // garantee database exists
            MigrationDatabase(serviceProvider); // execute migrations

        }   

        private static void EnsureDatabaseCreated(string connectionString)
        {
            var connectionStringBuilder = new NpgsqlConnectionStringBuilder(connectionString);

            var databaseName = connectionStringBuilder.Database;

            connectionStringBuilder.Remove("Database");

            using var dbConnection = new NpgsqlConnection(connectionStringBuilder.ConnectionString);
            dbConnection.Open();

            var parameters = new DynamicParameters();
            parameters.Add("name", databaseName);


            //var records = dbConnection.Query("SELECT * FROM information_schema.schemata WHERE schema_name = @name", parameters);
            var records  = dbConnection.ExecuteScalar<int>("SELECT COUNT(*) FROM pg_database WHERE datname = @name", parameters);

            if (records == 0 ) //(records.Any() == false)
            {
                dbConnection.Execute($@"CREATE DATABASE ""{databaseName}""");  //\"{databaseName}\"
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

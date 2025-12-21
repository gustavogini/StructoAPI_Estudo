using CommonTestUtilities.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Structo.Infrastructure.DataAccess;

namespace WebApi.Test
{
    public class CustomWebApplicationFactory : WebApplicationFactory<Program>
    {
        private Structo.Domain.Entities.User _user = default!;
        private string _password = string.Empty;

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseEnvironment("Test")
                .ConfigureServices(services =>
                {
                    var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<StructoDbContext>));
                    if (descriptor is not null)
                    {
                        services.Remove(descriptor); // Remove existing DbContext registration for StructoDbContext in test environment in memory database
                    }

                    var provider = services.AddEntityFrameworkInMemoryDatabase().BuildServiceProvider();

                    services.AddDbContext<StructoDbContext>(options =>
                    {
                        options.UseInMemoryDatabase("InMemoryDbForTesting");
                        options.UseInternalServiceProvider(provider);
                    });

                    using var scope = services.BuildServiceProvider().CreateScope();

                    var dbContext = scope.ServiceProvider.GetRequiredService<StructoDbContext>();

                    dbContext.Database.EnsureDeleted();

                    StartDatabase(dbContext);
                });
        }

        public string GetEmail()
        {
            return _user.Email;
        }
        
        public string GetPassword()
        {
            return _password;
        }
        
        public string GetName()
        {
            return _user.Username;
        }

        public Guid GetUserIdentifier()
        {
            return _user.UserIdentifier; 
        }


        private void StartDatabase(StructoDbContext dbContext) // This method can be used to initialize or seed the database if needed
        {
            (_user, _password) = UserBuilder.Build();
            
            dbContext.Users.Add(_user);// Add test data as needed

            dbContext.SaveChanges();//Save test data to the in-memory database
        }



    }
}

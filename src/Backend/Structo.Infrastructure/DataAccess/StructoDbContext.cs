using Microsoft.EntityFrameworkCore;
using Structo.Domain.Entities;

namespace Structo.Infrastructure.DataAccess
{
    public class StructoDbContext : DbContext
    {
        public StructoDbContext(DbContextOptions<StructoDbContext> options) : base(options)
        {
            //aqui passamos a informação de conexão com o banco de dados para a base DbContext
        }

        public DbSet<User> Users { get; set; } //aqui estamos dizendo ao Entity Framework que temos uma tabela de usuários no banco de dados
        // aqui vamos adicionar os DbSets das outras entidades quando criarmos elas
        public DbSet<Company> Companies { get; set; } //aqui estamos dizendo ao Entity Framework que temos uma tabela de companhias no banco de dados

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           modelBuilder.ApplyConfigurationsFromAssembly(typeof(StructoDbContext).Assembly); //esta ensinando para o Entity Framework aplicar todas as configurações de mapeamento que estão na pasta
        }
    }
}

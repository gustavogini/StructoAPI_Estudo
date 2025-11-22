using Structo.Domain.Repositories;

namespace Structo.Infrastructure.DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StructoDbContext _dbContext;

        public UnitOfWork(StructoDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Commit()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}

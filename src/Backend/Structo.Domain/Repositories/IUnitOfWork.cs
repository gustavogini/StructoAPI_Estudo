namespace Structo.Domain.Repositories
{
    public interface IUnitOfWork
    {
        public Task Commit();
    }
}

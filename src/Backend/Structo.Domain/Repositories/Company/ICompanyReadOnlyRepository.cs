using Structo.Domain.Dtos;

namespace Structo.Domain.Repositories.Company
{
    public interface ICompanyReadOnlyRepository
    {
        Task<IList<Entities.Company>> Filter(FilterCompanyDto filters);
        Task<Entities.Company?> GetById(long companyId);
    }
}

using Structo.Domain.Dtos;

namespace Structo.Domain.Repositories.Company
{
    public interface ICompanyWriteOnlyRepository
    {
        Task Add(Entities.Company company);
        Task<IList<Entities.Company>> Filter(FilterCompanyDto filter);

        Task Delete(long companyId);
    }
}

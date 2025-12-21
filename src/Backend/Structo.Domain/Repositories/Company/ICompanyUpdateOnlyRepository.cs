using System;
using System.Collections.Generic;
using System.Text;

namespace Structo.Domain.Repositories.Company
{
    public interface ICompanyUpdateOnlyRepository
    {
        Task<Entities.Company?> GetById(long companyId);
        void Update(Entities.Company company);
    }
}

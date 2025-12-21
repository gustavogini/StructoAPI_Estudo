using Structo.Communication.Responses;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Structo.Application.UseCases.Company.Delete
{
    public interface IDeleteCompanyUseCase
    {
        Task Execute(long companyId);
    }
}

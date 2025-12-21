using Structo.Communication.Requests;
using System;
using System.Collections.Generic;
using System.Text;

namespace Structo.Application.UseCases.Company.Update
{
    public interface IUpdateCompanyUseCase
    {
        Task Execute(long companyId, RequestCompanyJson request);
    }
}

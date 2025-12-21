using Structo.Communication.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace Structo.Application.UseCases.Company.GetById
{
    public interface IGetCompanyByIdUseCase
    {
        Task<ResponseCompanyJson> Execute(long companyId);
    }
}

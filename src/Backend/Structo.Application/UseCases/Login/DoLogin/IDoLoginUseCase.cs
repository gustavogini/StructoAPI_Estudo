using Structo.Communication.Requests;
using Structo.Communication.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace Structo.Application.UseCases.Login.DoLogin
{
    public interface IDoLoginUseCase
    {
        public Task<ResponseRegisteredUserJson> Execute(RequestLoginJson request);
    }
}

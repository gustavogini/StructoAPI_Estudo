using Structo.Communication.Requests;
using Structo.Communication.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace Structo.Application.UseCases.User.Register
{
    public interface IRegisterUserUseCase
    {
        public Task<ResponseRegisteredUserJson> Execute(RequestRegisterUserJson request);
    }
}

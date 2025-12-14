using Structo.Communication.Requests;
using System;
using System.Collections.Generic;
using System.Text;

namespace Structo.Application.UseCases.User.ChangePassword
{
    public interface IChangePasswordUseCase
    {
        public Task Execute(RequestChangePasswordJson request);
    }
}

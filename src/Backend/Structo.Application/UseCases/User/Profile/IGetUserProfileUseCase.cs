using Structo.Communication.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace Structo.Application.UseCases.User.Profile
{
    public interface IGetUserProfileUseCase
    {
        public Task<ResponseUserProfileJson> Execute();
    }
}

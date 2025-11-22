using AutoMapper;
using Structo.Communication.Requests;

namespace Structo.Application.Services.Automapper
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            RequestToDomain();
        }

        private void RequestToDomain()
        {
            CreateMap<RequestRegisterUserJson, Domain.Entities.User>()
                .ForMember(destino => destino.Password, option => option.Ignore());
        }
    }
}

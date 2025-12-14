using AutoMapper;
using Structo.Communication.Requests;
using Structo.Communication.Responses;

namespace Structo.Application.Services.Automapper
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            RequestToDomain();
            DomainToResponse();
        }

        private void RequestToDomain()
        {
            CreateMap<RequestRegisterUserJson, Domain.Entities.User>()
                .ForMember(destino => destino.Password, option => option.Ignore());
        }
        
        
        private void DomainToResponse()
        {
            CreateMap<Domain.Entities.User, ResponseUserProfileJson>();
        }



    }
}

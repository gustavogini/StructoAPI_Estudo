using AutoMapper;
using Sqids;
using Structo.Communication.Requests;
using Structo.Communication.Responses;

namespace Structo.Application.Services.Automapper
{
    public class AutoMapping : Profile
    {
        private readonly SqidsEncoder<long> _idEncoder;
        public AutoMapping(SqidsEncoder<long> idEncoder)
        {
            _idEncoder = idEncoder;

            RequestToDomain();
            DomainToResponse();
        }

        private void RequestToDomain()
        {
            CreateMap<RequestRegisterUserJson, Domain.Entities.User>()
                .ForMember(destino => destino.Password, option => option.Ignore());

            //CreateMap<RequestCompanyJson, Domain.Entities.Company>(); //precisa seguir a aula 121(final da aula) para arrumar aqui.
                
        }
        
        
        private void DomainToResponse()
        {
            CreateMap<Domain.Entities.User, ResponseUserProfileJson>()
                .ForMember(destino => destino.UserName, config => config.MapFrom(source => source.Username));

            CreateMap<Domain.Entities.Company, ResponseRegisteredCompanyJson>()
                .ForMember(dest => dest.Id, config => config.MapFrom(source => _idEncoder.Encode(source.Id)));

            /*CreateMap<Domain.Entities.Company, ResponseShortCompanyJson>()
                .ForMember(dest => dest.Id, config => config.MapFrom(source => _idEncoder.Encode(source.Id)));

            CreateMap<Domain.Entities.Company, ResponseCompanyJson>()
                .ForMember(dest => dest.Id, config => config.MapFrom(source => _idEncoder.Encode(source.Id)));

            CreateMap<Domain.Entities.Employee, ResponseEmployeeJson>()
                .ForMember(dest => dest.Id, config => config.MapFrom(source => _idEncoder.Encode(source.Id)));*/

        }



    }
}

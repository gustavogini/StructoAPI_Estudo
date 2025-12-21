using AutoMapper;
using Structo.Communication.Responses;
using Structo.Domain.Services.LoggedUser;

namespace Structo.Application.UseCases.User.Profile
{
    public class GetUserProfileUseCase : IGetUserProfileUseCase
    {
        private readonly ILoggedUser _loogedUser;
        private readonly IMapper _mapper;

        public GetUserProfileUseCase(ILoggedUser loogedUser, IMapper mapper)
        {
            _loogedUser = loogedUser;
            _mapper = mapper;
        }
        public async Task<ResponseUserProfileJson> Execute()
        {
            var user = await _loogedUser.User();

            return _mapper.Map<ResponseUserProfileJson>(user);
        }
    }
}

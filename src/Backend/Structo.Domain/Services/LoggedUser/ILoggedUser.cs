using Structo.Domain.Entities;

namespace Structo.Domain.Services.LoggedUser
{
    public interface ILoggedUser
    {
        public Task<User> User();
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Structo.Domain.Repositories.User
{
    public interface IUserUpdateOnlyRepository
    {
        public Task<Entities.User> GetById(long id);

        public void Update(Entities.User user);
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Structo.Domain.Repositories.User
{
    public interface IUserWriteOnlyRepository
    {
        public Task Add(Entities.User user);
    }
}

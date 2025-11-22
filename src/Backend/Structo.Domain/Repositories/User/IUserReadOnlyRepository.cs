using System;
using System.Collections.Generic;
using System.Text;

namespace Structo.Domain.Repositories.User
{
    public interface IUserReadOnlyRepository
    {
        public Task<bool> ExistActiveUserWtihEmail(string email);
    }
}

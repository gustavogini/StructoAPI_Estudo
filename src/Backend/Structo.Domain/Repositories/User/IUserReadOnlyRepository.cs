using System;
using System.Collections.Generic;
using System.Text;

namespace Structo.Domain.Repositories.User
{
    public interface IUserReadOnlyRepository
    {
        public Task<bool> ExistActiveUserWtihEmail(string email);   
        public Task<bool> ExistActiveUserWithIdentifier(Guid userIdentifier);   //preciso conferir se isso esta correto no codigo fonte do professor.

        public Task<Entities.User?> GetByEmailAndPassword(string email, string password);
    }
}

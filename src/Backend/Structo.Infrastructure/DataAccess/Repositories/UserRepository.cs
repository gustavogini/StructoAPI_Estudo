using Microsoft.EntityFrameworkCore;
using Structo.Domain.Entities;
using Structo.Domain.Repositories.User;

namespace Structo.Infrastructure.DataAccess.Repositories
{
    public class UserRepository : IUserReadOnlyRepository, IUserWriteOnlyRepository
    {
        private readonly StructoDbContext _dbContext;

        public UserRepository(StructoDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Add(User user)
        {
            await _dbContext.Users.AddAsync(user); //adiciona o usuário à tabela de usuários no contexto do banco de dados
            //await _dbContext.SaveChangesAsync(); //salva as alterações no banco de dados

        }

        public async Task<bool> ExistActiveUserWtihEmail(string email) //coloca bool na task pq o retorno é verdadeiro ou falso
        {
            return await _dbContext.Users.AnyAsync(user => user.Email.Equals(email) && user.Active);
        }


    }
}

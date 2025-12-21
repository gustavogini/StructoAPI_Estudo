using Microsoft.EntityFrameworkCore;
using Structo.Domain.Entities;
using Structo.Domain.Repositories.User;

namespace Structo.Infrastructure.DataAccess.Repositories
{
    public class UserRepository : IUserReadOnlyRepository, IUserWriteOnlyRepository, IUserUpdateOnlyRepository
    {
        private readonly StructoDbContext _dbContext; // somente o construtor tem acesso a essa variável

        public UserRepository(StructoDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public async Task Add(User user)
        {
            await _dbContext.Users.AddAsync(user); //adiciona o usuário à tabela de usuários no contexto do banco de dados
        }

        public async Task<bool> ExistActiveUserWtihEmail(string email) //coloca bool na task pq o retorno é verdadeiro ou falso
        {
            return await _dbContext.Users.AnyAsync(user => user.Email.Equals(email) && user.Active);// verifica se existe algum usuário na tabela de usuários com o email fornecido e que esteja ativo
        }


        public async Task<bool> ExistActiveUserWithIdentifier(Guid userIdentifier)
        {
            return await _dbContext.Users.AnyAsync(user => user.UserIdentifier.Equals(userIdentifier) && user.Active);
        }


        public async Task<User?> GetByEmailAndPassword(string email, string password)
        {
            return await _dbContext
                .Users
                .AsNoTracking()//indica que a consulta não irá rastrear as alterações feitas nos objetos retornados, o que pode melhorar o desempenho em consultas somente leitura
                .FirstOrDefaultAsync(user => user.Email.Equals(email) && user.Password.Equals(password) && user.Active); //verifica se existe um usuário com o email e senha fornecidos e que esteja ativo, retornando o primeiro usuário encontrado ou nulo se nenhum for encontrado
        }

        public async Task<User> GetById(long id)
        {
            return await _dbContext
                .Users.
                FirstAsync(user => user.Id == id); //verifica se existe um usuário com o id fornecido, retornando o primeiro usuário encontrado ou nulo se nenhum for encontrado
        }

        public void Update(User user)
        {
            _dbContext.Users.Update(user); //atualiza o usuário na tabela de usuários no contexto do banco de dados
        }
    }
}

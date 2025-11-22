using Moq;
using Structo.Domain.Repositories.User;

namespace CommonTestUtilities.Repositories
{
    public class UserReadOnlyRepositoryBuilder
    {
        private readonly Mock<IUserReadOnlyRepository> _repository;

        public UserReadOnlyRepositoryBuilder()
        {
            _repository = new Mock<IUserReadOnlyRepository>();
        }

        public void ExistActiveUserWtihEmail(string email)
        {
            _repository.Setup(repository => repository.ExistActiveUserWtihEmail(email)).ReturnsAsync(true);
        }


        public IUserReadOnlyRepository Build()
        {
            return _repository.Object;
        }
    }
}

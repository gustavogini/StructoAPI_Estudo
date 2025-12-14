using Bogus;
using CommonTestUtilities.Cryptography;
using Structo.Domain.Entities;

namespace CommonTestUtilities.Entities
{
    public class UserBuilder
    {
        public static (User user, string password) Build()
        {
            var passwordEncripter = PassordEncripterBuilder.Build();

            var password = new Faker().Internet.Password();

            var user = new Faker<User>()
                .RuleFor(user => user.Id, () => 1)
                .RuleFor(user => user.Username, f => f.Person.FirstName)
                .RuleFor(user => user.Email, (f, User) => f.Internet.Email(User.Username))
                .RuleFor(user => user.Password, f => passwordEncripter.Encrypt(password));

            return (user, password);
        }



    }
}

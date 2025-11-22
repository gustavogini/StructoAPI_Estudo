using Bogus;
using Structo.Communication.Requests;

namespace CommonTestUtilities.Requests
{
    public class RequestRegisterUserJsonBuilder
    {
        public static RequestRegisterUserJson Build(int passwordLength = 10)
        {
            return new Faker<RequestRegisterUserJson>()
                .RuleFor(user => user.Username, (f) => f.Person.FirstName)
                .RuleFor(user => user.Password, (f) => f.Internet.Password(passwordLength))
                .RuleFor(user => user.Email, (f, user) => f.Internet.Email(user.Username));
        }
    }
}

using Bogus;
using Structo.Communication.Requests;

namespace CommonTestUtilities.Requests
{
    public class RequestLoginJsonBuilder
    {
        public static Faker<RequestLoginJson> Build()
        {
            return new Faker<RequestLoginJson>()
                .RuleFor(user => user.Email, f => f.Internet.Email())
                .RuleFor(user => user.Password, f => f.Internet.Password());
            
        }






    }
}

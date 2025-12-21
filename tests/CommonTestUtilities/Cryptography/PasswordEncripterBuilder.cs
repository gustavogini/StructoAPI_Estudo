using Structo.Domain.Security.Cryptography;
using Structo.Infrastructure.Security.Cryptography;

namespace CommonTestUtilities.Cryptography
{
    public class PasswordEncripterBuilder
    {
        public static IPasswordEncripter Build()
        {
            return new Sha512Encripter("abc1234");
        }
    }
}

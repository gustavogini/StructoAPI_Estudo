using Structo.Application.Services.Cryptography;

namespace CommonTestUtilities.Cryptography
{
    public class PassordEncripterBuilder
    {
        public static PasswordEncripter Build()
        {
            return new PasswordEncripter("abc1234");
        }
    }
}

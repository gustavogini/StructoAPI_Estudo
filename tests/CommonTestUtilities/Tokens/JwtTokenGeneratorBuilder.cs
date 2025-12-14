using Structo.Domain.Security.Tokens;
using Structo.Infrastructure.Security.Tokens.Access.Generator;
using System;
using System.Collections.Generic;
using System.Text;

namespace CommonTestUtilities.Tokens
{
    public class JwtTokenGeneratorBuilder
    {
        public static IAccessTokenGenerator Build() => new JwtTokenGenerator(expirationTimeMinutes: 5, signingKey:"tttttttttttttttttttttttttttttttt");









    }
}

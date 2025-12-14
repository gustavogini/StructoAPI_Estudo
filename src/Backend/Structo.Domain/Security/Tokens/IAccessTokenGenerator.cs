using System;
using System.Collections.Generic;
using System.Text;

namespace Structo.Domain.Security.Tokens
{
    public interface IAccessTokenGenerator
    {
        public string Generate(Guid userIdentifier);





    }
}

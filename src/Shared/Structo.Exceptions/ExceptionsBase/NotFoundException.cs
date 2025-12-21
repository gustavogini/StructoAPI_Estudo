using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Structo.Exceptions.ExceptionsBase
{
    public class NotFoundException : StructoException
    {
        public NotFoundException(string message) : base(message)
        {




        }

        public override IList<string> GetErrorMessages() => [Message];

        public override HttpStatusCode GetStatusCode() => HttpStatusCode.NotFound;
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Structo.Exceptions.ExceptionsBase
{
    public class InvalidLoginException : StructoException
    {
        public InvalidLoginException() : base(ResourceMessagesException.EMAIL_OR_PASSWORD_INVALID)
        {

        }
    }
}

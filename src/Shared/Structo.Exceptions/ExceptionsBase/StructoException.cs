using System;
using System.Collections.Generic;
using System.Text;

namespace Structo.Exceptions.ExceptionsBase
{
    public class StructoException : SystemException
    {
        public StructoException(string? message) : base(message)
        {
        }
    }
}

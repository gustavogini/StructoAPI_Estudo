using System.Net;

namespace Structo.Exceptions.ExceptionsBase
{
    public abstract class StructoException : SystemException
    {
        public StructoException(string? message) : base(message)
        {
        }

        public abstract IList<string> GetErrorMessages();
        public abstract HttpStatusCode GetStatusCode();
    }
}

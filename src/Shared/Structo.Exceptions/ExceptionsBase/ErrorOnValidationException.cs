namespace Structo.Exceptions.ExceptionsBase
{
    public class ErrorOnValidationException : StructoException
    {
        public IList<string> ErrorMessages { get; set; }

        public ErrorOnValidationException(IList<string> errorMessages)
        {
            ErrorMessages = errorMessages;
        }
    }
}

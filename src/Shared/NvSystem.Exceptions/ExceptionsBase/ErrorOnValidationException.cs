namespace NvSystem.Exceptions.ExceptionsBase;

public class ErrorOnValidationException : NvSystemException
{
    public IList<string> ErrorMessages { get; set; }

    public ErrorOnValidationException(IList<string> errorMessages)
    {
        ErrorMessages = errorMessages;
    }
}
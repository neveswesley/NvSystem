namespace NvSystem.Exceptions.ExceptionsBase;

public class EmailAlreadyExistsException : NvSystemException
{
    public string Message { get; set; }

    public EmailAlreadyExistsException(string message)
    {
        Message = message;
    }
}
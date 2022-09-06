using ExceptionApiHandler.Enums;

namespace ExceptionApiHandler.Exceptions;

public class ErrorException : Exception
{
    public ErrorType ErrorType { get; private set; }
    
    public ErrorException(ErrorType errorType,string message) : base(message)
    {
        ErrorType = errorType;
    }
}
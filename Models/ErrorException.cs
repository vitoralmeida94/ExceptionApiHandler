using ExceptionApiHandler.Enums;

namespace ExceptionApiHandler.Models;

public class ErrorException : Exception
{
    public ErrorType ErrorType { get; private set; }
    
    public ErrorException(ErrorType errorType,string message) : base(message)
    {
        ErrorType = errorType;
    }
}
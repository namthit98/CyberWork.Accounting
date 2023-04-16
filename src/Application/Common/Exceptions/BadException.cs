namespace CyberWork.Accounting.Application.Common.Exceptions;

public class BadException : Exception
{
    public BadException() : base()
    {
    }

    public BadException(string message) : base(message)
    {
    }
}
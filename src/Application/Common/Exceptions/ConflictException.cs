namespace CyberWork.Accounting.Application.Common.Exceptions;

public class ConflictException : Exception
{
    public ConflictException() : base()
    {
    }

    public ConflictException(string message) : base(message)
    {
    }

    public ConflictException(string name, string key, string value) 
        : base($"Entity \"{name}\" with {key}={value} is existed")
    {
    }
}
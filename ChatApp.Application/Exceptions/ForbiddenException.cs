namespace ChatApp.Application.Exceptions;

public sealed class ForbiddenException : ApplicationException
{
    public ForbiddenException(string message) : base(message)
    {
    }
}

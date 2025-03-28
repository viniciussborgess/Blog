namespace Blog.Domain.Data.Exceptions;

public class UserDomainException : BlogDomainException
{
    public UserDomainException(string message) : base(message) { }
    public UserDomainException(string message, Exception innerException) : base(message, innerException) { }
}

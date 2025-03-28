namespace Blog.Domain.Data.Exceptions;

public class BlogDomainException : Exception
{
    public BlogDomainException(string message) : base(message) { }
    public BlogDomainException(string message, Exception innerException) : base(message, innerException) { }
}

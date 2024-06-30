namespace TechTolk.Exceptions;

public class SourceException : TechTolkException
{
    public SourceException() : base() { }
    public SourceException(string message) : base(message) { }
    public SourceException(string message, Exception innerException) : base(message, innerException) { }
}

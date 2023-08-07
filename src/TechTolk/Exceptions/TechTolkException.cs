namespace TechTolk.Exceptions;

public class TechTolkException : Exception
{
    internal TechTolkException() : base() { }
    internal TechTolkException(string message) : base(message) { }
    internal TechTolkException(string message, Exception innerException) : base(message, innerException) { }
}

namespace TechTolk.Exceptions;

internal sealed class CompilationException : TechTolkException
{
    internal CompilationException(string message) : base(message) { }
    internal CompilationException(string message, Exception innerException) : base(message, innerException) { }
}

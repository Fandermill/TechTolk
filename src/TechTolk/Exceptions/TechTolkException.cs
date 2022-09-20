using System;

namespace TechTolk.Exceptions;

public class TechTolkException : Exception
{
    public TechTolkException() : base() { }
    public TechTolkException(string message) : base(message) { }
    public TechTolkException(string message, Exception innerException) : base(message, innerException) { }
}

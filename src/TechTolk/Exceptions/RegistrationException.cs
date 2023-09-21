namespace TechTolk.Exceptions;

public sealed class RegistrationException : TechTolkException
{
    internal RegistrationException(string message) : base(message) { }
}

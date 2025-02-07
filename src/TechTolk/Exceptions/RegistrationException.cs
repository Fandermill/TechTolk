namespace TechTolk.Exceptions;

public sealed class RegistrationException : TechTolkException
{
    public RegistrationException(string message) : base(message) { }
}
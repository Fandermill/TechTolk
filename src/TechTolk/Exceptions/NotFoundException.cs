namespace TechTolk.Exceptions;

public abstract class NotFoundException : TechTolkException
{
    internal protected NotFoundException(string message) : base(message) { }
}
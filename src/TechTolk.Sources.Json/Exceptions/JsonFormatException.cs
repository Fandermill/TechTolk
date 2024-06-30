using TechTolk.Exceptions;

namespace TechTolk.Sources.Json.Exceptions;

public sealed class JsonFormatException : SourceException
{
    internal JsonFormatException(string message) : base(message) { }
}

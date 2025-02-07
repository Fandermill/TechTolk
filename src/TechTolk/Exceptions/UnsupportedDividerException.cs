using TechTolk.Division;

namespace TechTolk.Exceptions;

public sealed class UnsupportedDividerException : TechTolkException
{
    internal UnsupportedDividerException(string message) : base(message) { }
    internal UnsupportedDividerException(IDivider divider) : this(
        $"Divider '{divider.Key}' is not supported"
        )
    { }
}
namespace TechTolk.Exceptions;

public sealed class TranslationSetNotFoundException : NotFoundException
{
    internal TranslationSetNotFoundException(string key) : base(FormatMessage(key)) { }

    private static string FormatMessage(string key)
    {
        return $"No translation set registration found for key '{key}'";
    }
}
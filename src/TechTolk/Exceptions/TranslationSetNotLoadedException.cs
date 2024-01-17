namespace TechTolk.Exceptions;

public sealed class TranslationSetNotLoadedException : TechTolkException
{
    internal TranslationSetNotLoadedException(string key) :
        base($"Translation set registration with key '{key}' was not loaded")
    { }
}

using TechTolk.Registration;
using TechTolk.Sources;

namespace TechTolk.Tests.TestTranslationSets;

public class SimpleInstantiateTranslationSetSourceFactory<T> : ITranslationSetSourceFactory<T> where T : ITranslationSetSource, new()
{
    public ITranslationSetSource Create(ResolveSourceRegistration sourceRegistration)
    {
        return new T();
    }
}

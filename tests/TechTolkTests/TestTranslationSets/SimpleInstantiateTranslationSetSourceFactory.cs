using TechTolk.Registration;
using TechTolk.Sources;

namespace TechTolkTests.TestTranslationSets;

public class SimpleInstantiateTranslationSetSourceFactory<T> : ITranslationSetSourceFactory<T> where T : ITranslationSetSource, new()
{
    public ITranslationSetSource Create(ResolveSourceRegistration sourceRegistration)
    {
        return new T();
    }
}

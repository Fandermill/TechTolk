using TechTolk.Registration;

namespace TechTolk.Sources;

public interface ITranslationSetSourceFactory
{
    ITranslationSetSource Create(ResolveSourceRegistration sourceRegistration);
}

public interface ITranslationSetSourceFactory<TSource> : ITranslationSetSourceFactory
    where TSource : ITranslationSetSource
{
}
using TechTolk.Sources;

namespace TechTolk.Registration.Builders;

internal sealed class RootTranslationSetRegistrationBuilder : IRootTranslationSetRegistrationBuilder
{
    public ITechTolkBuilder RootBuilder { get; private init; }

    private readonly TranslationSetRegistration _registration;

    public RootTranslationSetRegistrationBuilder(ITechTolkBuilder rootBuilder, TranslationSetRegistration registration)
    {
        RootBuilder = rootBuilder;
        _registration = registration;
    }

    public void WithOptions(Action<ITranslationSetOptionsBuilder> options)
    {
        options(new TranslationSetOptionsBuilder(RootBuilder, _registration.Options));
    }


    public void FromSource<T>() where T : ITranslationSetSource
        => FromSource<T>(null);

    public void FromSource<T>(Func<TranslationSetSourceOptions>? options) where T : ITranslationSetSource
    {
        var sourceRegistration = new ResolveSourceRegistration(
            _registration.Name,
            typeof(T),
            options is not null ? options() : null);

        _registration.Sources.Add(sourceRegistration);
    }

    public void FromSource(ITranslationSetSource source)
        => FromSource(source, null);

    public void FromSource(ITranslationSetSource source, Func<TranslationSetSourceOptions>? options)
    {
        var sourceRegistration = new SourceInstanceRegistration(
            _registration.Name,
            source,
            options is not null ? options() : null);

        _registration.Sources.Add(sourceRegistration);
    }
}
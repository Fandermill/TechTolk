using TechTolk.Sources;
using TechTolk.TranslationSets.Merging;

namespace TechTolk.Registration.Builders;

internal sealed class MergedTranslationSetRegistrationBuilder : IMergedTranslationSetRegistrationBuilder
{
    public ITechTolkBuilder RootBuilder { get; private init; }

    private readonly TranslationSetRegistration _registration;

    public MergedTranslationSetRegistrationBuilder(ITechTolkBuilder rootBuilder, TranslationSetRegistration registration)
    {
        RootBuilder = rootBuilder;

        _registration = registration;
        _registration.MergeOptions = new TranslationSetMergeOptions()
        {
            MergedSetName = registration.Name
        };
    }

    public void WithOptions(Action<IMergedTranslationSetOptionsBuilder> options)
    {
        options(new MergedTranslationSetOptionsBuilder(RootBuilder, _registration.Options, _registration.MergeOptions!));
    }

    public void FromSource<T>(string name) where T : ITranslationSetSource
        => FromSource<T>(name, null);

    public void FromSource<T>(string name, Func<TranslationSetSourceOptions>? options) where T : ITranslationSetSource
    {
        var sourceRegistration = new ResolveSourceRegistration(
            name,
            typeof(T),
            options is not null ? options() : null);

        _registration.Sources.Add(sourceRegistration);
    }

    public void FromSource(string name, ITranslationSetSource source)
        => FromSource(name, source, null);

    public void FromSource(string name, ITranslationSetSource source, Func<TranslationSetSourceOptions>? options)
    {
        var sourceRegistration = new SourceInstanceRegistration(
            name,
            source,
            options is not null ? options() : null);

        _registration.Sources.Add(sourceRegistration);
    }
}
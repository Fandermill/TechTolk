using TechTolk.Division;
using TechTolk.Sources;
using TechTolk.TranslationSets.Values;

namespace TechTolk.Registration.Builders;

public interface ITechTolkBuilder
{
    ITechTolkBuilder ConfigureDividers(Action<IDividerConfigurationBuilder> dividerConfiguration);

    ITechTolkBuilder AddTranslationSet(string name, Action<IRootTranslationSetRegistrationBuilder> set);
    ITechTolkBuilder AddTranslationSet<T>(Action<IRootTranslationSetRegistrationBuilder> set);

    ITechTolkBuilder AddMergedTranslationSet(string name, Action<IMergedTranslationSetRegistrationBuilder> mergedSet);
	ITechTolkBuilder AddMergedTranslationSet<T>(Action<IMergedTranslationSetRegistrationBuilder> mergedSet);
}

public interface IDividerConfigurationBuilder
{
    IDividerConfigurationBuilder SetCurrentDividerProvider<T>() where T : ICurrentDividerProvider;
    IDividerConfigurationBuilder SetCurrentDividerProvider<T>(Func<IServiceProvider, ICurrentDividerProvider> provider) where T : ICurrentDividerProvider;
    IDividerConfigurationBuilder AddSupportedDivider(IDivider divider);
    // TODO - Set fallback, aliases
}
public interface ITranslationSetOptionsBuilder
{
    ITranslationSetOptionsBuilder UseValueRenderer<T>() where T : ITranslationValueFactory;
    ITranslationNotFoundBehaviorConfigurationBuilder OnTranslationNotFound();
}
public interface IMergedTranslationSetOptionsBuilder : ITranslationSetOptionsBuilder
{
    IDuplicateKeyBehaviorConfigurationBuilder OnDuplicateKey();
}
public interface ITranslationNotFoundBehaviorConfigurationBuilder
{
    ITranslationSetOptionsBuilder ThrowException();
    ITranslationSetOptionsBuilder ReturnEmptyString();
    ITranslationSetOptionsBuilder ReturnTranslationKey();
}
public interface IDuplicateKeyBehaviorConfigurationBuilder
{
    IMergedTranslationSetOptionsBuilder Replace();
    IMergedTranslationSetOptionsBuilder Discard();
    IMergedTranslationSetOptionsBuilder ThrowException();
}

public interface IRootTranslationSetRegistrationBuilder
{
    void WithOptions(Action<ITranslationSetOptionsBuilder> options);

    void FromSource<T>() where T : ITranslationSetSource;
    void FromSource<T>(Func<TranslationSetSourceOptions>? options) where T : ITranslationSetSource;
    void FromSource(ITranslationSetSource source);
    void FromSource(ITranslationSetSource source, Func<TranslationSetSourceOptions>? options);
}
public interface IMergedTranslationSetRegistrationBuilder
{
    void WithOptions(Action<IMergedTranslationSetOptionsBuilder> options);
    void FromSource<T>(string name) where T : ITranslationSetSource;
    void FromSource<T>(string name, Func<TranslationSetSourceOptions>? options) where T : ITranslationSetSource;
    void FromSource(string name, ITranslationSetSource source);
    void FromSource(string name, ITranslationSetSource source, Func<TranslationSetSourceOptions>? options);
}

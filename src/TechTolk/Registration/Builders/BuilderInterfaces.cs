using Microsoft.Extensions.DependencyInjection;
using TechTolk.Division;
using TechTolk.Rendering;
using TechTolk.Sources;

namespace TechTolk.Registration.Builders;

public interface ITechTolkBuilder
{
    IServiceCollection Services { get; }
    ITechTolkBuilder ConfigureDividers(Action<IDividerConfigurationBuilder> dividerConfiguration);
    ITechTolkBuilder ConfigureDefaultOptions(Action<ITranslationSetOptionsBuilder> setOptions);

    ITechTolkBuilder AddTranslationSet(string name, Action<IRootTranslationSetRegistrationBuilder> set);
    ITechTolkBuilder AddTranslationSet<T>(Action<IRootTranslationSetRegistrationBuilder> set);

    ITechTolkBuilder AddMergedTranslationSet(string name, Action<IMergedTranslationSetRegistrationBuilder> mergedSet);
    ITechTolkBuilder AddMergedTranslationSet<T>(Action<IMergedTranslationSetRegistrationBuilder> mergedSet);
}

public interface IDividerConfigurationBuilder
{
    ITechTolkBuilder RootBuilder { get; }

    /// <summary>
    /// Registers the given type as the implementation of the 
    /// <see cref="ICurrentDividerProvider"/> interface
    /// </summary>
    /// <typeparam name="T">The implementation type</typeparam>
    /// <returns>The builder to chain calls on</returns>
    IDividerConfigurationBuilder SetCurrentDividerProvider<T>() where T : ICurrentDividerProvider;

    /// <summary>
    /// Registers a factory method to create an implementation of the 
    /// <see cref="ICurrentDividerProvider"/> interface
    /// </summary>
    /// <typeparam name="T">The implementation type</typeparam>
    /// <param name="provider"></param>
    /// <returns>The builder to chain calls on</returns>
    IDividerConfigurationBuilder SetCurrentDividerProvider<T>(Func<IServiceProvider, ICurrentDividerProvider> provider) where T : ICurrentDividerProvider;
    IDividerConfigurationBuilder AddSupportedDivider(IDivider divider);
}

public interface ITranslationSetOptionsBuilder
{
    ITechTolkBuilder RootBuilder { get; }
    ITranslationSetOptionsBuilder UseValueRenderer<T>() where T : AbstractTranslationValueRenderer;
    ITranslationNotFoundBehaviorConfigurationBuilder OnTranslationNotFound();
    ITranslationSetNotLoadedBehaviorConfigurationBuilder OnTranslationSetNotLoaded();
}
public interface IMergedTranslationSetOptionsBuilder : ITranslationSetOptionsBuilder
{
    IDuplicateKeyBehaviorConfigurationBuilder OnDuplicateKey();
}
public interface ITranslationSetNotLoadedBehaviorConfigurationBuilder
{
    ITranslationSetOptionsBuilder ThrowException();
    ITranslationSetOptionsBuilder LazyLoad();
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
    ITechTolkBuilder RootBuilder { get; }
    void WithOptions(Action<ITranslationSetOptionsBuilder> options);

    void FromSource<T>() where T : ITranslationSetSource;
    void FromSource<T>(Func<TranslationSetSourceOptions>? options) where T : ITranslationSetSource;
    void FromSource(ITranslationSetSource source);
    void FromSource(ITranslationSetSource source, Func<TranslationSetSourceOptions>? options);
}
public interface IMergedTranslationSetRegistrationBuilder
{
    ITechTolkBuilder RootBuilder { get; }
    void WithOptions(Action<IMergedTranslationSetOptionsBuilder> options);
    void FromSource<T>(string name) where T : ITranslationSetSource;
    void FromSource<T>(string name, Func<TranslationSetSourceOptions>? options) where T : ITranslationSetSource;
    void FromSource(string name, ITranslationSetSource source);
    void FromSource(string name, ITranslationSetSource source, Func<TranslationSetSourceOptions>? options);
}

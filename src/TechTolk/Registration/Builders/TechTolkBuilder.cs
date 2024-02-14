using Microsoft.Extensions.DependencyInjection;
using TechTolk.Division;
using TechTolk.Division.Internals;
using TechTolk.TranslationSets.Options.Internals;

namespace TechTolk.Registration.Builders;

internal class TechTolkBuilder : ITechTolkBuilder
{
    private readonly IServiceCollection _services;

    private readonly SupportedDividersProvider _supportedDividersProvider;
    private readonly TranslationSetOptions _defaultOptions;
    private readonly TranslationSetRegistrationCollection _registrations;


    public TechTolkBuilder(IServiceCollection services, Action<ITranslationSetOptionsBuilder>? defaultOptions)
    {
        _services = services;

        _supportedDividersProvider = new SupportedDividersProvider();
        _services.AddSingleton<ISupportedDividersProvider>(_supportedDividersProvider);
        _services.AddSingleton<ICurrentDividerProvider, CurrentThreadUICultureDividerProvider>();

        _defaultOptions = new();
        if (defaultOptions is not null)
            ConfigureDefaultOptions(defaultOptions);

        _registrations = new();
        _services.AddSingleton(_registrations);
    }

    private void ConfigureDefaultOptions(Action<ITranslationSetOptionsBuilder> configureDefaultOptions)
    {
        var optionsBuilder = new TranslationSetOptionsBuilder(_defaultOptions);
        configureDefaultOptions(optionsBuilder);
    }

    public ITechTolkBuilder ConfigureDividers(Action<IDividerConfigurationBuilder> dividerConfiguration)
    {
        var builder = new DividerConfigurationBuilder(_services, _supportedDividersProvider);
        dividerConfiguration(builder);
        return this;
    }

    public ITechTolkBuilder AddTranslationSet(string name, Action<IRootTranslationSetRegistrationBuilder> set)
    {
        var registration = _registrations.Create(name, _defaultOptions.Clone());
        var builder = new RootTranslationSetRegistrationBuilder(registration);
        set(builder);
        return this;
    }

    public ITechTolkBuilder AddTranslationSet<T>(Action<IRootTranslationSetRegistrationBuilder> set)
    {
        // todo - Get metadata from T

        var name = typeof(T).ToTranslationSetKey();
        return AddTranslationSet(name, set);
    }

    public ITechTolkBuilder AddMergedTranslationSet(string name, Action<IMergedTranslationSetRegistrationBuilder> mergedSet)
    {
        var registration = _registrations.Create(name, _defaultOptions.Clone());
        var builder = new MergedTranslationSetRegistrationBuilder(registration);
        mergedSet(builder);
        return this;
    }
    public ITechTolkBuilder AddMergedTranslationSet<T>(Action<IMergedTranslationSetRegistrationBuilder> mergedSet)
    {
        // todo - Get metadata from T

        var name = typeof(T).ToTranslationSetKey();
        return AddMergedTranslationSet(name, mergedSet);
    }
}

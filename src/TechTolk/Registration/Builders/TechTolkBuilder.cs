using Microsoft.Extensions.DependencyInjection;
using TechTolk.Division;
using TechTolk.Division.Internals;
using TechTolk.TranslationSets.Options.Internals;

namespace TechTolk.Registration.Builders;

internal sealed class TechTolkBuilder : ITechTolkBuilder
{
    public IServiceCollection Services { get; private set; }

    private readonly SupportedDividersProvider _supportedDividersProvider;
    private readonly TranslationSetOptions _defaultOptions;
    private readonly TranslationSetRegistrationCollection _registrations;


    public TechTolkBuilder(IServiceCollection services, Action<ITranslationSetOptionsBuilder>? defaultOptions)
    {
        Services = services;

        _supportedDividersProvider = new SupportedDividersProvider();
        Services.AddSingleton<ISupportedDividersProvider>(_supportedDividersProvider);
        Services.AddSingleton<ICurrentDividerProvider, CurrentThreadUICultureDividerProvider>();

        _defaultOptions = new();
        if (defaultOptions is not null)
            ConfigureDefaultOptions(defaultOptions);

        _registrations = new();
        Services.AddSingleton(_registrations);
    }

    public ITechTolkBuilder ConfigureDefaultOptions(Action<ITranslationSetOptionsBuilder> setOptions)
    {
        var optionsBuilder = new TranslationSetOptionsBuilder(this, _defaultOptions);
        setOptions(optionsBuilder);
        return this;
    }

    public ITechTolkBuilder ConfigureDividers(Action<IDividerConfigurationBuilder> dividerConfiguration)
    {
        var builder = new DividerConfigurationBuilder(this, _supportedDividersProvider);
        dividerConfiguration(builder);
        return this;
    }

    public ITechTolkBuilder AddTranslationSet(string name, Action<IRootTranslationSetRegistrationBuilder> set)
    {
        var registration = _registrations.Create(name, _defaultOptions.Clone());
        var builder = new RootTranslationSetRegistrationBuilder(this, registration);
        set(builder);
        return this;
    }

    public ITechTolkBuilder AddTranslationSet<T>(Action<IRootTranslationSetRegistrationBuilder> set)
    {
        var name = typeof(T).ToTranslationSetKey();
        return AddTranslationSet(name, set);
    }

    public ITechTolkBuilder AddMergedTranslationSet(string name, Action<IMergedTranslationSetRegistrationBuilder> mergedSet)
    {
        var registration = _registrations.Create(name, _defaultOptions.Clone());
        var builder = new MergedTranslationSetRegistrationBuilder(this, registration);
        mergedSet(builder);
        return this;
    }
    public ITechTolkBuilder AddMergedTranslationSet<T>(Action<IMergedTranslationSetRegistrationBuilder> mergedSet)
    {
        var name = typeof(T).ToTranslationSetKey();
        return AddMergedTranslationSet(name, mergedSet);
    }
}

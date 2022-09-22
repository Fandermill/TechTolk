using System;
using System.Collections.Generic;
using TechTolk.Dividing;

namespace TechTolk.Compiling;

public sealed class TolkBuilder<T> : ITolkBuilder<T>
{
    private readonly List<ITranslationSetRegistration<T>> _setRegistrations;
    private ICurrentDividerProvider? _currentDividerProvider;

    public TolkBuilder()
    {
        _setRegistrations = new List<ITranslationSetRegistration<T>>();
    }


    public ITranslationSetRegistration<T> AddTranslationSet(Func<ITranslationSet<T>> getTranslationSet)
    {
        return AddTranslationSet(new DelegateTranslationSetProvider<T>(getTranslationSet));
    }

    public ITranslationSetRegistration<T> AddTranslationSet(ITranslationSetProvider<T> translationSetProvider)
    {
        var registration = new TranslationSetRegistration<T>(this, translationSetProvider);
        _setRegistrations.Add(registration);
        return registration;
    }



    public ITolkBuilder<T> WithDividerProvider(ICurrentDividerProvider provider)
    {
        _currentDividerProvider = provider ?? throw new ArgumentNullException(nameof(provider));
        return this;
    }

    public ITolk<T> Compile()
    {
        // todo - fetch all translations and merge them to a flat structure

        // then instantiate Tolk

        throw new NotImplementedException();
    }
}

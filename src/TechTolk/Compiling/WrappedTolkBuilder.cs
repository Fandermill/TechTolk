using System;
using TechTolk.Dividing;

namespace TechTolk.Compiling;

public abstract class WrappedTolkBuilder<T> : ITolkBuilder<T>
{
    protected ITolkBuilder<T> _builder;

    public WrappedTolkBuilder(ITolkBuilder<T> builder)
    {
        _builder = builder ?? throw new ArgumentNullException(nameof(builder));
    }

    public ITranslationSetRegistration<T> AddTranslationSet(Func<ITranslationSet<T>> getTranslationSet)
    {
        return _builder.AddTranslationSet(getTranslationSet);
    }

    public ITranslationSetRegistration<T> AddTranslationSet(ITranslationSetProvider<T> translationSetProvider)
    {
        return _builder.AddTranslationSet(translationSetProvider);
    }

    public ITolkBuilder<T> WithDividerProvider(ICurrentDividerProvider provider)
    {
        return _builder.WithDividerProvider(provider);
    }

    public ITolk<T> Compile()
    {
        return _builder.Compile();
    }
}

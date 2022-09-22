using System;

namespace TechTolk.Compiling;

public class DelegateTranslationSetProvider<T> : ITranslationSetProvider<T>
{
    private readonly Func<ITranslationSet<T>> _func;

    public DelegateTranslationSetProvider(Func<ITranslationSet<T>> func)
    {
        _func = func ?? throw new ArgumentNullException(nameof(func));
    }

    public ITranslationSet<T> GetTranslationSet()
    {
        return _func();
    }
}

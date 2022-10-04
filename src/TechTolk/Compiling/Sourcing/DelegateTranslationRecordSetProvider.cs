using System;

namespace TechTolk.Compiling.Sourcing;

public sealed class DelegateTranslationRecordSetProvider<T> : ITranslationRecordSetProvider<T>
{
    private readonly Func<ITranslationRecordSet<T>> _func;

    public DelegateTranslationRecordSetProvider(Func<ITranslationRecordSet<T>> func)
    {
        _func = func ?? throw new ArgumentNullException(nameof(func));
    }

    public ITranslationRecordSet<T> GetSet()
    {
        return _func();
    }
}

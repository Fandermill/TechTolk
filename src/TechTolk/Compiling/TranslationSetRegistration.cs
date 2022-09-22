using System;
using TechTolk.Exceptions;

namespace TechTolk.Compiling;

public class TranslationSetRegistration<T> : WrappedTolkBuilder<T>, ITranslationSetRegistration<T>
{
    private bool _overwriteDuplicates = false;

    private readonly ITranslationSetProvider<T> _translationSetProvider;

    public TranslationSetRegistration(
        ITolkBuilder<T> builder,
        ITranslationSetProvider<T> translationSetProvider)
        : base(builder)
    {
        _builder = builder ?? throw new ArgumentNullException(nameof(builder));
        _translationSetProvider = translationSetProvider ?? throw new ArgumentNullException(nameof(translationSetProvider));
    }

    public ITranslationSetRegistration<T> OverwriteDuplicates()
    {
        _overwriteDuplicates = true;
        return this;
    }

    public ITranslationSet<T> GetTranslationSet()
    {
        return _translationSetProvider.GetTranslationSet()
            ?? throw new TechTolkException("Function returned no translation set");
    }
}

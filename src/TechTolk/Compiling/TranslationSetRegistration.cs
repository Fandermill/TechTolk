using System;
using TechTolk.Exceptions;

namespace TechTolk.Compiling;

public class TranslationSetRegistration<T> : WrappedCompilableTolkCompilation<T>, ITranslationSetRegistration<T>
{
    public bool DiscardDuplicates { get; private set; } = false;

    private readonly ITranslationSetProvider<T> _translationSetProvider;

    public TranslationSetRegistration(
        ITolkCompilation<T> compilation,
        ITranslationSetProvider<T> translationSetProvider)
        : base(compilation)
    {
        _translationSetProvider = translationSetProvider ?? throw new ArgumentNullException(nameof(translationSetProvider));
    }

    public ITranslationSetRegistration<T> DiscardDuplicatesOnMerge()
    {
        DiscardDuplicates = true;
        return this;
    }

    public ITranslationSet<T> GetTranslationSet()
    {
        return _translationSetProvider.GetTranslationSet()
            ?? throw new TechTolkException("Function returned no translation set");
    }
}

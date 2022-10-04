using System;
using TechTolk.Compiling.Sourcing;
using TechTolk.Exceptions;

namespace TechTolk.Compiling.Compiler;

public class TranslationSetRegistration<T> : WrappedCompilableTolkCompilation<T>, ITranslationSetRegistration<T>
{
    public bool DiscardDuplicates { get; private set; } = false;

    private readonly ITranslationRecordSetProvider<T> _translationSetProvider;

    public TranslationSetRegistration(
        ITolkCompilation<T> compilation,
        ITranslationRecordSetProvider<T> translationSetProvider)
        : base(compilation)
    {
        _translationSetProvider = translationSetProvider ?? throw new ArgumentNullException(nameof(translationSetProvider));
    }

    public ITranslationSetRegistration<T> DiscardDuplicatesOnMerge()
    {
        DiscardDuplicates = true;
        return this;
    }

    public ITranslationRecordSet<T> GetTranslationSet()
    {
        return _translationSetProvider.GetSet()
            ?? throw new TechTolkException("Function returned no translation set");
    }
}

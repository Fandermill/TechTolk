using System;
using TechTolk.Dividing;

namespace TechTolk.Compiling;

public abstract class WrappedCompilableTolkCompilation<T> : ITolkCompilation<T>, ICompilableTolkCompiler<T>
{
    protected ITolkCompilation<T> _compilation;

    public WrappedCompilableTolkCompilation(ITolkCompilation<T> compilation)
    {
        _compilation = compilation ?? throw new ArgumentNullException(nameof(compilation));
    }

    public void WithDivider(ICurrentDividerProvider dividerProvider)
    {
        _compilation.WithDivider(dividerProvider);
    }

    public void WithMerger(ITranslationSetMerger<T> merger)
    {
        _compilation.WithMerger(merger);
    }

    public ITranslationSetRegistration<T> AddTranslationSet(Func<ITranslationSet<T>> getTranslationSet)
    {
        return _compilation.AddTranslationSet(getTranslationSet);
    }

    public ITranslationSetRegistration<T> AddTranslationSet(ITranslationSetProvider<T> translationSetProvider)
    {
        return _compilation.AddTranslationSet(translationSetProvider);
    }

    public ITolk<T> Compile()
    {
        return _compilation.Compile();
    }
}

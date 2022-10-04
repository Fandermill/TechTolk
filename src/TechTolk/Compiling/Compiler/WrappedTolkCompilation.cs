using System;
using TechTolk.Compiling.Merging;
using TechTolk.Compiling.Sourcing;
using TechTolk.Dividing;

namespace TechTolk.Compiling.Compiler;

public abstract class WrappedCompilableTolkCompilation<T> : ITolkCompiler<T>, ICompilableTolkCompiler<T>
{
    protected ITolkCompiler<T> _compiler;

    public WrappedCompilableTolkCompilation(ITolkCompiler<T> compiler)
    {
        _compiler = compiler ?? throw new ArgumentNullException(nameof(compiler));
    }

    public void WithDivider(ICurrentDividerProvider dividerProvider)
    {
        _compiler.WithDivider(dividerProvider);
    }

    public void WithMerger(ITranslationRecordSetMerger<T> merger)
    {
        _compiler.WithMerger(merger);
    }

    public ITranslationSetRegistration<T> AddTranslationSet(Func<ITranslationRecordSet<T>> getTranslationSet)
    {
        return _compiler.AddTranslationSet(getTranslationSet);
    }

    public ITranslationSetRegistration<T> AddTranslationSet(ITranslationRecordSetProvider<T> translationSetProvider)
    {
        return _compiler.AddTranslationSet(translationSetProvider);
    }

    public ITolk<T> Compile()
    {
        return _compiler.Compile();
    }
}

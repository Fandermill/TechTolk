using System;
using TechTolk.Dividing;

namespace TechTolk.Compiling.Compiler;

public interface ITolkCompilation<T>
{
    void WithDivider(ICurrentDividerProvider dividerProvider);
    void WithMerger(ITranslationSetMerger<T> merger);

    ITranslationSetRegistration<T> AddTranslationSet(Func<ITranslationSet<T>> getTranslationSet);
    ITranslationSetRegistration<T> AddTranslationSet(ITranslationSetProvider<T> translationSetProvider);

    ITolk<T> Compile();
}
using System;
using TechTolk.Compiling.Merging;
using TechTolk.Compiling.Sourcing;
using TechTolk.Dividing;

namespace TechTolk.Compiling.Compiler;

public interface ITolkCompiler<T>
{
    void WithDivider(ICurrentDividerProvider dividerProvider);
    void WithMerger(ITranslationRecordSetMerger<T> merger);

    
    ITranslationSetRegistration<T> AddTranslationSet(Func<ITranslationRecordSet<T>> getTranslationSet);
    ITranslationSetRegistration<T> AddTranslationSet(ITranslationRecordSetProvider<T> translationSetProvider);

    ITolk<T> Compile();
}
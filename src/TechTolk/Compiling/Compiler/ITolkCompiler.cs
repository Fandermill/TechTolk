using System;
using TechTolk.Compiling.Converting;
using TechTolk.Compiling.Merging;
using TechTolk.Compiling.Sourcing;
using TechTolk.Dividing;

namespace TechTolk.Compiling.Compiler;

public interface ITolkCompiler<T>
{
    void WithCurrentDividerProvider(ICurrentDividerProvider currentDividerProvider);
    void WithMerger(ITranslationRecordSetMerger<T> merger);
    void WithTranslationSetConverter(ITranslationSetConverter<T> translationSetConverter);


    ITranslationSetRegistration<T> AddTranslationSet(Func<ITranslationRecordSet<T>> getTranslationSet);
    ITranslationSetRegistration<T> AddTranslationSet(ITranslationRecordSetProvider<T> translationSetProvider);

    ITolk<T> Compile();
}
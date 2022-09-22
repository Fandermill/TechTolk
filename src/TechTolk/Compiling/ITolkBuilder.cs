using System;
using TechTolk.Dividing;

namespace TechTolk.Compiling;

public interface ITolkBuilder<T>
{
    ITranslationSetRegistration<T> AddTranslationSet(Func<ITranslationSet<T>> getTranslationSet);
    ITranslationSetRegistration<T> AddTranslationSet(ITranslationSetProvider<T> translationSetProvider);

    ITolkBuilder<T> WithDividerProvider(ICurrentDividerProvider provider);

    ITolk<T> Compile();
}
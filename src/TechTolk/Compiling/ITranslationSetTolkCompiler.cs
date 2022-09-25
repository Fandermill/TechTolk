using System;

namespace TechTolk.Compiling;

public interface ITranslationSetTolkCompiler<T>
{
    ITranslationSetRegistration<T> AddTranslationSet(Func<ITranslationSet<T>> getTranslationSet);
    ITranslationSetRegistration<T> AddTranslationSet(ITranslationSetProvider<T> translationSetProvider);
}

using System;
using TechTolk.Compiling.Sourcing;

namespace TechTolk.Compiling.Compiler;

public interface ITranslationSetTolkCompiler<T>
{
    ITranslationSetRegistration<T> AddTranslationSet(Func<ITranslationRecordSet<T>> getTranslationSet);
    ITranslationSetRegistration<T> AddTranslationSet(ITranslationRecordSetProvider<T> translationSetProvider);
}

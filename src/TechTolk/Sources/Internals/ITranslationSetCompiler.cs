using TechTolk.Registration;
using TechTolk.TranslationSets;

namespace TechTolk.Sources.Internals;

internal interface ITranslationSetCompiler
{
    Task<ITranslationSet> CompileTranslationSetAsync(TranslationSetRegistration registration);
    ITranslationSet CompileTranslationSetSynchronously(TranslationSetRegistration registration);
}
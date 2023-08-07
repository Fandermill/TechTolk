using TechTolk.Registration;
using TechTolk.TranslationSets;

namespace TechTolk.Sources.Internals;

internal interface ITranslationSetCompiler
{
    ITranslationSet CompileTranslationSet(TranslationSetRegistration registration);
}

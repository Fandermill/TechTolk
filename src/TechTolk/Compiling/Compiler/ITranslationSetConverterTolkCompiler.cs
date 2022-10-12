using TechTolk.Compiling.Converting;

namespace TechTolk.Compiling.Compiler;

public interface ITranslationSetConverterTolkCompiler<T>
{
    ITranslationSetTolkCompiler<T> WithTranslationSetConverter(ITranslationSetConverter<T> translationSetConverter);
}

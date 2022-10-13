using TechTolk.Translations;

namespace TechTolk.Compiling.Parsing;

public class StringTranslationValueParser : ITranslationValueParser<string>
{
    public ITranslation<string>? Parse(string? value)
    {
        if (value is null) return null;
        
        // TODO - Look for {Var} syntaxes and
        //        if found, create a MergeTranslation

        return new Translation<string>(value);
    }
}

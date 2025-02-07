using System.Text.RegularExpressions;
using TechTolk.Division;
using TechTolk.TranslationSets.Values;

namespace TechTolk.Rendering.Internals;

/// <summary>
/// This value renderer uses the anonymous object data as a value bag and replaces
/// tokens in the string with the values from the value bag. Tokens should be enclosed
/// in curly brackets, like '{MyToken}'.
/// If the renderer is called with an object? array, then the tokens are replaced in order.
/// </summary>
internal sealed class ValueBagTranslationValueRenderer : AbstractTranslationValueRenderer
{
    private static readonly Regex TOKEN_REGEX = new Regex(@"{.*?}", RegexOptions.Compiled);

    private readonly ValueBagConverter _valueBagConverter;


    public ValueBagTranslationValueRenderer() : base()
    {
        _valueBagConverter = new ValueBagConverter();
    }

    public override string Render(IDivider divider, TranslationValue value, object? data)
    {
        if (data is null)
            return value.Value;

        var tokens = TOKEN_REGEX.Matches(value.Value);
        if (tokens.Count == 0)
            return value.Value;

        if (data is object?[] dataArray)
            return ReplaceTokensInOrder(value.Value, tokens, dataArray);

        var valueBag = _valueBagConverter.ConvertFromObject(data);
        return ReplaceTokensByName(value.Value, tokens, valueBag);
    }

    private string ReplaceTokensInOrder(string value, MatchCollection tokens, object?[] dataArray)
    {
        string result = value;
        for (int i = 0; i < tokens.Count; i++)
        {
            var replacement = (dataArray.Length > i ? dataArray[i] : null)?.ToString();
            if (replacement is not null)
                result = result.Replace(tokens[i].Value, replacement);
        }
        return result;
    }

    private string ReplaceTokensByName(string value, MatchCollection tokens, ValueBag valueBag)
    {
        string result = value;
        foreach (Match token in tokens)
        {
            var replacement = valueBag[StripBrackets(token.Value)]?.ToString();
            if (replacement is not null)
                result = result.Replace(token.Value, replacement);
        }
        return result;
    }

    private string StripBrackets(string token)
    {
        return token.Replace("{", "").Replace("}", "");
    }
}
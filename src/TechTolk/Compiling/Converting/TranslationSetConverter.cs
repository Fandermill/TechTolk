using System.Linq;
using TechTolk.Compiling.Parsing;
using TechTolk.Compiling.Sourcing;

namespace TechTolk.Compiling.Converting;

public class TranslationSetConverter<T> : ITranslationSetConverter<T>
{
    private ITranslationValueParser<T> _translationValueParser;

    public TranslationSetConverter(ITranslationValueParser<T> translationValueParser)
    {
        _translationValueParser = translationValueParser;
    }

    public ITranslationSet<T> FromRecordSet(ITranslationRecordSet<T> recordSet)
    {
        var set = new TranslationSet<T>(recordSet.SetInfo.Name);
        foreach (var dividerRecords in recordSet.Records.GroupBy(r => r.Divider.GetKey()))
        {
            var dividerDictionary = new TranslationDictionary<T>(recordSet.SetInfo.Name + ".compiled");
            foreach (var record in dividerRecords)
            {
                var translation = _translationValueParser.Parse(record.Translation);

                if (translation is not null)
                    dividerDictionary.Add(record.Key, translation);
            }
            set.AddDivision(dividerRecords.Key, dividerDictionary);
        }
        return set;
    }
}
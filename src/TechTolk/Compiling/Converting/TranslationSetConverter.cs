using System.Linq;
using TechTolk.Compiling.Sourcing;
using TechTolk.Translations;

namespace TechTolk.Compiling.Converting;

public class TranslationSetConverter<T> : ITranslationSetConverter<T>
{
    public ITranslationSet<T> FromRecordSet(ITranslationRecordSet<T> recordSet)
    {
        var set = new TranslationSet<T>(recordSet.SetInfo.Name);
        foreach (var dividerRecords in recordSet.Records.GroupBy(r => r.Divider))
        {
            var dividerDictionary = new TranslationDictionary<T>(recordSet.SetInfo.Name + ".compiled");
            foreach (var record in dividerRecords)
            {
                if (record.Translation is not null)
                    dividerDictionary.Add(record.Key, CreateTranslation(record.Translation));
            }
            set.AddDivision(dividerRecords.Key.GetKey(), dividerDictionary);
        }
        return set;
    }

    private ITranslation<T> CreateTranslation(T translation)
    {
        // TODO - parse translation into right translation object type
        return new Translation<T>(translation);
    }
}
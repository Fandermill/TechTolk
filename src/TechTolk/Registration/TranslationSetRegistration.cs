using TechTolk.TranslationSets.Merging;
using TechTolk.TranslationSets.Options;

namespace TechTolk.Registration;

public class TranslationSetRegistration
{
    public string Key { get; init; }
    public string Name { get; init; }

    public TranslationSetOptions Options { get; init; }
    internal TranslationSetMergeOptions? MergeOptions { get; set; }

    internal bool Merge => Sources.Count > 1;
    internal List<SourceRegistrationBase> Sources { get; } = new();

    internal TranslationSetRegistration(string key) : this(key, new()) { }
    internal TranslationSetRegistration(string key, TranslationSetOptions options)
    { 
        Key = key; 
        Name = key;

        Options = options;
    }
}

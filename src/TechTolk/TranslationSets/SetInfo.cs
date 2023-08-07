namespace TechTolk.TranslationSets;

public class SetInfo
{
    public string Key { get; private set; }
    public string Name { get; private set; }

    public SetInfo(string key, string name)
    {
        ArgumentNullException.ThrowIfNull(key);
        ArgumentNullException.ThrowIfNull(name);
        Key = key;
        Name = name;
    }
}
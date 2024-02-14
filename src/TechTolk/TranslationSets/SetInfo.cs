namespace TechTolk.TranslationSets;

/// <summary>
/// This class stores metadata of a translation set.
/// Can be used to troubleshoot translation origins.
/// </summary>
public class SetInfo
{
    /// <summary>
    /// The key represents the unique identifier of a translation set
    /// </summary>
    public string Key { get; private set; }

    /// <summary>
    /// The name that has been given from configuring the translation set source
    /// </summary>
    public string Name { get; private set; }

    public SetInfo(string key, string name)
    {
        ArgumentNullException.ThrowIfNull(key);
        ArgumentNullException.ThrowIfNull(name);
        Key = key;
        Name = name;
    }
}
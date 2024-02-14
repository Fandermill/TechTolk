namespace TechTolk.Division;

/// <summary>
/// A Divider is used for separating different translated values per translation key.
/// This can be a culture info or anything really. In the end, it's the
/// <see cref="Key"/> that is used for separating.
/// </summary>
public interface IDivider
{
    /// <summary>
    /// The key that identifies the divider
    /// </summary>
    string Key { get; }
}

namespace TechTolk.Division;

/// <summary>
/// Interface that provides the current <see cref="IDivider"/> to be used when translating
/// keys without explicit divider
/// </summary>
public interface ICurrentDividerProvider
{
    /// <summary>
    /// Gets the current divider
    /// </summary>
    IDivider GetCurrent();
}

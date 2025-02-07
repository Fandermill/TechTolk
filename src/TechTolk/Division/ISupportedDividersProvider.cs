namespace TechTolk.Division;

/// <summary>
/// Interfaces for getting dividers that are configured with TechTolk
/// </summary>
public interface ISupportedDividersProvider
{
    /// <summary>
    /// Gets all supported dividers
    /// </summary>
    /// <returns>Collection of supported dividers</returns>
    IEnumerable<IDivider> GetSupportedDividers();

    /// <summary>
    /// Checks if the given divider is configured with TechTolk
    /// </summary>
    /// <param name="divider">The divider to check for</param>
    /// <returns>True is divider is supported</returns>
    bool IsSupportedDivider(IDivider divider);

    /// <summary>
    /// Gets the supported divider by it's key
    /// </summary>
    /// <param name="key">The key identifying the divider</param>
    IDivider GetByKey(string key);
}
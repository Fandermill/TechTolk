namespace TechTolk.Division;

/// <summary>
/// Interface that provides the current <see cref="IDivider"/> to pick the right translation value
/// </summary>
public interface ICurrentDividerProvider
{
    IDivider GetCurrent();
}

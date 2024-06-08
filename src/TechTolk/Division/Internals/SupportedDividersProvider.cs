using TechTolk.Exceptions;

namespace TechTolk.Division.Internals;

internal class SupportedDividersProvider : ISupportedDividersProvider
{
    private List<IDivider> _dividers = new();

    public IEnumerable<IDivider> GetSupportedDividers()
    {
        if (_dividers.Count == 0)
            throw new RegistrationException(
                "TechTolk did not find any configured dividers. " +
                "Did you forget to add supported dividers? You " +
                "can do this with the TechTolkdBuilder.ConfigureDividers() method.");

        return _dividers.AsReadOnly();
    }

    public bool IsSupportedDivider(IDivider divider)
    {
        return _dividers.Any(d => d.Key == divider.Key);
    }

    public IDivider GetByKey(string key)
    {
        return _dividers.FirstOrDefault(d => d.Key == key)
            ?? throw new UnsupportedDividerException($"No divider found with key '{key}'");
    }

    internal void AddSupportedDivider(IDivider divider)
    {
        if (IsSupportedDivider(divider))
            throw new RegistrationException(
                $"Divider with key '{divider.Key}' is already in the supported divider collection");

        _dividers.Add(divider);
    }
}

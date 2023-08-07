namespace TechTolk.Division;

public interface ISupportedDividersProvider
{
    IEnumerable<IDivider> GetSupportedDividers();
    bool IsSupportedDivider(IDivider divider);
    IDivider GetByKey(string key);
}

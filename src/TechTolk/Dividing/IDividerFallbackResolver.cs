namespace TechTolk.Dividing;

public interface IDividerFallbackResolver
{
    IDivider ResolveToFallback(IDivider divider);
}

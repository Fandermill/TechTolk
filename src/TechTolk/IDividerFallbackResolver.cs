namespace TechTolk;

public interface IDividerFallbackResolver
{
    IDivider ResolveToFallback(IDivider divider);
}

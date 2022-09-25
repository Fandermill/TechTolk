using TechTolk.Dividing;

namespace TechTolk.Compiling;

public interface IDividerProviderTolkCompiler<T>
{
    IMergerTolkCompiler<T> WithDivider(ICurrentDividerProvider dividerProvider);
}

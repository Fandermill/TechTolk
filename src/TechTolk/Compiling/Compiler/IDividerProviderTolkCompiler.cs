using TechTolk.Dividing;

namespace TechTolk.Compiling.Compiler;

public interface IDividerProviderTolkCompiler<T>
{
    IMergerTolkCompiler<T> WithDivider(ICurrentDividerProvider dividerProvider);
}

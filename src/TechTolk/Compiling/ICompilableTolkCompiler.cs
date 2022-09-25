namespace TechTolk.Compiling;

public interface ICompilableTolkCompiler<T>
{
    ITolk<T> Compile();
}

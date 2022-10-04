namespace TechTolk.Compiling.Compiler;

public interface ICompilableTolkCompiler<T>
{
    ITolk<T> Compile();
}

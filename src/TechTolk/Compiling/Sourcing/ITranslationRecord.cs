using TechTolk.Dividing;

namespace TechTolk.Compiling.Sourcing;

public interface ITranslationRecord<T>
{
    IDivider Divider { get; }
    string Key { get; }
    T? Translation { get; }
}

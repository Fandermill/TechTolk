namespace TechTolk.Values;

public interface IDataValueMerger<T>
{
    T MergeDataIntoValue(T value, object? data);
}

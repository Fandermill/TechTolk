using TechTolk.Dividing;

namespace TechTolk;

public interface ITolk<T>
{
    T Translate(string key);
    T Translate(string key, object? data);
    T Translate(IDivider divider, string key);
    T Translate(IDivider divider, string key, object? data);
}

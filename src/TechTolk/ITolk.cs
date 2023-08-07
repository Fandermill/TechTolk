using TechTolk.Division;

namespace TechTolk;

public interface ITolk
{
    string Translate(string key);
    string Translate(string key, object? data);
    string Translate(IDivider divider, string key);
    string Translate(IDivider divider, string key, object? data);
}

public interface ITolk<T> : ITolk { }
namespace TechTolk.Values;

public interface IValueBag
{
    void Set(string key, object? value);
    object? Get(string key);

    object? this[string key] { get; set; }
}

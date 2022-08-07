namespace TechTolk;

public interface ITolk
{
    string Translate(string key);
    string Translate(IDivider divider, string key);
    string Translate(IDivider divider, string key, object? data);
}

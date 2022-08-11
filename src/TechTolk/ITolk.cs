namespace TechTolk;

public interface ITolk
{
    // Wellicht kunnen deze <string> signatures naar extensie methods?
    string Translate(string key);
    string Translate(IDivider divider, string key);
    string Translate(IDivider divider, string key, object? data);

    T Translate<T>(string key);
    T Translate<T>(IDivider divider, string key);
    T Translate<T>(IDivider divider, string key, object? data);
}

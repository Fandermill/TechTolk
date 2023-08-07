namespace TechTolk;

public interface ITolkFactory
{
    public ITolk Create(string translationSetKey);
	public ITolk Create<T>();
	public ITolk Create(Type type);
}
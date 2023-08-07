namespace TechTolk.Rendering.Internals;

internal class ValueBagConverter
{
	public ValueBag ConvertFromObject(Dictionary<string, object?> obj)
	{
		return new ValueBag(obj);
	}

	public ValueBag ConvertFromObject(object obj)
    {
		if (obj is Dictionary<string, object?> dict)
			return ConvertFromObject(dict);

        var valueBag = new ValueBag();

        obj
            .GetType()
            .GetProperties()
            .ToList()
            .ForEach(p => valueBag.Set(p.Name, p.GetValue(obj, null)));

        return valueBag;
    }
}
using System.Linq;

namespace TechTolk.Values;

public class ValueBagConverter : IValueBagConverter
{
    public IValueBag ConvertFromObject(object obj)
    {
        var valueBag = new ValueBag();

        obj
            .GetType()
            .GetProperties()
            .ToList()
            .ForEach(p => valueBag.Set(p.Name, p.GetValue(obj, null)));

        return valueBag;
    }
}
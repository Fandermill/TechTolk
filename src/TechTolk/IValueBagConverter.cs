using System.Linq;

namespace TechTolk;

public interface IValueBagConverter
{
    IValueBag ConvertFromObject(object obj);
}

public class ValueBagConverter : IValueBagConverter
{
    public IValueBag ConvertFromObject(object obj)
    {
        var valueBag = new ValueBag();

        obj
            .GetType()
            .GetProperties()
            .ToList()
            .ForEach(p => valueBag.Add(p.Name, p.GetValue(obj, null)));

        return valueBag;
    }
}
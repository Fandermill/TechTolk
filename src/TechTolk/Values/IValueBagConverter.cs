namespace TechTolk.Values;

public interface IValueBagConverter
{
    IValueBag ConvertFromObject(object obj);
}

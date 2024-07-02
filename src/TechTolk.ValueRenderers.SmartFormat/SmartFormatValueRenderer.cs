using SmartFormat;
using System.Globalization;
using TechTolk.Division;
using TechTolk.Rendering;
using TechTolk.TranslationSets.Values;

namespace TechTolk.ValueRenderers.SmartFormat;

public sealed class SmartFormatValueRenderer : AbstractTranslationValueRenderer
{
    private readonly SmartFormatter _formatter;

    public SmartFormatValueRenderer()
    {
        _formatter = Smart.Default;
    }

    public SmartFormatValueRenderer(SmartFormatter formatter)
    {
        _formatter = formatter;
    }

    public override string Render(IDivider divider, TranslationValue value, object? data)
    {
        if (divider is CultureInfoDivider cid)
        {
            return _formatter.Format(new CultureInfo(cid.Key), value.Value, data);
        }
        else
        {
            return _formatter.Format(value.Value, data);
        }
    }
}

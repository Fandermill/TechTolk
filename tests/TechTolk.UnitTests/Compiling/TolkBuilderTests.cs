using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTolk.Compiling;

namespace TechTolk.UnitTests.Compiling;

public class TolkBuilderTests
{
    [Fact]
    public void Can_add_single_translation_set()
    {
        var builder = new TolkBuilder<string>();
        
        var tolk = builder
            .AddTranslationSet(() => new TranslationSet<string>("Set1")).OverwriteDuplicates()
            //.AddTranslationSet
            .Compile();

        // TODO
        throw new NotImplementedException();

    }
}

using FluentAssertions;
using TechTolk.Exceptions;
using TechTolk.Registration.Builders;
using TechTolk.Sources.Resx.Tests.Resources;
using TechTolk.TestSuite.Helpers;
using TechTolk.TestSuite.Helpers.Dividers;
using Xunit;

namespace TechTolk.Sources.Resx.Tests;

public sealed class TechTolkSourcesResxTests
{
    public class WhenUsingCultureInfosAsDivider : AbstractTechTolkTests
    {
        [Fact]
        public void Can_register_a_resource_by_generic_type_as_source_for_a_root_translation_set()
        {
            var builder = _services.AddTechTolk()
                .ConfigureDefaultCultureInfoDividers();

            builder.AddTranslationSet("Set1", set =>
            {
                set.FromResource<MyResource>();
            });

            var tolk = GetTolkForTranslationSet("Set1");

            var result = tolk.Translate(Constants.CultureInfoDividers.nl_NL, "MyKey");

            result.Should().Be("MyValue-NL");
        }

        [Fact]
        public void Can_register_a_resource_by_base_name_and_assembly_for_a_root_translation_set()
        {
            var builder = _services.AddTechTolk()
                .ConfigureDefaultCultureInfoDividers();

            builder.AddTranslationSet("Set1", set =>
            {
                set.FromResource("TechTolk.Sources.Resx.Tests.Resources.MyResource", typeof(MyResource).Assembly);
            });

            var tolk = GetTolkForTranslationSet("Set1");

            var result = tolk.Translate(Constants.CultureInfoDividers.nl_NL, "MyKey");

            result.Should().Be("MyValue-NL");
        }

        [Fact]
        public void Can_register_a_resource_by_generic_type_as_source_for_a_merged_translation_set()
        {
            var builder = _services.AddTechTolk()
                .ConfigureDefaultCultureInfoDividers();

            builder.AddMergedTranslationSet("MergedSet1", mergedSet =>
            {
                mergedSet.FromResource<MyResource>();
                mergedSet.FromResource<MySecondResource>();

                mergedSet.WithOptions(o => o.OnDuplicateKey().Replace());
            });

            var tolk = GetTolkForTranslationSet("MergedSet1");

            var result = tolk.Translate(Constants.CultureInfoDividers.en_US, "MyKey");

            result.Should().Be("MySecondValue-EN");
        }

        [Fact]
        public void Can_register_a_resource_by_base_name_and_assembly_for_a_merged_translation_set()
        {
            var builder = _services.AddTechTolk()
                .ConfigureDefaultCultureInfoDividers();

            builder.AddMergedTranslationSet("MergedSet1", set =>
            {
                set.FromResource("TechTolk.Sources.Resx.Tests.Resources.MyResource", typeof(MyResource).Assembly);
                set.FromResource("TechTolk.Sources.Resx.Tests.Resources.MySecondResource", typeof(MySecondResource).Assembly);
                set.WithOptions(o => o.OnDuplicateKey().Replace());
            });

            var tolk = GetTolkForTranslationSet("MergedSet1");

            var result = tolk.Translate(Constants.CultureInfoDividers.en_US, "MyKey");

            result.Should().Be("MySecondValue-EN");
        }
    }

    public class WhenUsingOtherThanCultureInfosAsDivider : AbstractTechTolkTests
    {
        [Fact]
        public void Can_register_a_resource_by_base_name_and_assembly_for_a_root_translation_set()
        {
            var builder = _services.AddTechTolk()
                .ConfigureDividers(dividers =>
                    dividers
                        .AddSupportedDivider(Constants.StringDividers.Div1)
                        .AddSupportedDivider(Constants.StringDividers.Div2)
                );

            var assembly = GetType().Assembly;

            builder.AddTranslationSet("Set1", set =>
            {
                set.FromResource("TechTolk.Sources.Resx.Tests.Resources.AnotherResource", assembly);
            });

            var tolk = GetTolkForTranslationSet("Set1");

            var result = tolk.Translate(Constants.StringDividers.Div1, "MyKey");

            result.Should().Be("MyValue-Div1");
        }

        [Fact]
        public void Registering_a_resource_by_generic_type_for_a_root_translation_set_will_throw_an_exception()
        {
            var builder = _services.AddTechTolk()
                .ConfigureDividers(dividers =>
                    dividers
                        .AddSupportedDivider(Constants.StringDividers.Div1)
                        .AddSupportedDivider(Constants.StringDividers.Div2)
                );

            builder.AddTranslationSet("Set1", set =>
            {
                set.FromResource<MyResource>();
            });

            var act = () => GetTolkForTranslationSet("Set1");

            act.Should().Throw<RegistrationException>();
        }

        [Fact]
        public void Can_register_a_resource_by_base_name_and_assembly_for_a_merged_translation_set()
        {
            var builder = _services.AddTechTolk()
                .ConfigureDividers(dividers =>
                    dividers
                        .AddSupportedDivider(Constants.StringDividers.Div1)
                        .AddSupportedDivider(Constants.StringDividers.Div2)
                );

            var assembly = GetType().Assembly;

            builder.AddMergedTranslationSet("MergedSet", set =>
            {
                set.FromResource("TechTolk.Sources.Resx.Tests.Resources.AnotherResource", assembly);
                set.FromResource("TechTolk.Sources.Resx.Tests.Resources.YetAnotherResource", assembly);
                set.WithOptions(options => options.OnDuplicateKey().Replace());
            });

            var tolk = GetTolkForTranslationSet("MergedSet");

            var result = tolk.Translate(Constants.StringDividers.Div1, "MyKey");

            result.Should().Be("MyValue-YetAnotherDiv1");
        }

        [Fact]
        public void Registering_a_resource_by_generic_type_for_a_merged_translation_set_will_throw_an_exception()
        {
            var builder = _services.AddTechTolk()
                .ConfigureDividers(dividers =>
                    dividers
                        .AddSupportedDivider(Constants.StringDividers.Div1)
                        .AddSupportedDivider(Constants.StringDividers.Div2)
                );

            builder.AddMergedTranslationSet("MergedSet1", set =>
            {
                set.FromResource<MyResource>();
            });

            var act = () => GetTolkForTranslationSet("MergedSet1");

            act.Should().Throw<RegistrationException>();
        }
    }

    public class Misc : AbstractTechTolkTests
    {
        [Fact]
        public void Can_register_a_resource_through_the_add_translation_set_extension_method()
        {
            _services.AddTechTolk()
                .ConfigureDefaultCultureInfoDividers()
                .AddTranslationSetFromResource<MyResource>(null);

            var tolk = GetTolkForTranslationSet<MyResource>();

            var result = tolk.Translate(Constants.CultureInfoDividers.nl_NL, "MyKey");

            result.Should().Be("MyValue-NL");
        }
    }
}

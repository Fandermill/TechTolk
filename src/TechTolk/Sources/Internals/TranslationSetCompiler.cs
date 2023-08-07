using TechTolk.Exceptions;
using TechTolk.Registration;
using TechTolk.TranslationSets;
using TechTolk.TranslationSets.Building;
using TechTolk.TranslationSets.Internals;
using TechTolk.TranslationSets.Merging;

namespace TechTolk.Sources.Internals;

internal class TranslationSetCompiler : ITranslationSetCompiler
{
    private ITranslationSetBuilderFactory _builderFactory;
    private ITranslationSetSourceFactory _sourceFactory;
    private ITranslationSetMerger _merger;

    public TranslationSetCompiler(
        ITranslationSetBuilderFactory builderFactory,
        ITranslationSetSourceFactory sourceFactory,
        ITranslationSetMerger merger)
    {
        _builderFactory = builderFactory;
        _sourceFactory = sourceFactory;
        _merger = merger;
    }

    public ITranslationSet CompileTranslationSet(TranslationSetRegistration registration)
    {
        var sets = CreateTranslationSets(registration);
        if (sets.Count == 1)
        {
            return sets.First();
        }
        else
        {
            return _merger.Merge(registration.MergeOptions ?? new TranslationSetMergeOptions(), sets.ToArray());
        }
    }

    private List<IInternalTranslationSet> CreateTranslationSets(TranslationSetRegistration registration)
    {
        List<IInternalTranslationSet> sets = new();
        foreach (var sourceRegistration in registration.Sources)
        {
            var set = CreateSetFromSource(registration, sourceRegistration);
            sets.Add(set);
        }
        return sets;
    }

    private IInternalTranslationSet CreateSetFromSource(TranslationSetRegistration registration, SourceRegistrationBase sourceRegistration)
    {
        var builder = _builderFactory.CreateBuilder(new SetInfo(registration.Key, registration.Name));
        var source = GetSource(sourceRegistration);
        source.PopulateTranslations(builder, sourceRegistration);

        var set = builder.Build();
        return set as IInternalTranslationSet
            ?? throw new CompilationException(
                $"Unexpected translation set type from builder. " +
                $"Expected '{nameof(IInternalTranslationSet)}' " +
                $"but got '{set.GetType().Name}'");
    }

    private ITranslationSetSource GetSource(SourceRegistrationBase sourceRegistration)
    {
        if (sourceRegistration is ResolveSourceRegistration resolveSourceRegistration)
            return _sourceFactory.Create(resolveSourceRegistration);
        else if (sourceRegistration is SourceInstanceRegistration sourceInstanceRegistration)
            return sourceInstanceRegistration.Instance;
        throw new CompilationException($"Unknown source registration type '{sourceRegistration.GetType().Name}'");
    }

}

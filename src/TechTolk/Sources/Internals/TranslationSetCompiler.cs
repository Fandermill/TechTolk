using TechTolk.Exceptions;
using TechTolk.Registration;
using TechTolk.TranslationSets;
using TechTolk.TranslationSets.Building.Internals;
using TechTolk.TranslationSets.Internals;
using TechTolk.TranslationSets.Merging;

namespace TechTolk.Sources.Internals;

internal sealed class TranslationSetCompiler : ITranslationSetCompiler
{
    private readonly ITranslationSetBuilderFactory _builderFactory;
    private readonly ITranslationSetSourceFactory _sourceFactory;
    private readonly ITranslationSetMerger _merger;

    public TranslationSetCompiler(
        ITranslationSetBuilderFactory builderFactory,
        ITranslationSetSourceFactory sourceFactory,
        ITranslationSetMerger merger)
    {
        _builderFactory = builderFactory;
        _sourceFactory = sourceFactory;
        _merger = merger;
    }

    public async Task<ITranslationSet> CompileTranslationSetAsync(TranslationSetRegistration registration)
    {
        var sets = await CreateTranslationSetsAsync(registration);
        return ToSingleTranslationSet(registration, sets);
    }

    public ITranslationSet CompileTranslationSetSynchronously(TranslationSetRegistration registration)
    {
        List<IInternalTranslationSet> sets;

        try
        {
            sets = Task.Run(async () => await CreateTranslationSetsAsync(registration)).Result;
        }
        catch (AggregateException ae)
        {
            if (ae.InnerExceptions.Count == 1)
                throw ae.InnerExceptions.First();

            throw new CompilationException(
                $"Multiple exceptions thrown while compiling translation set " +
                $"synchronously for '{registration.Name}'", ae);
        }

        return ToSingleTranslationSet(registration, sets);
    }

    private ITranslationSet ToSingleTranslationSet(TranslationSetRegistration registration, List<IInternalTranslationSet> translationSets)
    {
        if (translationSets.Count == 1)
        {
            return translationSets.First();
        }
        else
        {
            return _merger.Merge(registration.MergeOptions ?? new TranslationSetMergeOptions(), translationSets.ToArray());
        }
    }

    private async Task<List<IInternalTranslationSet>> CreateTranslationSetsAsync(TranslationSetRegistration registration)
    {
        List<IInternalTranslationSet> sets = new();
        foreach (var sourceRegistration in registration.Sources)
        {
            var set = await CreateSetFromSource(registration, sourceRegistration);
            sets.Add(set);
        }
        return sets;
    }

    private async Task<IInternalTranslationSet> CreateSetFromSource(TranslationSetRegistration registration, SourceRegistrationBase sourceRegistration)
    {
        var builder = _builderFactory.CreateBuilder(new SetInfo(registration.Key, registration.Name));
        var source = GetSource(sourceRegistration);

        await source.PopulateTranslationsAsync(builder, sourceRegistration).ConfigureAwait(false);

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

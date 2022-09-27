using System;
using System.Collections.Generic;
using TechTolk.Dividing;

namespace TechTolk.Compiling;

public class TolkCompiler
{
    public static IDividerProviderTolkCompiler<T> ForType<T>()
    {
        return new DividerProviderTolkCompiler<T>(new TolkCompilation<T>());
    }

    private class TolkCompilation<T> : ITolkCompilation<T>
    {
        private ICurrentDividerProvider? _currentDividerProvider;
        private ITranslationSetMerger<T>? _translationSetMerger;

        private readonly List<TranslationSetRegistration<T>> _setRegistrations;


        public TolkCompilation()
        {
            _setRegistrations = new List<TranslationSetRegistration<T>>();
        }

        public void WithDivider(ICurrentDividerProvider dividerProvider)
        {
            _currentDividerProvider = dividerProvider;
        }

        public void WithMerger(ITranslationSetMerger<T> merger)
        {
            _translationSetMerger = merger;
        }

        public ITranslationSetRegistration<T> AddTranslationSet(Func<ITranslationSet<T>> getTranslationSet)
        {
            var registration = new TranslationSetRegistration<T>(this, new DelegateTranslationSetProvider<T>(getTranslationSet));
            _setRegistrations.Add(registration);
            return registration;
        }

        public ITranslationSetRegistration<T> AddTranslationSet(ITranslationSetProvider<T> translationSetProvider)
        {
            var registration = new TranslationSetRegistration<T>(this, translationSetProvider);
            _setRegistrations.Add(registration);
            return registration;
        }

        public ITolk<T> Compile()
        {
            if (_currentDividerProvider is null)
                throw new InvalidOperationException($"Unable to compile without a divider provider");
            if (_translationSetMerger is null)
                throw new InvalidOperationException($"Unable to compile without a merger");

            var mergedTranslationSet = _translationSetMerger.Merge(_setRegistrations);
            return new Tolk<T>(_currentDividerProvider, mergedTranslationSet);
        }
    }

    private class DividerProviderTolkCompiler<T> : IDividerProviderTolkCompiler<T>
    {
        private ITolkCompilation<T> _compilation;

        public DividerProviderTolkCompiler(ITolkCompilation<T> compilation)
        {
            _compilation = compilation;
        }

        public IMergerTolkCompiler<T> WithDivider(ICurrentDividerProvider dividerProvider)
        {
            _compilation.WithDivider(dividerProvider);
            return new MergerTolkCompiler<T>(_compilation);
        }
    }

    private class MergerTolkCompiler<T> : IMergerTolkCompiler<T>
    {
        private ITolkCompilation<T> _compilation;

        public MergerTolkCompiler(ITolkCompilation<T> compilation)
        {
            _compilation = compilation;
        }

        public ITranslationSetTolkCompiler<T> WithMerger(ITranslationSetMerger<T> merger)
        {
            _compilation.WithMerger(merger);
            return new TranslationSetTolkCompiler<T>(_compilation);
        }
    }

    private class TranslationSetTolkCompiler<T> : ITranslationSetTolkCompiler<T>
    {
        private ITolkCompilation<T> _compilation;

        public TranslationSetTolkCompiler(ITolkCompilation<T> compilation)
        {
            _compilation = compilation;
        }

        public ITranslationSetRegistration<T> AddTranslationSet(Func<ITranslationSet<T>> getTranslationSet)
        {
            return _compilation.AddTranslationSet(getTranslationSet);
        }

        public ITranslationSetRegistration<T> AddTranslationSet(ITranslationSetProvider<T> translationSetProvider)
        {
            return _compilation.AddTranslationSet(translationSetProvider);
        }
    }
}

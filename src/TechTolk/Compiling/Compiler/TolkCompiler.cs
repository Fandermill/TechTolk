using System;
using System.Collections.Generic;
using TechTolk.Compiling.Converting;
using TechTolk.Compiling.Merging;
using TechTolk.Compiling.Sourcing;
using TechTolk.Dividing;

namespace TechTolk.Compiling.Compiler;

public class TolkCompilation
{
    public static IDividerProviderTolkCompiler<T> ForType<T>()
    {
        return new DividerProviderTolkCompiler<T>(new TolkCompiler<T>());
    }

    private class TolkCompiler<T> : ITolkCompiler<T>
    {
        private ICurrentDividerProvider? _currentDividerProvider;
        private ITranslationRecordSetMerger<T>? _merger;
        private ITranslationSetConverter<T>? _translationSetConverter;

        private readonly List<TranslationSetRegistration<T>> _setRegistrations;


        public TolkCompiler()
        {
            _setRegistrations = new List<TranslationSetRegistration<T>>();
        }

        public void WithDivider(ICurrentDividerProvider dividerProvider)
        {
            _currentDividerProvider = dividerProvider;
        }

        public void WithMerger(ITranslationRecordSetMerger<T> merger)
        {
            _merger = merger;
        }

        public void WithTranslationSetConverter(ITranslationSetConverter<T> translationSetConverter)
        {
            _translationSetConverter = translationSetConverter;
        }

        public ITranslationSetRegistration<T> AddTranslationSet(Func<ITranslationRecordSet<T>> getTranslationSet)
        {
            var registration = new TranslationSetRegistration<T>(this, new DelegateTranslationRecordSetProvider<T>(getTranslationSet));
            _setRegistrations.Add(registration);
            return registration;
        }

        public ITranslationSetRegistration<T> AddTranslationSet(ITranslationRecordSetProvider<T> translationSetProvider)
        {
            var registration = new TranslationSetRegistration<T>(this, translationSetProvider);
            _setRegistrations.Add(registration);
            return registration;
        }

        public ITolk<T> Compile()
        {
            if (_currentDividerProvider is null)
                throw new InvalidOperationException($"Unable to compile without a divider provider");
            if (_merger is null)
                throw new InvalidOperationException($"Unable to compile without a merger");
            if (_translationSetConverter is null)
                throw new InvalidOperationException($"Unable to compile without a converter");

            var mergedTranslationSet = _merger.Merge(_setRegistrations);
            var resultSet = _translationSetConverter.FromRecordSet(mergedTranslationSet);
            return new Tolk<T>(_currentDividerProvider, resultSet);
        }
    }

    private class DividerProviderTolkCompiler<T> : IDividerProviderTolkCompiler<T>
    {
        private ITolkCompiler<T> _compiler;

        public DividerProviderTolkCompiler(ITolkCompiler<T> compiler)
        {
            _compiler = compiler;
        }

        public IMergerTolkCompiler<T> WithDivider(ICurrentDividerProvider dividerProvider)
        {
            _compiler.WithDivider(dividerProvider);
            return new MergerTolkCompiler<T>(_compiler);
        }
    }

    private class MergerTolkCompiler<T> : IMergerTolkCompiler<T>
    {
        private ITolkCompiler<T> _compiler;

        public MergerTolkCompiler(ITolkCompiler<T> compiler)
        {
            _compiler = compiler;
        }

        public ITranslationSetConverterTolkCompiler<T> WithMerger(ITranslationRecordSetMerger<T> merger)
        {
            _compiler.WithMerger(merger);
            return new TranslationSetConverterTolkCompiler<T>(_compiler);
        }
    }

    private class TranslationSetConverterTolkCompiler<T> : ITranslationSetConverterTolkCompiler<T>
    {
        private ITolkCompiler<T> _compiler;

        public TranslationSetConverterTolkCompiler(ITolkCompiler<T> compiler)
        {
            _compiler = compiler;
        }

        public ITranslationSetTolkCompiler<T> WithTranslationSetConverter(ITranslationSetConverter<T> translationSetConverter)
        {
            _compiler.WithTranslationSetConverter(translationSetConverter);
            return new TranslationSetTolkCompiler<T>(_compiler);
        }
    }

    private class TranslationSetTolkCompiler<T> : ITranslationSetTolkCompiler<T>
    {
        private ITolkCompiler<T> _compiler;

        public TranslationSetTolkCompiler(ITolkCompiler<T> compiler)
        {
            _compiler = compiler;
        }

        public ITranslationSetRegistration<T> AddTranslationSet(Func<ITranslationRecordSet<T>> getTranslationSet)
        {
            return _compiler.AddTranslationSet(getTranslationSet);
        }

        public ITranslationSetRegistration<T> AddTranslationSet(ITranslationRecordSetProvider<T> translationSetProvider)
        {
            return _compiler.AddTranslationSet(translationSetProvider);
        }
    }
}

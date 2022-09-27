using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechTolk.Compiling;

public class TranslationSetMerger<T> : ITranslationSetMerger<T>
{
    public ITranslationSet<T> Merge(IEnumerable<ITranslationSetRegistration<T>> setRegistrations)
    {
        var resultSet = new TranslationSet<T>("compiled-set");
        foreach(var setRegistration in setRegistrations)
        {
            MergeIntoResult(resultSet, setRegistration);
        }
        return resultSet;
    }

    private void MergeIntoResult(
        TranslationSet<T> resultSet, 
        ITranslationSetRegistration<T> setRegistration)
    {
        var set = setRegistration.GetTranslationSet();
        foreach(var divisionDictionary in set.GetDivisionDictionaries())
        {
            if(resultSet.)
        }
    }
}

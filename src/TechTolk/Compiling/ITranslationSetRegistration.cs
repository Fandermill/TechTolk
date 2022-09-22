using System;

namespace TechTolk.Compiling;

public interface ITranslationSetRegistration<T> : ITolkBuilder<T>
{
    ITranslationSetRegistration<T> OverwriteDuplicates();
}

namespace TechTolk;

internal static class ExceptionExtensions
{
    public static void ThrowWithPreservedStackTrace(this Exception exception)
    {
        ThreadStart? savestack = Delegate.CreateDelegate(
                typeof(ThreadStart), exception, "InternalPreserveStackTrace", false, false)
                as ThreadStart;

        if (savestack is not null)
            savestack();

        throw exception;
    }
}
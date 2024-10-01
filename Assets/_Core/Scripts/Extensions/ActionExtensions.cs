using System;

namespace Odumbrata.Extensions
{
    public static class ActionExtensions
    {
        public static void SafeInvoke(this Action action)
        {
            action?.Invoke();
        }

        public static void SafeInvoke<T>(this Action<T> action, T arg)
        {
            action?.Invoke(arg);
        }

        public static void SafeInvoke<T, V>(this Action<T, V> action, T firstArg, V secondArg)
        {
            action?.Invoke(firstArg, secondArg);
        }
    }
}
using System;
using System.Collections.Generic;

namespace Godot.IcsEditor.Ui
{
    public static class ExtensionMethods
    {
        public static void Each<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            foreach (var t in enumerable)
                action(t);
        }
    }
}
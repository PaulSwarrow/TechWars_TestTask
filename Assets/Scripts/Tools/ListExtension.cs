using System;
using System.Collections.Generic;

namespace Tools
{
    public static class ListExtension
    {
        public static T Shift<T>(this List<T> list)
        {
            if (list.Count > 0)
            {
                var item = list[0];
                list.RemoveAt(0);
                return item;
            }

            return default;
        }

        public static bool TryFind<T>(this List<T> list, Predicate<T> predicate, out T result)
        {
            for (var i = 0; i < list.Count; i++)
            {
                var item = list[i];
                if (predicate(item))
                {
                    result = item;
                    return true;
                }
            }

            result = default;
            return false;
        }
    }
}
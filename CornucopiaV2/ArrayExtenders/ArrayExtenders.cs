using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CornucopiaV2
{
    public static class ArrayConvertersV2
    {
        public static T[] Convert<T>
           (params T[] args
           )
        {
            return args;
        }
        public static T[] Add<T>
           (this T[] array
           , T element
           )
        {
            return array.ToList().AddAndReturn(element).ToArray();
        }
    }
}

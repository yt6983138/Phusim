using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace System.Collections.Generic
{
    public static class ListExtension
    {
        public static string ToString<T>(this List<T> list)
        {
            return $"{{ {string.Join(", ", list)} }}";
        }
    }
}
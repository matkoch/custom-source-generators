using System.Collections.Generic;

namespace CustomGenerators.Utilities
{
    public static class StringExtensions
    {
        public static string TrimEnd(this string str, string trim)
        {
            return str.EndsWith(trim) ? str.Substring(startIndex: 0, str.Length - trim.Length) : str;
        }

        public static string TrimStart(this string str, string trim)
        {
            return str.StartsWith(trim) ? str.Substring(trim.Length) : str;
        }

        public static string Join(this IEnumerable<string> enumerable, string separator)
        {
            return string.Join(separator, enumerable);
        }
    }
}

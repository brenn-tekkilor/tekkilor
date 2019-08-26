using System;

namespace utility.language
{
    public static class StringExtensions
    {
        public static string ToCamelCase(this string v)
        {
            if (!string.IsNullOrEmpty(v) && v.Length > 1)
                return Char.ToLowerInvariant(v[0]) + v.Substring(1);
            return v;
        }
        public static string UpperFirst(this string v)
        {
            if (!string.IsNullOrEmpty(v))
            {
                char[] a = v.ToCharArray();
                a[0] = char.ToUpper(a[0]);
                return new string(a);
            }
            return v;
        }
        public static string ToPascelCase(this string v)
        {
            return v.ToCamelCase().UpperFirst();
        }
    }
}

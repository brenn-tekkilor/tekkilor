#nullable enable
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace Utility.Extensions
{
    public static class StringExtensions
    {
        private static readonly TextInfo TextInfo
            = (Thread.CurrentThread.CurrentCulture).TextInfo;
        public static string FormatDouble(
            this string? s)
        {
            return
                !(string.IsNullOrEmpty(s))
                    ? !(string.IsNullOrWhiteSpace(s))
                        ? s.ToDouble().ToString(
                            "F2", CultureInfo.CurrentCulture)
                    : "0"
                : "0";
        }
        public static string FromHex(
            this string? s)
        {
            if (s != null)
            {
                var bytes = new byte[s.Length / 2];
                for (var i = 0; i < bytes.Length; i++)
                {
                    bytes[i] = Convert.ToByte(s.Substring(i * 2, 2), 16);
                }
                return Encoding.Unicode.GetString(bytes);
            }
            else throw new ArgumentNullException(nameof(s));
        }
        public static Match? GetMatch(
            this string? s
            , string? pattern)
        {
            return
                !string.IsNullOrEmpty(
                    s)
                    ? pattern?.Rx()
                    ?.Match(s)
                    ?? null
                : null;
        }
        public static MatchCollection? GetMatchCollection(
            this string? s
            , string? pattern)
        {
            return
                !string.IsNullOrEmpty(s)
                    ? pattern?.Rx()
                    ?.Matches(s)
                    ?? null
                : null;
        }
        public static string GetMatchValue(
            this string? s
            , string? pattern
            , string? key)
        {
            MatchCollection? col =
                s?.GetMatchCollection(
                    pattern) ?? null;
            return
                !string.IsNullOrEmpty(
                    key)
                    ? col?.Any()
                    ?? false
                        ? col?[0]?.Success
                        ?? false
                            ? col?[0]?.Groups.ContainsKey(
                                key)
                            ?? false
                                ? col?[0]?.Groups[key].Value
                                    ?? string.Empty
                                : string.Empty
                            : string.Empty
                        : string.Empty
                    : string.Empty;
        }
        public static IDictionary<string, string>? GetMatchValues(
            this string? s
            , string? pattern
            , IEnumerable<string>? keys)
        {
            MatchCollection? col =
                s?.GetMatchCollection(
                    pattern) ?? null;
            return
                keys?.Any()
                ?? false
                    ? col?.Any()
                    ?? false
                        ? col?[0]?.Success
                        ?? false
                            ? col?[0]?.Groups.Count > 0
                                ? (from
                                        string key
                                    in
                                        keys
                                    select
                                        key)
                                .Where(
                                    k => col?[0]
                                        ?.Groups
                                        .ContainsKey(k)
                                    ?? false)
                                .ToDictionary(
                                    k => k
                                    , k => col?[0]
                                        ?.Groups[k]
                                        .Value
                                    ?? string.Empty)
                            : null
                        : null
                    : null
                : null;
        }
        public static bool IsMatch(
            this string? s
            , string? pattern)
        {
            return
                s?.GetMatch(
                    pattern)
                ?.Success
            ?? false;
        }
        public static string ObjectId(
            this string? s)
        {
            return string.IsNullOrEmpty(s)
                ? throw new ArgumentException(null, nameof(s))
                : s.Length < 12
                        ? s.PadLeft(12, '0').ToHex()
                    : s.Length > 12
                        ? s.Substring(-1, 12).ToHex()
                    : s.Length == 12
                        ? s.ToHex()
                    : throw new ArgumentException(null, nameof(s));
        }
        public static string PascelCase(
            this string? s)
        {
            return
                s?.ToCamelCase()
                ?.ToUpper(
                    CultureInfo
                    .CurrentCulture)
                ?? string.Empty;
        }
        public static Regex? Rx(
            this string? s)
        {
            return
                !string.IsNullOrEmpty(
                    s)
                    ? new Regex(
                        s
                        , RegexOptions.IgnoreCase
                        | RegexOptions.IgnorePatternWhitespace)
                : null;
        }
        public static string ToCamelCase(
            this string? s)
            {
                return
                    s?.ToTitleCase()?
                    .Replace(
                        " "
                        , null
                        , StringComparison
                        .CurrentCultureIgnoreCase)
                    ?.Replace(
                        "_"
                        , null
                        , StringComparison
                        .CurrentCultureIgnoreCase)
                    ?.ToLower(
                            CultureInfo
                            .CurrentCulture)
                    ?? string.Empty;
            }
        public static DateTime ToDate(
            this string? s)
        {
            return DateTime.TryParse(s
                ?? DateTime.Today.ToLocalTime().ToShortDateString()
                , CultureInfo.CurrentCulture
                , DateTimeStyles.AssumeLocal, out DateTime result)
                ? result : DateTime.Today;
        }
        public static DateTime ToDateIfLater(
            this string? s
            , DateTime toCompare)
        {
            return s.ToDate().ToDateIfLater(toCompare);
        }
        public static DateTime ToDateIfLater(
            this string? s
            , string? toCompare)
        {
            return s.ToDateIfLater(toCompare.ToDate());
        }
        public static double ToDouble(
            this string? s)
        {
            return
                !string.IsNullOrEmpty(s)
                    ? double.TryParse(
                        s
                        .TrimStart('$')
                        .Trim()
                        , out double result)
                        ? result
                    : double.NaN
                : double.NaN;
        }
        public static string ToHex(
            this string? s)
        {
            StringBuilder sb =
                new StringBuilder();

            byte[] bytes =
                !string.IsNullOrEmpty(s)
                    ? Encoding
                    .Unicode
                    .GetBytes(s)
                : Array.Empty<byte>();
            foreach (byte t in bytes)
            {
                sb.Append(t.ToString("X2"
                    , CultureInfo.CurrentCulture));
            }
            return sb.ToString();
        }
        public static string ToTitleCase(
            this string? s)
        {
            return
                !string.IsNullOrEmpty(s)
                    ? TextInfo.ToTitleCase(s)
                : string.Empty;
        }
        public static DateTime ToWedIfLater(
            this string? s)
        {
            return s.ToDate().ToNextWedIfLater();
        }
        public static bool TryMatch(
           this string? s
            , string? pattern
            , out Match? result)
        {
            result =
                s?.GetMatch(
                    pattern)
                ?? null;
            return
                result != null
                    ? result.Success
                        ? true
                    : false
                : false;
        }
        public static bool TryMatch(
           this string? s
            , string? pattern
            , out MatchCollection? result)
        {
            result =
                s?.GetMatchCollection(
                    pattern);
            return
                result != null
                    ? result.Count > 0
                        ? result[0] != null
                            ? result[0].Success
                                ? true
                            : false
                        : false
                    : false
                : false;
        }
        public static bool TryMatch(
           this string? s
            , string? pattern
            , string? key
            , out string result)
        {
            result =
                s?.GetMatchValue(
                    pattern, key)
                ?? string.Empty;
            return
                !string.IsNullOrEmpty(
                    result)
                    ? true
                : false;                
        }
        public static bool TryMatch(
            this string? s
            , string? pattern
            , IEnumerable<string>? keys
            , out IDictionary<string, string>? result)
        {
            result =
                s?.GetMatchValues(
                    pattern
                    , keys);
            return
                result?.Any()
                ?? false
                ? true
            : false;
        }
    public static string ValueOrDefault(
            this string? s
            , string defaultValue)
        {
            return
                !string.IsNullOrEmpty(s)
                    ? !string.IsNullOrWhiteSpace(s)
                        ? s
                    : defaultValue
                : defaultValue;
        }
    }
}

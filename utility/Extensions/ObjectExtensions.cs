#nullable enable
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Utility.Extensions
{
    public static class ObjectExtensions
    {
        public static void ToConsole(
            this object? o) =>
            Console.WriteLine(
                o?.ToString() ?? string.Empty);
        public static void ToConsole(
            this IEnumerable<object?>? numerable) =>
            numerable?.ToList()
            ?.ForEach(
                o => Console.WriteLine(
                    o?.ToString()
                    ?? string.Empty));
        public static IEnumerable<string?>? ToStrings(
            this IEnumerable<object>? numerable) =>
            numerable?.Any() ?? false
                ? (
                from
                    object o
                in
                    numerable
                select
                    o)
                    .Select(
                        o => o.ToString())
                    .ToList()
                : null;
    }
}

using System;

namespace EPiFindEnvironments.Extensions
{
    public static class StringExtensions
    {
        public static string EnsureEndsWith(this string source, string value,
            StringComparison comparisonType = StringComparison.CurrentCulture)
        {
            return source.EndsWith(value, comparisonType) ? source : source + value;
        }

        public static string NullIfEmpty(this string @string)
        {
            return string.IsNullOrEmpty(@string) ? null : @string;
        }
    }
}
using System.Globalization;
using System.Text.RegularExpressions;
using System.Text;

namespace Embarcaciones.AplicacionWeb.Models.Utils
{
    public static class StringUtils
    {
        public static char[] CommonAllowedChars => new[] { '@', ':', '?', '-', '_', '{', '}', '(', ')', '.', '|' };
        public static string CleanString(this string value, params char[] allowedChars)
        {
            if (value == null) return string.Empty;

            value = value.Trim();
            if (string.IsNullOrWhiteSpace(value)) return string.Empty;

            allowedChars = allowedChars ?? new char[] { };

            return value
                .Normalize(NormalizationForm.FormD)
                .Select(
                    AllowedChars(allowedChars)
                )
                .CleanCharArrayToString()
                .DeepTrim();
        }

        private static Func<char, char> AllowedChars(params char[] allowedChars)
        {
            var commonCategory = new[]
            {
                UnicodeCategory.LowercaseLetter, UnicodeCategory.UppercaseLetter, UnicodeCategory.SpaceSeparator,
                UnicodeCategory.DecimalDigitNumber
            };

            return x =>
                commonCategory.Contains(CharUnicodeInfo.GetUnicodeCategory(x)) ||
                allowedChars.Contains(x)
                    ? x
                    : '_';
        }

        private static string CleanCharArrayToString(this IEnumerable<char> charArray)
        {
            return new string(charArray.ToArray()).Replace("_", string.Empty);
        }

        private static string DeepTrim(this string value)
        {
            var regex = new Regex("[ ]{2,}", RegexOptions.None);

            return regex.Replace(value, " ");
        }
    }
}

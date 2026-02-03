using System.Globalization;
using System.Text.RegularExpressions;
using System.Text;

namespace Embarcaciones.AplicacionWeb.Models.Utils
{
    public static class StringExtensions
    {
        public static char[] CommonAllowedChars => new[] { '@', ':', '?', '-', '_', '{', '}', '(', ')', '.', '|' };

        public static string CleanStringV2(this string value, params char[] allowedChars)
        {
            if (string.IsNullOrWhiteSpace(value)) return string.Empty;

            value = value.Trim();
            allowedChars = allowedChars ?? new char[] { };

            // Normalizar y eliminar acentos
            var normalized = value.Normalize(NormalizationForm.FormD);

            var cleaned = new string(normalized
                .Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark || allowedChars.Contains(c))
                .ToArray());

            // Reemplaza múltiples espacios por uno solo y pasa a mayúsculas
            cleaned = Regex.Replace(cleaned, @"\s+", " ").ToUpper();

            return cleaned;
        }
    }
}

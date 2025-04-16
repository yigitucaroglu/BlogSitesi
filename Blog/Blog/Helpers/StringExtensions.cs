using System.Text.RegularExpressions;

namespace BlogProject.Helpers
{
    public static class StringExtensions
    {
        public static string StripHtml(this string input)
        {
            return string.IsNullOrEmpty(input)
                ? string.Empty
                : Regex.Replace(input, "<.*?>", string.Empty);
        }

        public static string Truncate(this string input, int length)
        {
            if (string.IsNullOrEmpty(input)) return string.Empty;
            return input.Length <= length ? input : input.Substring(0, length) + "...";
        }
    }
}

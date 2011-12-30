using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace MayMart.Wiki
{
    public static class TextTypographer
    {
        private const string LEFT_RUS_QUOTATION = "\xAB";
        private const string RIGHT_RUS_QUOTATION = "\xBB";

        private const string LEFT_LAT_QUOTATION = "\x201C";
        private const string RIGHT_LAT_QUOTATION = "\x201D";

        private const string MULTIPLICATION = "\xD7";
        private const string MINUS = "\x2212";

        private const string ELLIPSIS = "\x2026";
        private const string EN_DASH = "\x2013";
        private const string EM_DASH = "\x2014";

        private const string COPYRIGHT = "\xA9";
        private const string REGISTERED = "\xAE";
        private const string TRADEMARK = "\x2122";

        private static readonly Regex _leftRusQoutExspression = new Regex(@"(?<=^|\W)""(?=\w)",
            RegexOptions.Multiline | RegexOptions.ExplicitCapture | RegexOptions.Compiled);

        private static readonly Regex _rightRusQoutExspression = new Regex(@"(?<=\w([\.,!?\-\u2026\xA9\xAE\u2122]|!\?)?)""(?=$|\W)",
            RegexOptions.Multiline | RegexOptions.ExplicitCapture | RegexOptions.Compiled);

        private static readonly Regex _leftLatQoutExspression = new Regex(@"(?<=^|\W)''(?=\w)",
            RegexOptions.Multiline | RegexOptions.ExplicitCapture | RegexOptions.Compiled);

        private static readonly Regex _rightLatQoutExspression = new Regex(@"(?<=\w')''(?=$|\W)|(?<=\w([\.,!?\-\u2026\xA9\xAE\u2122]|!\?))''(?=$|\W)",
            RegexOptions.Multiline | RegexOptions.ExplicitCapture | RegexOptions.Compiled);

        private static readonly Regex _multiExspression = new Regex(@"(?<=\w\s*)\*(?=\s*\d)",
            RegexOptions.Multiline | RegexOptions.ExplicitCapture | RegexOptions.Compiled);

        private static readonly Regex _minusExspression = new Regex(@"(?<=^|\s)-(?=\d)",
            RegexOptions.Multiline | RegexOptions.ExplicitCapture | RegexOptions.Compiled);

        private static readonly Regex _enDashExspression = new Regex(@"(?<=\d)-(?=\d)",
            RegexOptions.Multiline | RegexOptions.ExplicitCapture | RegexOptions.Compiled);

        private static readonly Regex _emDashExspression = new Regex(@"(?<=\w)--(?=\w)|(?<=(^|\s))--?(?=($|\s))",
            RegexOptions.Multiline | RegexOptions.ExplicitCapture | RegexOptions.Compiled);

        public static string FormatText(string text)
        {
            text = text.Replace("...", ELLIPSIS);
            text = text.Replace("(c)", COPYRIGHT);
            text = text.Replace("(r)", REGISTERED);
            text = text.Replace("(tm)", TRADEMARK);

            text = _leftRusQoutExspression.Replace(text, LEFT_RUS_QUOTATION);
            text = _rightRusQoutExspression.Replace(text, RIGHT_RUS_QUOTATION);
            text = _leftLatQoutExspression.Replace(text, LEFT_LAT_QUOTATION);
            text = _rightLatQoutExspression.Replace(text, RIGHT_LAT_QUOTATION);

            text = _multiExspression.Replace(text, MULTIPLICATION);
            text = _minusExspression.Replace(text, MINUS);

            text = _enDashExspression.Replace(text, EN_DASH);
            text = _emDashExspression.Replace(text, EM_DASH);

            return text;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using MayMart.Wiki.Content;
using MayMart.Wiki.Elements;
using MayMart.Wiki.Elements.Specialized;

namespace MayMart.Wiki.Parsers.Specialized
{
    public class LinkParser : WikiParser
    {
        private static readonly Regex _linkExpression;

        static LinkParser()
        {
            var expressions = new[]
            {
                @"\[url:(?<url>[^]|\s]+)(\|(?<nofollow>nofollow))?(\|(?<text>[^]|]+))?\]",
                @"\[mailto:(?<email>[^]|\s]+)(\|(?<text>[^]|]+))?\]"
            };

            _linkExpression = new Regex(string.Join("|", expressions),
                RegexOptions.Singleline | RegexOptions.ExplicitCapture | RegexOptions.Compiled);
        }

        public override LinkedList<IWikiElement> Parse(ParsingContext parsingContext, ElementContent containerContent)
        {
            var elements = new LinkedList<IWikiElement>();

            foreach (Match match in _linkExpression.Matches(containerContent.ToString()))
            {
                var elementContent = new ElementContent(containerContent, match);
                var innerContent = new ElementContent(containerContent, match.Groups["content"]);

                string originalAddress, normalizedAddress;

                if (match.Groups["url"].Success)
                {
                    originalAddress = match.Groups["url"].Value;
                    normalizedAddress = FormatUrl(originalAddress);
                }
                else if (match.Groups["email"].Success)
                {
                    originalAddress = match.Groups["email"].Value;
                    normalizedAddress = FormatEmail(originalAddress);
                }
                else
                    continue;

                string text = match.Groups["text"].Success ? match.Groups["text"].Value : originalAddress;
                bool noFollow = match.Groups["nofollow"].Success;

                var element = new LinkElement(parsingContext, elementContent, innerContent, normalizedAddress, text, noFollow);

                elements.AddLast(element);
            }

            return elements;
        }

        private static string FormatUrl(string url)
        {
            if (!url.StartsWith("/") && !url.StartsWith("http://", StringComparison.InvariantCultureIgnoreCase))
                url = "http://" + url;

            return url;
        }

        private static string FormatEmail(string email)
        {
            return "mailto:" + email;
        }
    }
}

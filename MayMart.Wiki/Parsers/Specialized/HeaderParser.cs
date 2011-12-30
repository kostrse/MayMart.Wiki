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
    public class HeaderParser : WikiParser
    {
        private static readonly Regex _headerExpression = new Regex(@"^(?<level>={1,4})(?!=)\s*(?<content>\S+(.*\S+)?)\s*$",
            RegexOptions.Multiline | RegexOptions.ExplicitCapture | RegexOptions.Compiled);

        public override LinkedList<IWikiElement> Parse(ParsingContext parsingContext, ElementContent containerContent)
        {
            var elements = new LinkedList<IWikiElement>();

            foreach (Match match in _headerExpression.Matches(containerContent.ToString()))
            {
                var elementContent = new ElementContent(containerContent, match);
                var innerContent = new ElementContent(containerContent, match.Groups["content"]);
                
                int level = match.Groups["level"].Length;

                if (IsLevelEnabled(parsingContext, level))
                {
                    elements.AddLast(new HeaderElement(parsingContext, elementContent, innerContent, level));
                }
                else
                    elements.AddLast(new ParagraphElement(parsingContext, elementContent, innerContent));
            }

            return elements;
        }

        private static bool IsLevelEnabled(ParsingContext parsingContext, int level)
        {
            if (level == 1 && (parsingContext.Headers & HeaderTypes.Header1) == HeaderTypes.Header1)
                return true;

            if (level == 2 && (parsingContext.Headers & HeaderTypes.Header2) == HeaderTypes.Header2)
                return true;

            if (level == 3 && (parsingContext.Headers & HeaderTypes.Header3) == HeaderTypes.Header3)
                return true;

            if (level == 4 && (parsingContext.Headers & HeaderTypes.Header4) == HeaderTypes.Header4)
                return true;

            return false;
        }
    }
}

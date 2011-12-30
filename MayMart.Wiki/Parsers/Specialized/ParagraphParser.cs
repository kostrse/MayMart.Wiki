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
    public class ParagraphParser : WikiParser
    {
        private static readonly Regex _paragraphExpression = new Regex(@"^\s*(?<content>\S+(.*\S+)?)\s*$",
            RegexOptions.Multiline | RegexOptions.ExplicitCapture | RegexOptions.Compiled);

        public override LinkedList<IWikiElement> Parse(ParsingContext parsingContext, ElementContent containerContent)
        {
            var elements = new LinkedList<IWikiElement>();

            foreach (Match match in _paragraphExpression.Matches(containerContent.ToString()))
            {
                var elementContent = new ElementContent(containerContent, match);
                var innerContent = new ElementContent(containerContent, match.Groups["content"]);

                var paragraph = new ParagraphElement(parsingContext, elementContent, innerContent);

                elements.AddLast(paragraph);
            }

            return elements;
        }
    }
}

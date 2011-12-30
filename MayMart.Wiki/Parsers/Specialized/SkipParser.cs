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
    public class SkipParser : WikiParser
    {
        private static readonly Regex _skipExpression = new Regex(@"^\?.*$",
            RegexOptions.Multiline | RegexOptions.ExplicitCapture | RegexOptions.Compiled);

        public override LinkedList<IWikiElement> Parse(ParsingContext parsingContext, ElementContent containerContent)
        {
            if (!parsingContext.SkipLinesEnabled)
                return new LinkedList<IWikiElement>();
            
            var elements = new LinkedList<IWikiElement>();

            foreach (Match match in _skipExpression.Matches(containerContent.ToString()))
            {
                var elementContent = new ElementContent(containerContent, match);

                elements.AddLast(new SkipElement(parsingContext, elementContent));
            }

            return elements;
        }
    }
}

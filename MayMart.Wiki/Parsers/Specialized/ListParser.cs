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
    public class ListParser : WikiParser
    {
        private static readonly Regex _listExpression = new Regex(@"(?<ul>)(^-\s\S.*(\r\n(\s*\r\n)?|\z))+|(?<ol>)(^(\d+\.)\s\S.*(\r\n(\s*\r\n)?|\z))+",
            RegexOptions.Multiline | RegexOptions.ExplicitCapture | RegexOptions.Compiled);

        public override LinkedList<IWikiElement> Parse(ParsingContext parsingContext, ElementContent containerContent)
        {
            var elements = new LinkedList<IWikiElement>();

            foreach (Match match in _listExpression.Matches(containerContent.ToString()))
            {
                var elementContent = new ElementContent(containerContent, match);

                ListType listType;

                if (match.Groups["ul"].Success)
                {
                    listType = ListType.BulletedList;
                }
                else if (match.Groups["ol"].Success)
                {
                    listType = ListType.NumberedList;
                }
                else
                    continue;

                var element = new ListElement(parsingContext, elementContent, listType);

                elements.AddLast(element);
            }

            return elements;
        }
    }
}

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
    public class FormattingParser : WikiParser
    {
        private static readonly Regex _formattingExpression;

        static FormattingParser()
        {
            var expressions = new[]
            {
                @"(?<=(\s|^))((?<symbol>\*)(?=\S)(?<content>([^\*]|\S\*\S|\s\*\s)+)(?<=\S)\*)(?=\W?(\s|$))",
                @"(?<=(\s|^))((?<symbol>_)(?=\S)(?<content>([^_]|\S_\S|\s_\s)+)(?<=\S)_)(?=\W?(\s|$))",
                @"(?<=(\s|^))((?<symbol>\+)(?=\S)(?<content>([^\+]|\S\+\S|\s\+\s)+)(?<=\S)\+)(?=\W?(\s|$))"
            };

            _formattingExpression = new Regex(string.Join("|", expressions),
                RegexOptions.Singleline | RegexOptions.ExplicitCapture | RegexOptions.Compiled);
        }

        public override LinkedList<IWikiElement> Parse(ParsingContext parsingContext, ElementContent containerContent)
        {
            var elements = new LinkedList<IWikiElement>();

            foreach (Match match in _formattingExpression.Matches(containerContent.ToString()))
            {
                var elementContent = new ElementContent(containerContent, match);
                var innerContent = new ElementContent(containerContent, match.Groups["content"]);

                var formatting = GetFormatting(match.Groups["symbol"].Value);

                var element = new FormattingElement(parsingContext, elementContent, innerContent, formatting);

                elements.AddLast(element);
            }

            return elements;
        }

        private static Formatting GetFormatting(string symbol)
        {
            if (symbol == "*")
            {
                return Formatting.Bold;
            }
            else if (symbol == "_")
            {
                return Formatting.Italic;
            }
            else if (symbol == "+")
            {
                return Formatting.Underline;
            }
            else if (symbol == "--")
            {
                return Formatting.Strikethrough;
            }
            else if (symbol == "^^")
            {
                return Formatting.Superscript;
            }
            else if (symbol == ",,")
            {
                return Formatting.Subscript;
            }
            else
                throw new NotSupportedException("Unsupported formatting symbol.");
        }
    }
}

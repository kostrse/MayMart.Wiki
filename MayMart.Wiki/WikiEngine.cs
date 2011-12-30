using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using MayMart.Wiki.Content;
using MayMart.Wiki.Elements.Specialized;
using MayMart.Wiki.Parsers.Specialized;

namespace MayMart.Wiki
{
    public class WikiEngine
    {
        private static readonly Regex _lineEndExpression = new Regex(@"\r(?!\n)|(?<!\r)\n", RegexOptions.Singleline | RegexOptions.Compiled);

        private readonly ParsingContext _parsingContext;

        public IWikiRenderingOptions RenderingOptions
        {
            get
            {
                return _parsingContext;
            }
        }

        public WikiEngine()
        {
            _parsingContext = new ParsingContext();

            _parsingContext.RegisterParserForContainer<RawElement>(new SkipParser(), ParserPriority.High);
            _parsingContext.RegisterParserForContainer<RawElement>(new HeaderParser());
            _parsingContext.RegisterParserForContainer<RawElement>(new ListParser());
            _parsingContext.RegisterParserForContainer<RawElement>(new ParagraphParser(), ParserPriority.Low);

            _parsingContext.RegisterParserForContainer<FormattedTextElement>(new LinkParser());
            _parsingContext.RegisterParserForContainer<FormattedTextElement>(new FormattingParser(), ParserPriority.Low);

            _parsingContext.RegisterParserForContainer<ListElement>(new ListItemParser());
        }

        public string Render(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return null;

            text = NormalizeLineEnds(text);

            text = text.Replace('\x2012', '-');
            text = text.Replace('\x2013', '-');

            RawElement rootElement = new RawElement(_parsingContext, new ElementContent(text));

            StringWriter writer = new StringWriter();

            rootElement.Render(writer);

            return writer.ToString();
        }

        private static string NormalizeLineEnds(string text)
        {
            return _lineEndExpression.Replace(text, Environment.NewLine);
        }
    }
}

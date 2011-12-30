using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MayMart.Wiki.Elements;
using MayMart.Wiki.Parsers;

namespace MayMart.Wiki
{
    public class ParsingContext : IWikiRenderingOptions
    {
        public class ParserItem
        {
            public WikiParser Parser { get; set; }
            public ParserPriority Priority { get; set; }
        }

        private readonly Dictionary<Type, List<ParserItem>> _parsers = new Dictionary<Type, List<ParserItem>>();

        public HeaderTypes Headers { get; set; }
        public bool ApplyTypography { get; set; }
        public bool SkipLinesEnabled { get; set; }

        public ParsingContext()
        {
            Headers = HeaderTypes.Default;
            ApplyTypography = true;
            SkipLinesEnabled = true;
        }

        public void RegisterParserForContainer<TContainer>(WikiParser parser, ParserPriority priority = ParserPriority.Normal)
            where TContainer : IContainerWikiElement
        {
            Type containerType = typeof(TContainer);

            if (!_parsers.ContainsKey(containerType))
                _parsers[containerType] = new List<ParserItem>();

            _parsers[containerType].Add(new ParserItem { Parser = parser, Priority = priority});
        }

        public IEnumerable<WikiParser> GetParsersForContainer(Type containerType)
        {
            if (_parsers.ContainsKey(containerType))
            {
                foreach (var parserItem in _parsers[containerType].OrderBy(p => p.Priority))
                {
                    yield return parserItem.Parser;
                }
            }
        }
    }
}

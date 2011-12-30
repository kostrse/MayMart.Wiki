using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MayMart.Wiki.Content;
using MayMart.Wiki.Elements;

namespace MayMart.Wiki.Parsers
{
    public abstract class WikiParser
    {
        public abstract LinkedList<IWikiElement> Parse(ParsingContext parsingContext, ElementContent containerContent);
    }
}

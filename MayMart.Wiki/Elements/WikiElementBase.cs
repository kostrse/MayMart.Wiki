using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using MayMart.Wiki.Content;

namespace MayMart.Wiki.Elements
{
    public abstract class WikiElementBase : IWikiElement
    {
        public ParsingContext ParsingContext { get; set; }
        public ElementContent Content { get; private set; }

        protected WikiElementBase(ParsingContext parsingContext, ElementContent elementContent)
        {
            ParsingContext = parsingContext;
            Content = elementContent;
        }

        public abstract void Render(TextWriter writer);
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using MayMart.Wiki.Content;

namespace MayMart.Wiki.Elements
{
    public class ContentElementBase : WikiElementBase
    {
        public IWikiElement InnerElement { get; private set; }

        public ContentElementBase(ParsingContext parsingContext, ElementContent elementContent, IWikiElement innerElement)
            : base(parsingContext, elementContent)
        {
            InnerElement = innerElement;
        }

        public override void Render(TextWriter writer)
        {
            InnerElement.Render(writer);
        }
    }
}

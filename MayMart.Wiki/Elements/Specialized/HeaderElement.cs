using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using MayMart.Wiki.Content;

namespace MayMart.Wiki.Elements.Specialized
{
    public class HeaderElement : ContentElementBase
    {
        private readonly int _level;

        public HeaderElement(ParsingContext parsingContext, ElementContent elementContent, ElementContent innerContent, int level)
            : base(parsingContext, elementContent, new FormattedTextElement(parsingContext, innerContent))
        {
            _level = level;
        }

        public override void Render(TextWriter writer)
        {
            writer.Write("<h{0:d}>", _level);
            base.Render(writer);
            writer.Write("</h{0:d}>", _level);
        }
    }
}

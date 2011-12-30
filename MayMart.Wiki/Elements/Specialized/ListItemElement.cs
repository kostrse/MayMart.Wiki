using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using MayMart.Wiki.Content;

namespace MayMart.Wiki.Elements.Specialized
{
    public class ListItemElement : ContentElementBase
    {
        public ListItemElement(ParsingContext parsingContext, ElementContent elementContent, ElementContent innerContent)
            : base(parsingContext, elementContent, new FormattedTextElement(parsingContext, innerContent))
        {
        }

        public override void Render(TextWriter writer)
        {
            writer.Write("<li>");
            base.Render(writer);
            writer.Write("</li>");
        }
    }
}

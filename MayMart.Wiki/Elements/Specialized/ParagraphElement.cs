using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using MayMart.Wiki.Content;

namespace MayMart.Wiki.Elements.Specialized
{
    public class ParagraphElement : ContentElementBase
    {
        public ParagraphElement(ParsingContext parsingContext, ElementContent elementContent, ElementContent innerContent)
            : base(parsingContext, elementContent, new FormattedTextElement(parsingContext, innerContent))
        {
        }

        public override void Render(TextWriter writer)
        {
            writer.Write("<p>");
            base.Render(writer);
            writer.Write("</p>");
        }
    }
}

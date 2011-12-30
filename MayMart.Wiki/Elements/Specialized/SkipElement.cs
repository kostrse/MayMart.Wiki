using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using MayMart.Wiki.Content;

namespace MayMart.Wiki.Elements.Specialized
{
    public class SkipElement : WikiElementBase
    {
        public SkipElement(ParsingContext parsingContext, ElementContent elementContent)
            : base(parsingContext, elementContent)
        {
        }

        public override void Render(TextWriter writer)
        {
            // Nothing
        }
    }
}

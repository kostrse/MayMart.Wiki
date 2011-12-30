using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MayMart.Wiki.Content;

namespace MayMart.Wiki.Elements.Specialized
{
    public class RawElement : ContainerElementBase
    {
        public RawElement(ParsingContext parsingContext, ElementContent elementContent)
            : base(parsingContext, elementContent, elementContent)
        {
        }
    }
}

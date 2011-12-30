using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using MayMart.Wiki.Content;

namespace MayMart.Wiki.Elements.Specialized
{
    public class TextElement : WikiElementBase
    {
        private readonly bool _disableTypography;

        public TextElement(ParsingContext parsingContext, ElementContent elementContent, bool disableTypography = false)
            : base(parsingContext, elementContent)
        {
            _disableTypography = disableTypography;
        }

        public override void Render(TextWriter writer)
        {
            string text = Content.ToString();

            if (ParsingContext.ApplyTypography && !_disableTypography)
                text = TextTypographer.FormatText(text);

            HttpUtility.HtmlEncode(text, writer);
        }
    }
}

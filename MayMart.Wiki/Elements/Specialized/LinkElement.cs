using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using MayMart.Wiki.Content;

namespace MayMart.Wiki.Elements.Specialized
{
    public class LinkElement : WikiElementBase
    {
        private readonly string _url;
        private readonly string _text;
        private readonly bool _noFollow;

        public LinkElement(ParsingContext parsingContext, ElementContent elementContent, ElementContent innerContent, string url, string text, bool noFollow)
            : base(parsingContext, elementContent)
        {
            _url = url;
            _text = text;
            _noFollow = noFollow;
        }

        public override void Render(TextWriter writer)
        {
            writer.Write("<a href=\"{0}\"", HttpUtility.HtmlAttributeEncode(_url));

            if (_noFollow)
                writer.Write(" rel=\"nofollow\"");

            writer.Write(">");
            writer.Write(HttpUtility.HtmlEncode(_text));
            writer.Write("</a>");
        }
    }
}

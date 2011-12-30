using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using MayMart.Wiki.Content;

namespace MayMart.Wiki.Elements.Specialized
{
    public enum Formatting
    {
        Bold,
        Italic,
        Underline,
        Strikethrough,
        Subscript,
        Superscript
    }

    public class FormattingElement : ContentElementBase
    {
        private readonly Formatting _formatting;

        public FormattingElement(ParsingContext parsingContext, ElementContent elementContent, ElementContent innerContent, Formatting formatting)
            : base(parsingContext, elementContent, new FormattedTextElement(parsingContext, innerContent))
        {
            _formatting = formatting;
        }

        public override void Render(TextWriter writer)
        {
            string tagName = GetTagName(_formatting);

            writer.Write("<{0}>", tagName);
            base.Render(writer);
            writer.Write("</{0}>", tagName);
        }

        private static string GetTagName(Formatting formatting)
        {
            switch (formatting)
            {
                case Formatting.Bold:
                    return "b";

                case Formatting.Italic:
                    return "i";

                case Formatting.Underline:
                    return "u";

                case Formatting.Strikethrough:
                    return "del";

                case Formatting.Subscript:
                    return "sub";
                
                case Formatting.Superscript:
                    return "sup";
                
                default:
                    throw new ArgumentOutOfRangeException("formatting");
            }
        }
    }
}

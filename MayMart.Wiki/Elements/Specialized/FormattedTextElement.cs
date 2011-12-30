using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MayMart.Wiki.Content;

namespace MayMart.Wiki.Elements.Specialized
{
    public class FormattedTextElement : ContainerElementBase
    {
        public FormattedTextElement(ParsingContext parsingContext, ElementContent elementContent)
            : base(parsingContext, elementContent, elementContent)
        {
        }

        protected override void ParseContent()
        {
            base.ParseContent();

            var currentChunk = InnerContentChunks.First;

            while (currentChunk != null)
            {
                AddElement(new TextElement(ParsingContext, currentChunk.Value));

                var nextChunk = currentChunk.Next;

                InnerContentChunks.Remove(currentChunk);
                currentChunk = nextChunk;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using MayMart.Wiki.Content;

namespace MayMart.Wiki.Elements
{
    public abstract class ContainerElementBase : WikiElementBase, IContainerWikiElement
    {
        public LinkedList<IWikiElement> InnerElements { get; private set; }
        public LinkedList<ElementContent> InnerContentChunks { get; private set; }

        protected ContainerElementBase(ParsingContext parsingContext, ElementContent elementContent, ElementContent innerContent)
            : base(parsingContext, elementContent)
        {
            InnerElements = new LinkedList<IWikiElement>();
            InnerContentChunks = new LinkedList<ElementContent>();

            if (innerContent.Length > 0)
                InnerContentChunks.AddFirst(innerContent);
        }

        public override void Render(TextWriter writer)
        {
            ParseContent();
            RenderContent(writer);
        }

        protected virtual void ParseContent()
        {
            foreach (var parser in ParsingContext.GetParsersForContainer(GetType()))
            {
                var currentChunk = InnerContentChunks.First;

                if (currentChunk == null)
                    break;

                bool leaveCurrent;

                do
                {
                    leaveCurrent = false;

                    var parsedElements = parser.Parse(ParsingContext, currentChunk.Value);

                    foreach (var parsedElement in parsedElements)
                    {
                        // Adding element to collection

                        AddElement(parsedElement);

                        // Splitting current content on two separate parts: before found element and after

                        var contentBefore = currentChunk.Value.ContentBefore(parsedElement.Content);

                        if (contentBefore.Length > 0)
                            InnerContentChunks.AddBefore(currentChunk, contentBefore);

                        var contentAfter = currentChunk.Value.ContentAfter(parsedElement.Content);

                        if (contentAfter.Length > 0)
                        {
                            var rightChunk = InnerContentChunks.AddAfter(currentChunk, contentAfter);

                            InnerContentChunks.Remove(currentChunk);
                            currentChunk = rightChunk;
                        }
                        else
                        {
                            leaveCurrent = true;

                            var nextChunk = currentChunk.Next;

                            InnerContentChunks.Remove(currentChunk);
                            currentChunk = nextChunk;
                            
                            break;
                        }
                    }
                }
                while (currentChunk != null && (leaveCurrent || (currentChunk = currentChunk.Next) != null));
            }
        }

        protected void AddElement(IWikiElement element)
        {
            var node = InnerElements.First;

            while (node != null)
            {
                if (node.Value.Content.Position > element.Content.Position)
                    break;

                node = node.Next;
            }

            if (node != null)
            {
                InnerElements.AddBefore(node, element);
            }
            else
                InnerElements.AddLast(element);
        }

        protected virtual void RenderContent(TextWriter writer)
        {
            foreach (var element in InnerElements)
            {
                element.Render(writer);
            }
        }
    }
}

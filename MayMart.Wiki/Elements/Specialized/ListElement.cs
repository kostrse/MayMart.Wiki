using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using MayMart.Wiki.Content;

namespace MayMart.Wiki.Elements.Specialized
{
    public enum ListType
    {
        BulletedList,
        NumberedList
    }

    public class ListElement : ContainerElementBase
    {
        private readonly ListType _listType;

        public ListElement(ParsingContext parsingContext, ElementContent elementContent, ListType listType)
            : base(parsingContext, elementContent, elementContent)
        {
            _listType = listType;
        }

        protected override void RenderContent(TextWriter writer)
        {
            string tagName = GetTagName(_listType);

            writer.Write("<{0}>", tagName);
            base.RenderContent(writer);
            writer.Write("</{0}>", tagName);
        }
        
        private static string GetTagName(ListType listType)
        {
            switch (listType)
            {
                case ListType.BulletedList:
                    return "ul";
                
                case ListType.NumberedList:
                    return "ol";
                
                default:
                    throw new ArgumentOutOfRangeException("listType");
            }
        }
    }
}

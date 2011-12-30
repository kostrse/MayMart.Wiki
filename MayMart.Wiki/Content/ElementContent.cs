using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace MayMart.Wiki.Content
{
    public struct ElementContent
    {
        private readonly string _text;
        private readonly int _position;
        private readonly int _length;

        public string Text
        {
            get
            {
                return _text;
            }
        }

        public int Position
        {
            get
            {
                return _position;
            }
        }

        public int Length
        {
            get
            {
                return _length;
            }
        }

        public ElementContent(string content)
        {
            if (content == null)
                content = string.Empty;

            _text = content;
            _position = 0;
            _length = content.Length;
        }

        public ElementContent(string content, int position, int length)
        {
            if (content == null)
                content = string.Empty;

            if (position + length > content.Length)
                throw new ArgumentOutOfRangeException("length", length, "Invalid content length.");

            _text = content;
            _position = position;
            _length = length;
        }

        public ElementContent(ElementContent content, int relativePosition, int length)
        {
            if (relativePosition + length > content.Length)
                throw new ArgumentOutOfRangeException("length", length, "Invalid content length.");

            _text = content.Text;
            _position = content.Position + relativePosition;
            _length = length;
        }

        public ElementContent(ElementContent content, Capture capture)
            : this(content, capture.Index, capture.Length)
        {
        }

        public ElementContent ContentBefore(ElementContent innerContent)
        {
            return new ElementContent(Text, Position, innerContent.Position - Position);
        }

        public ElementContent ContentAfter(ElementContent innerContent)
        {
            int last = Position + Length;
            int innerLast = innerContent.Position + innerContent.Length;

            return new ElementContent(Text, innerLast, last - innerLast);
        }

        public override string ToString()
        {
            return Text.Substring(Position, Length);
        }
    }
}

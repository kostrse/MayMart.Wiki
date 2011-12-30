using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using MayMart.Wiki.Content;

namespace MayMart.Wiki.Elements
{
    public interface IWikiElement
    {
        ElementContent Content { get; }

        void Render(TextWriter writer);
    }
}

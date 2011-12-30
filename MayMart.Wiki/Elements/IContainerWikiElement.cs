using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MayMart.Wiki.Elements
{
    public interface IContainerWikiElement
    {
        LinkedList<IWikiElement> InnerElements { get; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MayMart.Wiki
{
    [Flags]
    public enum HeaderTypes
    {
        None = 0,
        
        Header1 = 1,
        Header2 = 2,
        Header3 = 4,
        Header4 = 8,
        
        All = Header1 | Header2 | Header3 | Header4,
        Default = Header2 | Header3 | Header4
    }
}

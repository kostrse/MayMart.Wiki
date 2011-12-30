using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MayMart.Wiki
{
    public interface IWikiRenderingOptions
    {
        HeaderTypes Headers { get; set; }
        bool ApplyTypography { get; set; }
        bool SkipLinesEnabled { get; set; }
    }
}

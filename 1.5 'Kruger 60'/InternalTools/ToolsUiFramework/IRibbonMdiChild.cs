using System;
using System.Windows.Forms;

namespace IpTviewr.Internal.Tools.UiFramework
{
    public interface IRibbonMdiChild
    {
        IRibbonMdiForm RibbonMdiForm { set; }

        Guid TypeGuid { get; }

        MdiRibbonContext[] GetChildContexts();

        bool IsActiveChild { set; }

        Form Form { get; }
    } // IRibbonMdiChild
} // namespace
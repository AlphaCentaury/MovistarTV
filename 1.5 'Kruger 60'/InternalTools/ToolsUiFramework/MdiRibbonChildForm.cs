using System;
using System.Windows.Forms;

namespace IpTviewr.Internal.Tools.UiFramework
{
    public class MdiRibbonChildForm: Form, IRibbonMdiChild
    {
        #region Implementation of IRibbonMdiChild

        public IRibbonMdiForm RibbonMdiForm { get; set; }

        public virtual Guid TypeGuid => throw new NotSupportedException();
        
        public virtual MdiRibbonContext[] GetChildContexts() => null;

        public virtual bool IsActiveChild { get; set; }

        public Form Form => this;

        #endregion
    } // class MdiRibbonChildForm
} // namespace
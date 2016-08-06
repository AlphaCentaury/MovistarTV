using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Project.DvbIpTv.UiServices.Common.Forms
{
    public interface IBackgroundWorkerDialog
    {
        IWin32Window ThisWindow
        {
            get;
        } // ThisWindow

        Form OwnerForm
        {
            get;
        } // OwnerForm

        void SetProgressText(string text);
        void SetProgressMinMax(int min, int max);
        void SetProgress(int value);
        void SetProgressUndefined();
        bool QueryCancel();
    } // interface IBackgroundWorkerDialog
} // namespace

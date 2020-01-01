// ==============================================================================
// 
//   Copyright (C) 2014-2020, GitHub/Codeplex user AlphaCentaury
//   All rights reserved.
// 
//     See 'LICENSE.MD' file (or 'license.txt' if missing) in the project root
//     for complete license information.
// 
//   http://www.alphacentaury.org/movistartv
//   https://github.com/AlphaCentaury
// 
// ==============================================================================

using System.Windows.Forms;

namespace IpTviewr.UiServices.Common.Forms
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

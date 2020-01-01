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

using System;
using IpTviewr.UiServices.Common.Forms;

namespace IpTviewr.UiServices.Forms
{
    public partial class MulticastScannerOptionsDialog : CommonBaseForm
    {
        public MulticastScannerOptionsDialog()
        {
            InitializeComponent();
        } // constructor

        public enum ScanWhatList
        {
            AllServices,
            ActiveServices,
            DeadServices
        } // ScanWhatList

        /// <summary>
        /// Timeout, in milliseconds
        /// </summary>
        public int Timeout
        {
            get;
            private set;
        } // Timeout

        public ScanWhatList ScanList
        {
            get;
            private set;
        } // ScanList

        private void buttonStart_Click(object sender, EventArgs e)
        {
            Timeout = (int)(numericTimeout.Value * 1000);

            // What
            if (radioScanAll.Checked) ScanList = ScanWhatList.AllServices;
            else if (radioScanActive.Checked) ScanList = ScanWhatList.ActiveServices;
            else if (radioScanDead.Checked) ScanList = ScanWhatList.DeadServices;
            else ScanList = ScanWhatList.AllServices;
        } // buttonStart_Click
    } // AskMulticastScannerDlg
} // namespace

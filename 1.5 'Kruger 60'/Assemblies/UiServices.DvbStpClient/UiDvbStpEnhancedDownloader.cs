// Copyright (C) 2014-2019, GitHub/Codeplex user AlphaCentaury
// 
// All rights reserved, except those granted by the governing license of this software.
// See 'license.txt' file in the project root for complete license information.
// 
// http://www.alphacentaury.org/movistartv https://github.com/AlphaCentaury

using Microsoft.SqlServer.MessageBox;
using IpTviewr.UiServices.DvbStpClient.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace IpTviewr.UiServices.DvbStpClient
{
    public sealed class UiDvbStpEnhancedDownloader : UiDvbStpBaseDownloader
    {
        // "Input" properties

        public UiDvbStpEnhancedDownloadRequest Request
        {
            get;
            set;
        } // Request

        // "Return" properties

        public UiDvbStpEnhancedDownloadResponse Response
        {
            get;
            private set;
        } // Response

        protected override UiDvbStpBaseDownloadResponse ShowDialog(IWin32Window owner)
        {
            using (var dlg = new DvbStpEnhancedDownloadDialog(Request))
            {
                dlg.ShowDialog(owner);
                TelemetryScreenName = dlg.TelemetryScreenName;
                Response = dlg.Response;
                return Response;
            } // using
        } // ShowDialog
    } // UiDvbStpEnhancedDownloader
} // namespace

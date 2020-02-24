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

using Microsoft.SqlServer.MessageBox;
using IpTviewr.Common.Telemetry;
using IpTviewr.UiServices.Common.Forms;
using System;
using System.Net.Sockets;
using System.Windows.Forms;

namespace IpTviewr.UiServices.DvbStpClient
{
    public abstract class UiDvbStpBaseDownloader
    {
        // "Input" properties

        public string CaptionUserCancelled
        {
            get;
            set;
        } // CaptionUserCancelled

        public string TextUserCancelled
        {
            get;
            set;
        } // TextUserCancelled

        public string TextDownloadException
        {
            get;
            set;
        } // TextDownloadException

        // "Return" properties

        public bool IsOk
        {
            get;
            private set;
        } // IsOk

        protected string TelemetryScreenName
        {
            get;
            set;
        } // TelemetryScreenName

        public void Download(IWin32Window owner)
        {
            var response = ShowDialog(owner);

            IsOk = ((response.UserCancelled == false) && (response.DownloadException == null));

            if (response.UserCancelled)
            {
                var box = new ExceptionMessageBox()
                {
                    Caption = CaptionUserCancelled ?? Properties.Texts.HelperUserCancelledCaption,
                    Text = TextUserCancelled ?? Properties.Texts.HelperUserCancelledText,
                    Beep = true,
                    Symbol = ExceptionMessageBoxSymbol.Information
                };
                box.Show(owner);

                return;
            } // if

            if (response.DownloadException != null)
            {
                HandleException(owner, response.DownloadException);
            } // if
        } // Download

        protected abstract UiDvbStpBaseDownloadResponse ShowDialog(IWin32Window owner);

        private void HandleException(IWin32Window owner, Exception ex)
        {
            var message = TextDownloadException ?? Properties.Texts.HelperExceptionText;
            AppTelemetry.ScreenException(ex, TelemetryScreenName, message);

            switch (ex)
            {
                case SocketException isSocket:
                    message = string.Format(Properties.Texts.SocketException, message, isSocket.SocketErrorCode);
                    break;
                case TimeoutException _:
                    message = string.Format(Properties.Texts.TimeoutException, message);
                    break;
            } // switch

            var box = new ExceptionMessageBox()
            {
                Buttons = ExceptionMessageBoxButtons.Custom,
                InnerException = ex,
                Text = message,
                DefaultButton = ExceptionMessageBoxDefaultButton.Button2,
                CustomSymbol = Properties.Resources.DvbStpDownload_Error_48x48
            };
            box.SetButtonText(ExceptionMessageBox.OKButtonText, Properties.Texts.HandleExceptionHelpButton);
            box.Show(owner);

            if (box.CustomDialogResult == ExceptionMessageBoxDialogResult.Button2)
            {
                AppTelemetry.CustomEvent(TelemetryScreenName, "ShowDialog", "UiServices.DvbStpClient.HelpDialog", TelemetryScreenName);
                HelpDialog.ShowRtfHelp(owner, Properties.Texts.RtfTroubleshootingGuide);
            } // if
        } // HandleException
    } // abstract class UiDvbStpBaseDownloader
} // namespace

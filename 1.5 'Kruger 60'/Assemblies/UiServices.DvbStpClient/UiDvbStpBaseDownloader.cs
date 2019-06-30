// Copyright (C) 2014-2019, GitHub/Codeplex user AlphaCentaury
// 
// All rights reserved, except those granted by the governing license of this software.
// See 'license.txt' file in the project root for complete license information.
// 
// http://www.alphacentaury.org/movistartv https://github.com/AlphaCentaury

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
            string message;

            BasicGoogleTelemetry.SendExtendedExceptionHit(ex, false, TelemetryScreenName, TelemetryScreenName);

            var isSocket = ex as SocketException;
            var isTimeout = ex as TimeoutException;

            message = TextDownloadException ?? Properties.Texts.HelperExceptionText;
            if (isSocket != null) message = string.Format(Properties.Texts.SocketException, message, isSocket.SocketErrorCode);
            else if (isTimeout != null) message = string.Format(Properties.Texts.TimeoutException, message);

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
                BasicGoogleTelemetry.SendEventHit("ShowDialog", "UiServices.DvbStpClient.HelpDialog", TelemetryScreenName, TelemetryScreenName);
                HelpDialog.ShowRtfHelp(owner, Properties.Texts.RtfTroubleshootingGuide);
            } // if
        } // HandleException
    } // abstract class UiDvbStpBaseDownloader
} // namespace

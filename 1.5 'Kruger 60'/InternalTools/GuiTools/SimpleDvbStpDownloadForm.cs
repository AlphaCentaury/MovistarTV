// Copyright (C) 2014-2016, Codeplex user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using Project.IpTv.UiServices.DvbStpClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace Project.IpTv.Internal.Tools.GuiTools
{
    public partial class SimpleDvbStpDownloadForm : Form
    {
        public SimpleDvbStpDownloadForm()
        {
            InitializeComponent();
            this.Icon = Properties.Resources.GuiTools;
        } // constructor

        private void buttonDownload_Click(object sender, EventArgs e)
        {
            string context = null;
            string input;

            textBoxResult.Text = null;

            try
            {
                context = "IP Address";
                input = textIpAddress.Text.Trim();
                var ipAddress = IPAddress.Parse(input);

                context = "Port";
                var port = Program.ParseNumber(textPort.Text);

                context = "PayloadID";
                var payloadId = (byte)Program.ParseNumber(textPayloadId.Text);

                context = "SegmentID";
                var segmentId = (short?)Program.ParseNullableNumber(textSegmentId.Text);

                context = "Start downloader";
                var downloader = new UiDvbStpSimpleDownloader();
                downloader.Request = new UiDvbStpSimpleDownloadRequest()
                {
                    MulticastAddress = ipAddress,
                    MulticastPort = port,
                    PayloadId = payloadId,
                    SegmentId = segmentId,
                    NoDataTimeout = 120000,
                };
                downloader.Download(this);

                context = "After download";
                if (downloader.IsOk)
                {
                    textBoxResult.Text = Encoding.UTF8.GetString(downloader.Response.PayloadData);
                } // if
            }
            catch (Exception ex)
            {
                MyApplication.HandleException(this, context, ex);
            } // try-catch
        } // buttonDownload_Click
    } // class SimpleDvbStpDownloadForm
} // namespace

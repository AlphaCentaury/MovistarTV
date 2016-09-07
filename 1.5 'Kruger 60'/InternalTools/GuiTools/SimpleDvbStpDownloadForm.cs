// Copyright (C) 2014-2016, Codeplex/GitHub user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using IpTviewr.UiServices.DvbStpClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace IpTviewr.Internal.Tools.GuiTools
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
                    if (radioFormatBinary.Checked)
                    {
                        textBoxResult.ScrollBars = ScrollBars.Vertical;
                        textBoxResult.WordWrap = true;
                    }
                    else
                    {
                        textBoxResult.ScrollBars = ScrollBars.Both;
                        textBoxResult.WordWrap = false;
                    } // if-else
                    textBoxResult.Text = GetPayloadText(downloader.Response.PayloadData);
                } // if
            }
            catch (Exception ex)
            {
                MyApplication.HandleException(this, context, ex);
            } // try-catch
        } // buttonDownload_Click

        private string GetPayloadText(byte[] data)
        {
            if (radioFormatBinary.Checked)
            {
                var count = data.Length;
                var buffer = new StringBuilder(count);
                for (int index = 0; index < count; index++)
                {
                    var b = data[index];
                    buffer.Append((b < 32) ? '·' : (char)b);
                } // for index

                return buffer.ToString();
            }
            else
            {
                using (var input = new MemoryStream(data, false))
                {
                    var xmlDoc = new XmlDocument();
                    xmlDoc.Load(input);

                    var buffer = new StringBuilder();
                    var settings = new XmlWriterSettings()
                    {
                        Indent = true,
                        WriteEndDocumentOnClose = true,
                    }; // settings
                    using (var writer = XmlWriter.Create(buffer, settings))
                    {
                        xmlDoc.Save(writer);
                    } // using writer

                    return buffer.ToString();
                } // using input

            } // if-else
        } // GetPayloadText
    } // class SimpleDvbStpDownloadForm
} // namespace

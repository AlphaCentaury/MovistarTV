// Copyright (C) 2014-2019, GitHub/Codeplex user AlphaCentaury
// 
// All rights reserved, except those granted by the governing license of this software.
// See 'license.txt' file in the project root for complete license information.
// 
// http://www.alphacentaury.org/movistartv https://github.com/AlphaCentaury

using IpTviewr.Common;
using IpTviewr.Common.Telemetry;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace IpTviewr.UiServices.Common.Forms
{
    public partial class HelpDialog : Form
    {
        public static DialogResult ShowRtfHelp(IWin32Window owner, string rtfHelpText, string caption = null)
        {
            using (var dialog = new HelpDialog())
            {
                BasicGoogleTelemetry.SendScreenHit(dialog, caption);
                dialog.richTextHelp.Rtf = rtfHelpText;
                if (caption != null) dialog.Text = caption;
                return dialog.ShowDialog(owner);
            } // using dialog
        } // ShowRtfHelp

        public static DialogResult ShowPlainTextHelp(IWin32Window owner, string helpText, string caption = null)
        {
            using (var dialog = new HelpDialog())
            {
                BasicGoogleTelemetry.SendScreenHit(dialog, caption);
                dialog.richTextHelp.Text = helpText;
                if (caption != null) dialog.Text = caption;
                return dialog.ShowDialog(owner);
            } // using dialog
        } // ShowRtfHelp

        public HelpDialog()
        {
            InitializeComponent();
        } // constructor

        private void richTextHelp_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            Launcher.OpenUrl(this, e.LinkText);
        } // richTextHelp_LinkClicked
    } // class HelpDialog
} // namespace

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

using IpTviewr.Common;
using IpTviewr.Common.Telemetry;
using System.Windows.Forms;

namespace IpTviewr.UiServices.Common.Forms
{
    public partial class HelpDialog : Form
    {
        public static DialogResult ShowRtfHelp(IWin32Window owner, string rtfHelpText, string caption = null, bool maximize = false)
        {
            using var dialog = new HelpDialog();
            AppTelemetry.FormEvent(AppTelemetry.LoadEvent, dialog, caption);
            dialog.richTextHelp.Rtf = rtfHelpText;
            if (caption != null) dialog.Text = caption;
            if (maximize) dialog.WindowState = FormWindowState.Maximized;

            return dialog.ShowDialog(owner);
        } // ShowRtfHelp

        public static DialogResult ShowPlainTextHelp(IWin32Window owner, string helpText, string caption = null, bool maximize = false)
        {
            using var dialog = new HelpDialog();
            AppTelemetry.FormEvent(AppTelemetry.LoadEvent, dialog, caption);
            dialog.richTextHelp.Text = helpText;
            if (caption != null) dialog.Text = caption;
            if (maximize) dialog.WindowState = FormWindowState.Maximized;

            return dialog.ShowDialog(owner);
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

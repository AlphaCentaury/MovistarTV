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

using IpTviewr.Native;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IpTviewr.Internal.Tools.ChannelLogos
{
    public partial class FormSvgLogosExporter : Form
    {
        private readonly TextBoxOutputWriter _output;
        private CancellationTokenSource _tokenSource;

        public FormSvgLogosExporter()
        {
            InitializeComponent();
            Icon = Properties.Resources.IPTViewr_Tool;
            _output = new TextBoxOutputWriter(textBoxOutput, timerOuput);
        } // constructor

        #region Overrides of Form

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                components?.Dispose();
                _tokenSource?.Dispose();
            } //if
            base.Dispose(disposing);
        } // Dispose

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            textBoxInkscape.Text = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), @"Inkscape\Inkscape.com");

            var baseFolder = Path.GetFullPath(Path.Combine(Application.StartupPath, @"..\..\..\.."));
            textBoxFrom.Text = Path.Combine(baseFolder, @"Logos\Sources");
            textBoxTo.Text = Path.Combine(baseFolder, @"Logos");
        } // OnLoad

        #endregion

        private void checkBoxExport_CheckedChanged(object sender, EventArgs e)
        {
            groupBoxSizes.Enabled = checkBoxExport.Checked;
            checkBoxExportAll.Enabled = checkBoxExport.Checked;
        } // checkBoxExport_CheckedChanged

        private void checkBoxIcons_CheckedChanged(object sender, EventArgs e)
        {
            checkBoxRebuildAllIcons.Enabled = checkBoxIcons.Checked;
        } // checkBoxIcons_CheckedChanged

        private async void buttonExecute_Click(object sender, EventArgs e)
        {
            buttonCancel.Enabled = true;
            tabControlMain.SelectedTab = tabPageOutput;
            tabPageOptions.Enabled = false;
            _output.Clear();
            _output.Start();

            try
            {
                _tokenSource?.Dispose();
                _tokenSource = new CancellationTokenSource();
                var token = _tokenSource.Token;
                if (checkBoxExport.Checked) await Export(token);
                if (checkBoxIcons.Checked) await BuildIcons(token);
            }
            catch (Exception ex)
            {
                _output.WriteException(ex);
            } // try-catch

            _output.Stop();
            buttonCancel.Enabled = false;
            tabPageOptions.Enabled = true;
        } // buttonExecute_Click

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            _tokenSource?.Cancel();
            buttonCancel.Enabled = false;
        } // buttonCancel_Click

        #region Actions: Export

        private Task Export(CancellationToken token)
        {
            _output.WriteLine("---------------------------------------------------");
            _output.WriteLine("Exporting logos");
            _output.WriteLine("---------------------------------------------------");
            _output.WriteLine();

            var sizes = new List<int>(8);
            if (checkBoxSize24.Checked) sizes.Add(24);
            if (checkBoxSize32.Checked) sizes.Add(32);
            if (checkBoxSize48.Checked) sizes.Add(48);
            if (checkBoxSize64.Checked) sizes.Add(64);
            if (checkBoxSize96.Checked) sizes.Add(96);
            if (checkBoxSize128.Checked) sizes.Add(128);
            if (checkBoxSize256.Checked) sizes.Add(256);

            var subPath = radioIsServices.Checked ? "Services" : "Providers";
            var source = Path.Combine(textBoxFrom.Text, subPath);
            var destination = Path.Combine(textBoxTo.Text, subPath);
            var exporter = new LogosSvgExporter(textBoxInkscape.Text, sizes, source, destination, radioIsServices.Checked, checkBoxExportAll.Checked, _output);
            return exporter.ExportAsync(Handle, token);
        } // Export

        #endregion

        #region Actions: Icons

        private Task BuildIcons(CancellationToken token)
        {
            _output.WriteLine("---------------------------------------------------");
            _output.WriteLine("Building icons");
            _output.WriteLine("---------------------------------------------------");
            _output.WriteLine();

            var subPath = radioIsServices.Checked ? "Services" : "Providers";
            var source = Path.Combine(textBoxTo.Text, subPath);
            var builder = new LogosIconBuilder(source, radioIsServices.Checked, checkBoxRebuildAllIcons.Checked, _output);

            return builder.BuildAsync(token);
        } // BuildIcons

        #endregion
    } // partial class FormSvgLogosExporter
} // namespace

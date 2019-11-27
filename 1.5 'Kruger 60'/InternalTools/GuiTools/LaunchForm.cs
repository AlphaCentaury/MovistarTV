// Copyright (C) 2014-2019, GitHub/Codeplex user AlphaCentaury
// 
// All rights reserved, except those granted by the governing license of this software.
// See 'license.txt' file in the project root for complete license information.
// 
// http://www.alphacentaury.org/movistartv https://github.com/AlphaCentaury

using System;
using System.Windows.Forms;

namespace IpTviewr.Internal.Tools.GuiTools
{
    public partial class LaunchForm : Form
    {
        private Type SelectedForm;

        public LaunchForm()
        {
            InitializeComponent();
            Icon = Properties.Resources.GuiTools;
        } // constructor

        private void buttonExecute_Click(object sender, EventArgs e)
        {
            if (Activator.CreateInstance(SelectedForm) is Form form)
            {
                form.Show();
                form.Dispose();
            } // if
        } // buttonExecute_Click

        private void radioOption_CheckedChanged(object sender, EventArgs e)
        {
            SelectedForm = GetSelectedForm();
            buttonExecute.Enabled = SelectedForm != null;
        } // radioOption_CheckedChanged

        private Type GetSelectedForm()
        {
            if (radioSimpleDownload.Checked) return typeof(SimpleDvbStpDownloadForm);
            if (radioDvbStpExplorer.Checked) return typeof(DvbStpStreamExplorerForm);
            if (radioMulticastExplorer.Checked) return typeof(MulticastStreamExplorerForm);
            if (radioOpchExplorer.Checked) return typeof(OpchExplorerForm);
            if (radioBinaryEditor.Checked) return typeof(BinaryEditorForm);
            if (radioIconBuilder.Checked) return typeof(IconBuilder);
            //if (radioRibbon.Checked) return typeof(RibbonForm);
            return null;
        } // GetSelectedForm

        private void groupBoxTools_Enter(object sender, EventArgs e)
        {

        }
    } // class LaunchForm
} // namespace

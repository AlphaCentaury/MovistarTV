// Copyright (C) 2014-2016, Codeplex/GitHub user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace IpTviewr.Internal.Tools.GuiTools
{
    public partial class LaunchForm : Form
    {
        private Type SelectedForm;

        public LaunchForm()
        {
            InitializeComponent();
            this.Icon = Properties.Resources.GuiTools;
        } // constructor

        private void buttonExecute_Click(object sender, EventArgs e)
        {
            var form = Activator.CreateInstance(SelectedForm) as Form;
            if (form != null)
            {
                form.Show();
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
            else if (radioDvbStpExplorer.Checked) return typeof(DvbStpStreamExplorerForm);
            else if (radioMulticastExplorer.Checked) return typeof(MulticastStreamExplorerForm);
            else if (radioOpchExplorer.Checked) return typeof(OpchExplorerForm);
            else if (radioBinaryEditor.Checked) return typeof(BinaryEditorForm);
            else return null;
        } // GetSelectedForm
    } // class LaunchForm
} // namespace

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

namespace Project.IpTv.Internal.Tools.GuiTools
{
    public partial class LaunchForm : Form
    {
        public LaunchForm()
        {
            InitializeComponent();
            this.Icon = Properties.Resources.GuiTools;
        } // constructor

        private void buttonExecute_Click(object sender, EventArgs e)
        {
            Form form = null;

            if (radioSimpleDownload.Checked) form = new SimpleDvbStpDownloadForm();
            else if (radioMulticastExplorer.Checked) form = new MulticastStreamExplorerForm();
            else if (radioOpchExplorer.Checked) form = new OpchExplorerForm();

            if (form != null)
            {
                form.Show();
            } // if
        } // buttonExecute_Click
    } // class LaunchForm
} // namespace

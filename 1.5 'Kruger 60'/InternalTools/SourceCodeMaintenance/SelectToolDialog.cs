// Copyright (C) 2014-2019, GitHub/Codeplex user AlphaCentaury
// 
// All rights reserved, except those granted by the governing license of this software.
// See 'license.txt' file in the project root for complete license information.
// 
// http://www.alphacentaury.org/movistartv https://github.com/AlphaCentaury

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AlphaCentaury.Tools.SourceCodeMaintenance
{
    public partial class SelectToolDialog : Form
    {
        public SelectToolDialog()
        {
            InitializeComponent();
        } // constructor

        public Lazy<IMaintenanceTool, IMaintenanceToolMetadata> SelectedTool { get; private set; }

        private void DialogSelectTool_Load(object sender, EventArgs e)
        {
            listBoxTools.DataSource = Program.ToolsUiNames;
            listBoxTools_SelectedIndexChanged(listBoxTools, EventArgs.Empty);
        } // DialogSelectTool_Load

        private void listBoxTools_SelectedIndexChanged(object sender, EventArgs e)
        {
            var enable = (listBoxTools.SelectedIndex >= 0);
            SelectedTool = enable ? Program.ToolsUi[listBoxTools.SelectedIndex] : null;
            buttonUsage.Enabled = enable && SelectedTool.Metadata.HasUsage;
            buttonOk.Enabled = enable;
            SelectedTool = Program.ToolsUi[listBoxTools.SelectedIndex];
        } // listBoxTools_SelectedIndexChanged

        private void listBoxTools_DoubleClick(object sender, EventArgs e)
        {
            buttonOk.PerformClick();
        } // listBoxTools_DoubleClick

        private void buttonUsage_Click(object sender, EventArgs e)
        {

        } // buttonUsage_Click
    } // class SelectToolDialog
} // namespace

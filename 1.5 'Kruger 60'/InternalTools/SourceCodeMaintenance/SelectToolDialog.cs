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

using System;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AlphaCentaury.Tools.SourceCodeMaintenance.Interfaces;

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
            listViewTools.BeginUpdate();
            listViewTools_Resize(listViewTools, EventArgs.Empty);
            listViewTools.Items.AddRange(Program.ToolsUi.Select(tool => new ListViewItem(tool.Metadata.Name, 0)).ToArray());
            listViewTools.EndUpdate();

            if (listViewTools.Items.Count > 0)
            {
                listViewTools.Items[0].Selected = true;
            }
            else
            {
                listViewTools.Enabled = false;
                buttonOk.Enabled = false;
            } // if-else
        } // DialogSelectTool_Load

        private void listViewTools_SelectedIndexChanged(object sender, EventArgs e)
        {
            var enable = (listViewTools.SelectedIndices.Count > 0);
            SelectedTool = enable ? Program.ToolsUi[listViewTools.SelectedIndices[0]] : null;
            buttonUsage.Enabled = enable && SelectedTool.Metadata.HasUsage;
            buttonOk.Enabled = enable;

            if (SelectedTool == null) return;

            var buffer = new StringBuilder();
            buffer.AppendLine($"Guid: {SelectedTool.Metadata.Guid}");
            if (SelectedTool.Metadata.CliName != null)
            {
                buffer.AppendLine($"CLI name: {SelectedTool.Metadata.CliName}");
            } // if
            buffer.AppendLine($"Has parameters: {SelectedTool.Metadata.HasParameters}");
            buffer.AppendLine($"Has file parameters: {SelectedTool.Metadata.HasFileParameters}");
            buffer.AppendLine($"Has UI: {SelectedTool.Metadata.HasUi}");
            buffer.AppendLine($"Has Usage: {SelectedTool.Metadata.HasUsage}");
            textBoxInfo.Text = buffer.ToString();
        } // listViewTools_SelectedIndexChanged

        private void listViewTools_DoubleClick(object sender, EventArgs e)
        {
            buttonOk.PerformClick();
        } // listViewTools_DoubleClick

        private void buttonUsage_Click(object sender, EventArgs e)
        {
            var writer = new Helpers.TextBoxOutputWriter(textBoxInfo);
            writer.Start();
            SelectedTool.Value.ShowUsage(writer);
            writer.Stop();
        } // buttonUsage_Click

        private void listViewTools_Resize(object sender, EventArgs e)
        {
            columnHeaderToolName.Width = listViewTools.ClientSize.Width;
        } // listViewTools_Resize
    } // class SelectToolDialog
} // namespace

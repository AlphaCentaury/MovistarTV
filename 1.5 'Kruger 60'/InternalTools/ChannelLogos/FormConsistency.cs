using Project.IpTv.UiServices.Configuration;
using Project.IpTv.UiServices.Discovery;
using Project.IpTv.UiServices.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Project.IpTv.Internal.Tools.ChannelLogos
{
    public partial class FormConsistency : Form
    {
        public FormConsistency()
        {
            InitializeComponent();
            Icon = Properties.Resources.IPTViewr_Tool;
            buttonRun.Enabled = false;
        } // constructor

        private void comboCheck_SelectedIndexChanged(object sender, EventArgs e)
        {
            buttonRun.Enabled = !(comboCheck.SelectedItem.ToString() ?? "").StartsWith("--");
        } // comboCheck_SelectedIndexChanged

        private void buttonRun_Click(object sender, EventArgs e)
        {
            ConsistencyCheck check;

            listViewResults.Items.Clear();

            var index = int.Parse(comboCheck.SelectedItem.ToString().Substring(0, 2));
            switch (index)
            {
                case 1: check = new ConsistencyCheckMissingServiceLogos(); break;
                case 2: check = new ConsistencyCheckMissingLogoFiles(); break;
                case 3: check = new ConsistencyCheckUnusedEntries(); break;
                case 4: check = new ConsistencyCheckUnusedFiles(); break;
                default:
                    return;
            } // switch

            check.ProgressChanged += Check_ProgressChanged;
            check.Run();
            ShowResults(check);
        } // buttonRun_Click

        private void LoadDisplayProgress(string text)
        {
            labelStatus.Text = text;
            labelStatus.GetCurrentParent().Refresh();
        } // LoadDisplayProgress

        private void Check_ProgressChanged(object sender, ConsistencyCheck.ProgressChangedEventArgs e)
        {
            LoadDisplayProgress(e.Message);
        } // Check_ProgressChanged

        private void ShowResults(ConsistencyCheck consistencyCheck)
        {
            int maxColumns = 0;

            listViewResults.BeginUpdate();
            listViewResults.Columns.Clear();

            foreach (var result in consistencyCheck.Results)
            {
                maxColumns = Math.Max(result.Data.Length, maxColumns);
            } // foreach result

            foreach (var result in consistencyCheck.Results)
            {
                var item = new ListViewItem(result.Data);
                item.ImageKey = result.Severity.ToString();
                for (int missing = 0; missing < maxColumns - result.Data.Length; missing++)
                {
                    item.SubItems.Add((string)null);
                } // for missing
                item.SubItems.Add((result.Timestamp - consistencyCheck.StartTime).ToString());
                listViewResults.Items.Add(item);
            } // foreach result

            for (int i = 0; i < maxColumns + 2; i++)
            {
                listViewResults.Columns.Add(i.ToString());
                listViewResults.Columns[i].AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
            } // for i
            listViewResults.EndUpdate();
        } // ShowResults
    } // class FormConsistency
} // namespace

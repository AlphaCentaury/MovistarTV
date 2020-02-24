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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace IpTviewr.UiServices.Discovery
{
    public partial class UiBroadcastDiscoveryMergeResultDetailsDialog : Form
    {
        public UiBroadcastDiscoveryMergeResultDetailsDialog()
        {
            InitializeComponent();
        } // constructor

        public UiBroadcastDiscoveryMergeResult MergeResult
        {
            get;
            set;
        } // MergeResult

        private void UiBroadcastDiscoveryMergeResultDetailsDialog_Load(object sender, EventArgs e)
        {
            if (MergeResult == null) return;

            var items = new List<ListViewItem>();
            AddServices(items, MergeResult.NewServices, listViewDetails.Groups["Added"]);
            AddServices(items, MergeResult.RemovedServices, listViewDetails.Groups["Removed"]);
            AddServices(items, MergeResult.ChangedServices, listViewDetails.Groups["Changed"]);

            if (items.Count > 0)
            {
                var q = from item in items
                        orderby item.Text
                        select item;

                listViewDetails.BeginUpdate();
                listViewDetails.Items.AddRange(q.ToArray());
                listViewDetails.EndUpdate();
            }
            else
            {
                listViewDetails.ShowGroups = false;
                listViewDetails.Items.Add(Properties.Texts.MergeResultDetailsNoData);
                buttonCopy.Enabled = false;
            } // if-else
        } // UiBroadcastDiscoveryMergeResultDetailsDialog_Load

        private void ButtonCopy_Click(object sender, EventArgs e)
        {
            var output = new StringBuilder();

            output.AppendLine("Display name\tService Name\tStatus");

            for (var index = 0; index < listViewDetails.Items.Count; index++)
            {
                var item = listViewDetails.Items[index];
                output.Append(item.SubItems[0].Text);
                output.Append('\t');
                output.Append(item.SubItems[1].Text);
                output.Append('\t');
                output.Append(item.Group.Name);
                output.AppendLine();
            } // for

            Clipboard.SetText(output.ToString());
        } // buttonCopy_Click

        private static void AddServices(List<ListViewItem> items, IList<UiBroadcastService> services, ListViewGroup group)
        {
            if ((services == null) || (services.Count == 0)) return;

            foreach(var service in services)
            {
                var item = new ListViewItem(service.DisplayName) {Group = @group};
                item.SubItems.Add(service.ServiceName);

                items.Add(item);
            } // foreach service
        } // AddServices
    } // class UiBroadcastDiscoveryMergeResultDetailsDialog
} // namespace

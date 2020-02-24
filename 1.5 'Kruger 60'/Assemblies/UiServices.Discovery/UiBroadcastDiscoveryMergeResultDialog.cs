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
using System.Windows.Forms;

namespace IpTviewr.UiServices.Discovery
{
    public partial class UiBroadcastDiscoveryMergeResultDialog : Form
    {
        protected UiBroadcastDiscoveryMergeResult MergeResult;

        public static UiBroadcastDiscoveryMergeResult Merge(IWin32Window owner, UiBroadcastDiscovery oldDiscovery, UiBroadcastDiscovery newDiscovery)
        {
            var result = UiBroadcastDiscovery.Merge(oldDiscovery, newDiscovery);

            if (oldDiscovery == null) return result;

            using (var dialog = new UiBroadcastDiscoveryMergeResultDialog())
            {
                dialog.MergeResult = result;
                dialog.ShowDialog(owner);
            } // using dialog

            return result;
        } // Merge

        public UiBroadcastDiscoveryMergeResultDialog()
        {
            InitializeComponent();
        } // constructor

        private void UiBroadcastDiscoveryMergeResultDialog_Load(object sender, EventArgs e)
        {
            Text = Owner.Text;

            if (MergeResult.IsEmpty)
            {
                pictureIconSuccess.Image = Properties.Resources.Status_Info_24x24;
                labelSuccess.Text = Properties.Texts.BroadcastListRefreshEmpty;
            }
            else
            {
                labelSuccess.Text = Properties.Texts.BroadcastListRefreshSuccess;
            } // if-else

            // New services
            if (MergeResult.NewServices.Count == 0)
            {
                labelAdded.Text = Properties.Texts.MergeResultZeroNewServices;
            }
            else
            {
                labelAdded.Text = string.Format(Properties.Texts.MergeResultNewServicesCount, MergeResult.NewServices.Count);
            } // if-else

            // Removed services
            if (MergeResult.RemovedServices.Count == 0)
            {
                labelRemoved.Text = Properties.Texts.MergeResultZeroRemovedServices;
            }
            else
            {
                labelRemoved.Text = string.Format(Properties.Texts.MergeResultRemovedServicesCount, MergeResult.RemovedServices.Count);
            } // if-else

            // Changed services
            if (MergeResult.ChangedServices.Count == 0)
            {
                labelChanged.Text = Properties.Texts.MergeResultZeroChangedServices;
            }
            else
            {
                labelChanged.Text = string.Format(Properties.Texts.MergeResultChangedServicesCount, MergeResult.ChangedServices.Count);
            } // if-else

            // Not changed
            if (MergeResult.CountNotChanged == 0)
            {
                labelEqual.Text = Properties.Texts.MergeResultZeroEqualServices;
            }
            else
            {
                if (MergeResult.CountNotChanged > 0)
                {
                    labelEqual.Text = string.Format(Properties.Texts.MergeResultEqualServicesCount, MergeResult.CountNotChanged);
                }
                else
                {
                    labelBulletEqual.Visible = false;
                    labelEqual.Visible = false;
                } // if-else
            } // if-else
        } // UiBroadcastDiscoveryMergeResultDialog_Load

        private void ButtonDetails_Click(object sender, EventArgs e)
        {
            using (var dlg = new UiBroadcastDiscoveryMergeResultDetailsDialog())
            {
                dlg.MergeResult = MergeResult;
                dlg.ShowDialog(this);
            } // using dlg
        } // buttonDetails_Click
    } // class UiBroadcastDiscoveryMergeResultDialog
} // namespace

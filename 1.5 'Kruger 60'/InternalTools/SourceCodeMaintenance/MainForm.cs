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
using System.Reflection;
using System.Windows.Forms;
using AlphaCentaury.Tools.SourceCodeMaintenance;
using IpTviewr.UiServices.Common.Forms;

namespace AlphaCentaury.Tools.SourceCodeMaintenance
{
    public partial class MainForm : CommonBaseForm
    {
        public MainForm()
        {
            InitializeComponent();
        } // constructor

        #region Overrides of Form

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            newStripMenuItem.PerformClick();
        } // OnShow

        #endregion

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using var form = new SelectToolDialog();
            if (form.ShowDialog(this) != DialogResult.OK) return;
            var selected = form.SelectedTool;
            SafeCall(() =>
            {
                var form = selected.Value.GetUi();
                form.MdiParent = this;
                form.Show();
            });
        } // newToolStripMenuItem_Click

        private void cascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        } // cascadeToolStripMenuItem_Click

        private void tileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        } // tileHorizontalToolStripMenuItem_Click

        private void tileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        } // tileVerticalToolStripMenuItem_Click

        private void arrangeIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.ArrangeIcons);
        } // arrangeIconsToolStripMenuItem_Click

        private void closeAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (var mdiChild in MdiChildren)
            {
                mdiChild.Close();
            } // foreach
        } // closeAllToolStripMenuItem_Click

        private void windowsToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            var enabled = (MdiChildren.Length != 0);
            arrangeToolStripMenuItem.Enabled = enabled;
            closeAllToolStripMenuItem.Enabled = enabled;
        } // windowsToolStripMenuItem_DropDownOpening

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using var box = new AboutBox
            {
                ApplicationData = new AboutBoxApplicationData
                {
                    Name = Assembly.GetEntryAssembly()?.GetCustomAttribute<AssemblyTitleAttribute>()?.Title
                }
            };

            box.ShowDialog(this);
        } // aboutToolStripMenuItem_Click
    } // class MainForm
} // namespace

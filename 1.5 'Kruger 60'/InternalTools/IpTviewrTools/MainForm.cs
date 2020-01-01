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
using ComponentFactory.Krypton.Toolkit;
using IpTviewr.Internal.Tools.Properties;
using IpTviewr.Internal.Tools.UiFramework;

namespace IpTviewr.Internal.Tools
{
    public partial class MainForm : MdiRibbonForm
    {
        public MainForm()
        {
            InitializeComponent();
            Icon = Resources.IpTViewrGuiTools;
            RibbonAppButtonImage = Resources.IpTViewrGuiTools_32x;
        }

        #region Overrides of MdiRibbonForm

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            if (!CreateNewToolWindow())
            {
                Close();
            } // if
        } // OnShown

        protected override Form CreateNewMdiChild()
        {
            using var dlg = new SelectToolDialog
            {
                OkButtonImage = Resources.Action_Ok_16x16,
                CancelButtonImage = Resources.Action_Cancel_16x16,
            };

            if (dlg.ShowDialog(this) != DialogResult.OK) return null;

            return dlg.SelectedGuiTool?.Value.CreateForm();
        } // CreateNewMdiChild

        #endregion
    }
}

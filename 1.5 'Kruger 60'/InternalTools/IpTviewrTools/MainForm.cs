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
using System.Threading.Tasks;
using System.Windows.Forms;
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
        } // constructor

        #region Overrides of MdiRibbonForm

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

        private async void MainForm_Shown(object sender, EventArgs e)
        {
            EnableRibbon(false);
            ((IRibbonMdiForm)this).SetStatusText("Loading tools metadata...");
            var ex = await Task.Run(() =>
            {
                try
                {
                    _ = ToolsContainer.Current;
                    return null;
                }
                catch (Exception e)
                {
                    return e;
                } // try-catch
            });

            if (ex != null)
            {
                if (ex.InnerException != null) ex = ex.InnerException;

                MessageBox.Show(this, $"Unable to load tools list:\n{ex.Message}\n\n{ex.GetType().Name}", Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
            } // if
            else
            {
                if (!CreateNewToolWindow())
                {
                    Close();
                } // if
            } // if-else

            EnableRibbon(true);
        } // MainForm_Shown
    } // class MainForm
} // namespace

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
using System.Drawing;
using System.Windows.Forms;

namespace AlphaCentaury.WindowsForms.MsgBoxEx
{
    internal sealed partial class MsgBoxExForm : Form
    {
        public MsgBoxExForm()
        {
            InitializeComponent();
        } // public

        public MsgBoxExForm(MsgBoxExContents msgBoxContents)
        {
            InitializeComponent();

            BoxLayout = new BoxExLayout(this, msgBoxContents);
            Contents = new BoxExContents(this, msgBoxContents);

            SuspendLayout();

            Contents.FillMissing();
            Contents.Apply();

            BoxLayout.PerformLayout();

            ResumeLayout(false);
            PerformLayout();

            if (msgBoxContents.Owner == null)
            {
                StartPosition = FormStartPosition.CenterScreen;
                ShowInTaskbar = true;
            } // if

            BoxLayout = null;
        } // constructor

        #region Form events

        private void MsgBoxExForm_Load(object sender, EventArgs e)
        {
            // cosmetic adjustments
            labelAdditionalInfo.Font = new Font(labelAdditionalInfo.Font, FontStyle.Bold);
            if (iconDialog.Image != null)
            {
                iconDialog.Paint += IconDialog_Paint;
            } // if
        } // MsgBoxExForm_Load

        private void MsgBoxExForm_Shown(object sender, EventArgs e)
        {
            Contents.SetDefaultButton();
            Contents = null;
        } // MsgBoxExForm_Shown

        #endregion

        #region Events

        private void buttonCopy_Click(object sender, EventArgs e)
        {

        } // buttonCopy_Click

        private void buttonDetails_Click(object sender, EventArgs e)
        {
        } // buttonDetails_Click

        private void IconDialog_Paint(object sender, PaintEventArgs e)
        {
            if (iconDialog.Image == null) return;

            e.Graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            e.Graphics.FillRectangle(SystemBrushes.Window, iconDialog.DisplayRectangle);
            e.Graphics.DrawImage(iconDialog.Image, iconDialog.DisplayRectangle, new Rectangle(0, 0, 96, 96), GraphicsUnit.Pixel);
        } // IconDialog_Paint

        #endregion
    }
}

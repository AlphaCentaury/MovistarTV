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
    internal sealed partial class OldMsgBoxExForm : Form
    {
        private const int MaxLinesText = 4;
        private const int MaxLinesException = 3;
        private const int MaxLinesInnerException = 3;
        private const int MaxInnerExceptions = 4;

        public OldMsgBoxExForm()
        {
            InitializeComponent();
        } // constructor

        public MsgBoxExContents Contents
        {
            get;
            set;
        } // Contents

        #region FormEvents

        private void Form_Load(object sender, EventArgs e)
        {
            SuspendLayout();

            CalcLineSize();

            // cosmetic adjustments
            labelAdditionalInfo.Font = new Font(labelAdditionalInfo.Font, FontStyle.Bold);
            if (iconDialog.Image != null)
            {
                iconDialog.Paint += IconDialog_Paint;
            } // if

            ResumeLayout(false);
            PerformLayout();

            CenterToParent();
        } // Form_Load

        private void Form_Shown(object sender, EventArgs e)
        {
            StartPosition = FormStartPosition.CenterScreen;
        } // Form_Shown
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
            e.Graphics.FillRectangle(SystemBrushes.Control, iconDialog.DisplayRectangle);
            e.Graphics.DrawImage(iconDialog.Image, iconDialog.DisplayRectangle, new Rectangle(0,0,96,96), GraphicsUnit.Pixel);
        } // IconDialog_Paint

        #endregion


        private void CalcLineSize()
        {
            labelText.Text = "1⁰ ‡§" + Environment.NewLine +
                "2 åÅĀā" + Environment.NewLine +
                "3⁰ ‡§" + Environment.NewLine +
                "4 åÅĀā";
            //Box.LineHeight = labelText.Height / 4;

            //labelText.MaximumSize = new Size(0, Box.LineHeight * MaxLinesText);
            //labelException1.MaximumSize = new Size(0, Box.LineHeight * MaxLinesException);
        } // private
    } // sealed class MsgBoxExForm : Form
} // namespace

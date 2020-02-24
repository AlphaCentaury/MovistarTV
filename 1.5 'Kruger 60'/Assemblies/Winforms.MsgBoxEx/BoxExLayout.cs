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
    internal partial class MsgBoxExForm
    {
        private BoxExLayout BoxLayout;

        private sealed class BoxExLayout
        {
            private MsgBoxExForm Form;
            private MsgBoxExContents BoxExContents;
            private Button[] DlgButtons;
            private Size NonClientSize;
            private int LineHeight;
            private bool IsMono;

            private const int MaxDialogWidth = 600;

            private const int MinTextLines = 3;

            private const int DefaultTextLines = 15;
            private const int DefaultExceptionLines = 3;
            private const int DefaultInnerExceptionLines = 2;

            private const int MaxTextLines = 30;
            private const int MaxExceptionLines = 7;
            private const int MaxInnerExceptionLines = 5;

            public BoxExLayout(MsgBoxExForm form, MsgBoxExContents contents)
            {
                Form = form;
                BoxExContents = contents;
                DlgButtons = new Button[] { Form.buttonDlg1, Form.buttonDlg2, Form.buttonDlg3, Form.buttonDlg4, Form.buttonDlg5 };
            } // constructor

            public void PerformLayout()
            {
                IsMono = Type.GetType("Mono.Runtime") != null;
                NonClientSize = Form.Size - Form.ClientSize;
                CalcLineHeight();
                SizeBottomPanel();
                SizeTopPanel();
            } // PerformLayout

            private void SizeTopPanel()
            {
                PositionContentsPanel();
                SetLabelsMaxSizes();
                AdjustLabelTextSize();
                PositionContentsPanel();
            } // SizeTopPanel

            private void PositionContentsPanel()
            {
                ArrangeLinearHorizontal(Form.iconDialog, Form.panelContents);

                if (Form.panelAdditional.IsDisposed)
                {
                    ArrangeLinearVertical(Form.labelText);
                    return;
                } // if

                ArrangeLinearVertical(Form.labelText, Form.panelAdditional);

                ArrangeLinearVertical(Form.labelAdditionalInfo, Form.panelDetails1);

                ArrangeLinearHorizontal(Form.iconArrow1, Form.panelException1);
                ArrangeLinearVertical(Form.labelException1, Form.panelDetails2);

                if (Form.panelDetails2.IsDisposed) return;

                ArrangeLinearHorizontal(Form.iconArrow2, Form.panelException2);
                ArrangeLinearVertical(Form.labelException2, Form.panelDetails3);

                if (Form.panelDetails3.IsDisposed) return;

                ArrangeLinearHorizontal(Form.iconArrow3, Form.panelException3);
                ArrangeLinearVertical(Form.labelException3, Form.panelDetails4);

                if (Form.panelDetails4.IsDisposed) return;

                ArrangeLinearHorizontal(Form.iconArrow4, Form.panelException4);
                ArrangeLinearVertical(Form.labelException4, Form.panelDetails5);

                if (Form.panelDetails5.IsDisposed) return;

                ArrangeLinearHorizontal(Form.iconArrow5, Form.labelException5);
            } // PositionContentsPanel

            private void SetLabelsMaxSizes()
            {
                Point[] panelLocation;
                Control[] label;
                int[] maxLines;
                Point p;
                int maxTextLines, maxExceptionLines, maxInnerExceptionLines;

                maxTextLines = MinMaxDefault(BoxExContents.MaxTextLines, DefaultTextLines, MaxInnerExceptionLines);
                maxExceptionLines = MinMaxDefault(BoxExContents.MaxExceptionLines, DefaultExceptionLines, MaxExceptionLines);
                maxInnerExceptionLines = MinMaxDefault(BoxExContents.MaxInnerExceptionLines, DefaultInnerExceptionLines, MaxInnerExceptionLines);

                panelLocation = new Point[6];
                label = new Control[] { Form.labelText, Form.labelException1, Form.labelException2, Form.labelException3, Form.labelException4, Form.labelException5 };
                maxLines = new int[] { maxTextLines, maxExceptionLines, maxInnerExceptionLines, maxInnerExceptionLines, maxInnerExceptionLines, maxInnerExceptionLines };

                p = Form.panelTop.Location;
                p.Offset(Form.panelContents.Location);
                panelLocation[0] = p;

                p.Offset(Form.panelAdditional.Location);
                p.Offset(Form.panelDetails1.Location);
                p.Offset(Form.panelException1.Location);
                panelLocation[1] = p;

                p.Offset(Form.panelDetails2.Location);
                p.Offset(Form.panelException2.Location);
                panelLocation[2] = p;

                p.Offset(Form.panelDetails3.Location);
                p.Offset(Form.panelException3.Location);
                panelLocation[3] = p;

                p.Offset(Form.panelDetails4.Location);
                p.Offset(Form.panelException4.Location);
                panelLocation[4] = p;

                p.Offset(Form.panelDetails5.Location);
                panelLocation[5] = p;

                var maxWidth = Math.Max(MaxDialogWidth, Form.panelBottom.Width);
                maxWidth -= Form.panelTop.Margin.Right + Form.panelContents.Margin.Right;
                maxWidth -= NonClientSize.Width;

                for (var index = 0; index < panelLocation.Length; index++)
                {
                    p = panelLocation[index];
                    p.Offset(label[index].Location);

                    label[index].MaximumSize = new Size(maxWidth - p.X, LineHeight * maxLines[index]);
                    label[index].Size = GetPreferredSize(label[index]);
                } // for index
            } // SetLabelsMaxSizes

            private void AdjustLabelTextSize()
            {
                if ((Form.panelAdditional.IsDisposed) && Form.labelText.Height < Form.iconDialog.Height)
                {
                    Form.labelText.TextAlign = ContentAlignment.MiddleLeft;
                    Form.labelText.MinimumSize = new Size(0, Form.iconDialog.Height);
                    Form.labelText.Size = new Size(Form.labelText.Size.Width, Math.Min(Form.labelText.Size.Height, Form.iconDialog.Height));
                    Form.Text = Form.labelText.Size.ToString();
                    return;
                } // if

                // TODO: reduce lines if there's additional info
                // to avoid having a huge dialog. A minumum ammount of
                // lines is guaranteed (MinTextLines)
            } // AdjustLabelTextSize

            private void SizeBottomPanel()
            {
                SizeControlButtonsPanel();
                SizeDialogButtonsPanel();

                var size = new Size();
                size.Width = Form.panelBottom.Padding.Left +
                    Form.panelControlButtons.Width + Form.panelDialogButtons.Width
                    + Form.panelBottom.Padding.Right;
                size.Height = Math.Max(Form.panelControlButtons.Height, Form.panelDialogButtons.Height);

                Form.panelBottom.Size = MaxSize(size, Form.panelBottom.Size);
                Form.panelBottom.MinimumSize = size;

                Form.MinimumSize = new Size(size.Width + NonClientSize.Width, Form.MinimumSize.Height);
                Form.Size = MaxSize(Form.Size, Form.MinimumSize);
            } // SizeBottomPanel

            private void SizeControlButtonsPanel()
            {
                var buttons = new Button[] { Form.buttonCopy, Form.buttonDetails };

                AutoSizeButtons(Form.buttonCopy.MinimumSize, buttons);

                var size = ArrangeLinearHorizontal(buttons);
                Form.panelControlButtons.Size = MaxSize(size, Form.panelControlButtons.Size);
                Form.panelControlButtons.MinimumSize = size;
            } // SizeControlButtonsPanel

            private void SizeDialogButtonsPanel()
            {
                AutoSizeButtons(Form.buttonDlg1.MinimumSize, DlgButtons);

                var size = ArrangeLinearHorizontal(DlgButtons);
                Form.panelDialogButtons.Size = MaxSize(size, Form.panelDialogButtons.Size);
                Form.panelDialogButtons.MinimumSize = size;
            } // CalcDialogButtonsPanel

            private void AutoSizeButtons(Size minSize, params Button[] controls)
            {
                var trick = new Size(1, 0);

                foreach (var button in controls)
                {
                    if (button.IsDisposed) continue;

                    button.MinimumSize = minSize;

                    if (button.AutoEllipsis)
                    {
                        button.Size = GetPreferredSize(button) + trick;
                        button.Size = button.Size - trick;
                    }
                    else
                    {
                        button.Size = GetPreferredSize(button);
                    } // if-else AutoEllipsis
                } // foreach button
            } // AutosizeButtons

            private Size ArrangeLinearHorizontal(params Control[] controls)
            {
                var width = 0;
                var height = 0;

                foreach (var control in controls)
                {
                    if (control.IsDisposed) continue;

                    control.Left = control.Margin.Left + width;
                    control.Top = control.Margin.Top;

                    width += control.Margin.Left + control.Width + control.Margin.Right;
                    height = Math.Max(height, control.Margin.Top + control.Height + control.Margin.Bottom);
                } // foreach

                return new Size(width, height);
            } // ArrangeLinearHorizontal

            private Size ArrangeLinearVertical(params Control[] controls)
            {
                var width = 0;
                var height = 0;

                foreach (var control in controls)
                {
                    if (control.IsDisposed) continue;

                    control.Left = control.Margin.Left;
                    control.Top = height + control.Margin.Top;

                    width = Math.Max(width, control.Margin.Left + control.Width + control.Margin.Right);
                    height += control.Margin.Top + control.Height + control.Margin.Bottom;
                } // foreach

                return new Size(width, height);
            } // ArrangeLinearVertical

            private void CalcLineHeight()
            {
                var current = Form.labelText.Text;
                var x = Form.Handle;
                Form.labelText.Text = "1⁰ ‡§ Lorem Ipsum Dolor" + Environment.NewLine +
                    "2 åÅĀā" + Environment.NewLine +
                    "3⁰ ‡§" + Environment.NewLine +
                    "4 åÅĀā";
                LineHeight = GetPreferredSize(Form.labelText).Height / 4;
                Form.labelText.Text = current;
            } // CalcLineHeight

            private int MinMaxDefault(int value, int defaultValue, int maxValue)
            {
                if (value <= 0) return defaultValue;
                return Math.Min(value, maxValue);
            } // MinMaxDefault

            private Size GetPreferredSize(Control control)
            {
                if (!IsMono)
                {
                    return control.PreferredSize;
                }
                else
                {
                    var maxSize = MaxSize(new Size(int.MaxValue, int.MaxValue), control.MaximumSize);
                    var size = TextRenderer.MeasureText(control.Text, control.Font, maxSize, TextFormatFlags.NoPrefix | TextFormatFlags.WordBreak);
                    if (control is Button)
                    {
                        size += new Size(6, 6);
                    } // if
                    size = MinSize(size, control.MinimumSize);
                    size = MaxSize(size, control.MaximumSize);

                    return size;
                } // if-else
            } // GetPreferredSize

            private Size MinSize(Size size, Size minSize)
            {
                return new Size(Math.Max(size.Width, minSize.Width), Math.Max(size.Height, minSize.Height));
            } // MinSize

            private Size MaxSize(Size size, Size maxSize)
            {
                var width = maxSize.Width <= 0 ? size.Width : Math.Min(size.Width, maxSize.Width);
                var height = maxSize.Height <= 0 ? size.Height : Math.Min(size.Height, maxSize.Height);
                return new Size(width, height);
            } // MaxSize
        } // sealed class BoxExLayout
    } // partial class MsgBoxExForm
} // namespace

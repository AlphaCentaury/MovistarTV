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
using System.Reflection;
using System.Windows.Forms;

namespace AlphaCentaury.WindowsForms.MsgBoxEx
{
    internal partial class MsgBoxExForm
    {
        private BoxExContents Contents;

        private sealed class BoxExContents
        {
            private MsgBoxExForm Form;
            private MsgBoxExContents Contents;
            private string Caption;
            private MsgBoxSeverity Severity;
            private string Text;
            private string Additional;
            private Exception Exception;

            public BoxExContents(MsgBoxExForm form, MsgBoxExContents contents)
            {
                Form = form;
                Contents = contents;
            } // constructor

            public void FillMissing()
            {
                if (Contents.Caption != null)
                {
                    Caption = Contents.Caption;
                }
                else
                {
                    var owner = Form.Owner as Form;
                    if (owner != null)
                    {
                        Caption = owner.Text;
                    }
                    else
                    {
                        var entry = Assembly.GetEntryAssembly();
                        var assemblyTitle = entry.GetCustomAttributes(typeof(AssemblyTitleAttribute), true);
                        var firstTitle = assemblyTitle?[0] as AssemblyTitleAttribute;
                        Caption = (firstTitle != null) ? firstTitle.Title : entry.GetName().Name;
                    } // if-else
                } // if !Caption

                Text = Contents.Text;
                Additional = Contents.AdditionalInformation;
                Exception = Contents.Exception;

                if (Text == null)
                {
                    if (Contents.AdditionalInformation != null)
                    {
                        Text = Contents.AdditionalInformation;
                        Additional = null;
                    }
                    else if (Contents.Exception != null)
                    {
                        Text = Contents.Exception.Message;
                        Exception = Contents.Exception?.InnerException;
                    } // if-else
                } // if !Box.Text

                if (Additional == null)
                {
                    if (Exception != null)
                    {
                        Additional = Exception.Message;
                        Exception = Exception?.InnerException;
                    } // if
                } // if !Box.Additional

                if (Contents.Severity == MsgBoxSeverity.Auto)
                {
                    if (Contents.Exception != null)
                    {
                        Severity = MsgBoxSeverity.Exception;
                    } // if
                    else
                    {
                        Severity = MsgBoxSeverity.Informational;
                    } // if-else
                }
                else
                {
                    Severity = Contents.Severity;
                } // if-else
            } // FillMissing

            #region Apply()
            public void Apply()
            {
                SetSeverity();

                Form.Text = Caption;
                Form.labelText.Text = Text;

                SetExceptionDetails();

                SetButtons();
            } // Apply

            private void SetSeverity()
            {
                switch (Severity)
                {
                    case MsgBoxSeverity.None:
                        RemoveControl(Form.iconDialog);
                        break;

                    case MsgBoxSeverity.Verbose:
                        Form.iconDialog.Image = (Form.iconDialog.Width <= 96) ? Properties.Resources.IconVerbose96 : Properties.Resources.IconVerbose512;
                        break;
                    case MsgBoxSeverity.Informational:
                        Form.iconDialog.Image = (Form.iconDialog.Width <= 96) ? Properties.Resources.IconInfo96 : Properties.Resources.IconInfo512;
                        break;
                    case MsgBoxSeverity.Warning:
                        Form.iconDialog.Image = (Form.iconDialog.Width <= 96) ? Properties.Resources.IconWarning96 : Properties.Resources.IconWarning512;
                        break;
                    case MsgBoxSeverity.Error:
                        Form.iconDialog.Image = (Form.iconDialog.Width <= 96) ? Properties.Resources.IconError96 : Properties.Resources.IconError512;
                        break;
                    case MsgBoxSeverity.Exception:
                        Form.iconDialog.Image = (Form.iconDialog.Width <= 96) ? Properties.Resources.IconException96 : Properties.Resources.IconException512;
                        break;
                } // switch
            } // SetSeverity

            private void SetExceptionDetails()
            {
                Exception exception;
                var texts = new string[5];
                var panels = new Control[] { Form.panelAdditional, Form.panelDetails2, Form.panelDetails3, Form.panelDetails4, Form.panelDetails5 };
                var labels = new Control[] { Form.labelException1, Form.labelException2, Form.labelException3, Form.labelException4, Form.labelException5 };
                var index = 0;

                if (Additional != null)
                {
                    texts[index++] = Additional;
                } // if

                exception = Exception;
                while ((exception != null) && (index < 5))
                {
                    texts[index++] = exception.Message;
                    exception = exception.InnerException;
                } // while

                for (index = 0; index < 5; index++)
                {
                    var text = texts[index];
                    if (text == null)
                    {
                        RemoveControl(panels[index]);
                        break;
                    } // if

                    labels[index].Text = text;
                } // for index

            } // SetExceptionDetails

            private void SetButtons()
            {
                Form.buttonCopy.Text = Properties.Resources.ButtonCopy;
                Form.buttonDetails.Text = Properties.Resources.ButtonDetails;

                switch (Contents.Buttons)
                {
                    case MsgBoxExButtons.OK:
                        SetButtonsText(Properties.Resources.ButtonOK);
                        Form.AcceptButton = Form.buttonDlg1;
                        Form.buttonDlg1.DialogResult = DialogResult.OK;
                        break;

                    case MsgBoxExButtons.OKCancel:
                        SetButtonsText(Properties.Resources.ButtonOK, Properties.Resources.ButtonCancel);
                        Form.AcceptButton = Form.buttonDlg1;
                        Form.CancelButton = Form.buttonDlg2;
                        Form.buttonDlg1.DialogResult = DialogResult.OK;
                        Form.buttonDlg2.DialogResult = DialogResult.Cancel;
                        break;

                    case MsgBoxExButtons.YesNo:
                        SetButtonsText(Properties.Resources.ButtonYes, Properties.Resources.ButtonNo);
                        Form.AcceptButton = Form.buttonDlg1;
                        Form.CancelButton = Form.buttonDlg2;
                        Form.buttonDlg1.DialogResult = DialogResult.Yes;
                        Form.buttonDlg2.DialogResult = DialogResult.No;
                        break;

                    case MsgBoxExButtons.RetryCancel:
                        SetButtonsText(Properties.Resources.ButtonRetry, Properties.Resources.ButtonCancel);
                        Form.AcceptButton = Form.buttonDlg1;
                        Form.CancelButton = Form.buttonDlg2;
                        Form.buttonDlg1.DialogResult = DialogResult.Retry;
                        Form.buttonDlg2.DialogResult = DialogResult.Cancel;
                        break;

                    case MsgBoxExButtons.YesNoCancel:
                        SetButtonsText(Properties.Resources.ButtonYes, Properties.Resources.ButtonNo, Properties.Resources.ButtonCancel);
                        Form.AcceptButton = Form.buttonDlg1;
                        Form.CancelButton = Form.buttonDlg2;
                        Form.buttonDlg1.DialogResult = DialogResult.Yes;
                        Form.buttonDlg2.DialogResult = DialogResult.No;
                        Form.buttonDlg3.DialogResult = DialogResult.Cancel;
                        break;

                    case MsgBoxExButtons.AbortRetryIgnore:
                        SetButtonsText(Properties.Resources.ButtonAbort, Properties.Resources.ButtonRetry, Properties.Resources.ButtonIgnore);
                        Form.AcceptButton = Form.buttonDlg1;
                        Form.CancelButton = Form.buttonDlg3;
                        Form.buttonDlg1.DialogResult = DialogResult.Abort;
                        Form.buttonDlg2.DialogResult = DialogResult.Retry;
                        Form.buttonDlg3.DialogResult = DialogResult.Ignore;
                        break;

                    case MsgBoxExButtons.Custom:
                        SetCustomButtons();
                        break;
                } // switch Buttons
            } // SetButtons

            private void SetCustomButtons()
            {
                if ((Contents.CustomButtons == null) || (Contents.CustomButtons.Length == 0))
                {
                    SetButtonsText(Properties.Resources.ButtonOK);
                    Form.AcceptButton = Form.buttonDlg1;
                    Form.buttonDlg1.DialogResult = DialogResult.OK;
                }
                else
                {
                    SetButtonsText(Contents.CustomButtons);
                    Form.AcceptButton = GetButton(Contents.CustomAcceptButton);
                    Form.CancelButton = GetButton(Contents.CustomCancelButton);

                    Form.buttonDlg1.DialogResult = Contents.CustomResults?[0] ?? DialogResult.None;
                    Form.buttonDlg2.DialogResult = Contents.CustomResults?[1] ?? DialogResult.None;
                    Form.buttonDlg3.DialogResult = Contents.CustomResults?[2] ?? DialogResult.None;
                    Form.buttonDlg4.DialogResult = Contents.CustomResults?[3] ?? DialogResult.None;
                    Form.buttonDlg5.DialogResult = Contents.CustomResults?[4] ?? DialogResult.None;
                } // if-else
            } // SetCustomButtons

            public void SetDefaultButton()
            {
                Button control = null;

                switch (Contents.DefaultButton)
                {
                    case MsgBoxExButton.Button1:
                        control = Form.buttonDlg1;
                        break;
                    case MsgBoxExButton.Button2:
                        control = Form.buttonDlg2;
                        break;
                    case MsgBoxExButton.Button3:
                        control = Form.buttonDlg3;
                        break;
                    case MsgBoxExButton.Button4:
                        control = Form.buttonDlg4;
                        break;
                    case MsgBoxExButton.Button5:
                        control = Form.buttonDlg5;
                        break;
                } // switch

                if (control != null)
                {
                    control.Focus();
                    Form.AcceptButton = control;
                } // if
            } // SetDefaultButton
            #endregion

            private Button GetButton(MsgBoxExButton button)
            {
                switch (button)
                {
                    case MsgBoxExButton.Button1: return Form.buttonDlg1;
                    case MsgBoxExButton.Button2: return Form.buttonDlg2;
                    case MsgBoxExButton.Button3: return Form.buttonDlg3;
                    case MsgBoxExButton.Button4: return Form.buttonDlg4;
                    case MsgBoxExButton.Button5: return Form.buttonDlg5;
                    default:
                        return null;
                } // switch
            } // GetButton

            private void SetButtonsText(params string[] buttons)
            {
                SetButtonText(Form.buttonDlg1, buttons.Length > 0 ? buttons[0] : null);
                SetButtonText(Form.buttonDlg2, buttons.Length > 1 ? buttons[1] : null);
                SetButtonText(Form.buttonDlg3, buttons.Length > 2 ? buttons[2] : null);
                SetButtonText(Form.buttonDlg4, buttons.Length > 3 ? buttons[3] : null);
                SetButtonText(Form.buttonDlg5, buttons.Length > 4 ? buttons[4] : null);
            } // SetButtonsText

            private void SetButtonText(Button button, string text)
            {
                if (text == null)
                {
                    RemoveControl(button);
                }
                else
                {
                    button.MinimumSize = Form.buttonDlg1.MinimumSize;
                    button.Size = Size.Empty;
                    button.Text = text;
                } // if-else
            } // SetButtonText

            private void RemoveControl(Control control)
            {
                control.Parent.Controls.Remove(control);
                control.Dispose();
            } // RemoveControl
        } // internal sealed class MsgBoxExData
    } // partial class MsgBoxExForm
} // namespace

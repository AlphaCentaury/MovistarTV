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

namespace AlphaCentaury.WindowsForms.MsgBoxEx
{
    public static class MessageBoxEx
    {
        #region System.Windows.Forms.MessageBox compatibility (no owner)

        public static DialogResult Show(string text)
        {
            var contents = new MsgBoxExContents(text);
            return Show(contents);
        } // Show: string

        public static DialogResult Show(string text, string caption)
        {
            var contents = new MsgBoxExContents(text, caption);
            return Show(contents);
        } // Show: string, string

        public static DialogResult Show(string text, string caption, MessageBoxButtons buttons)
        {
            var contents = new MsgBoxExContents(text, caption, ToExEnum(buttons));
            return Show(contents);
        } // Show: string, string, MessageBoxButtons

        public static DialogResult Show(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            var contents = new MsgBoxExContents(text, caption, ToExEnum(buttons), ToExSeverity(icon));
            return Show(contents);
        } // Show: string, string, MessageBoxButtons, MessageBoxIcon

        public static DialogResult Show(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton)
        {
            var contents = new MsgBoxExContents(text, caption, ToExEnum(buttons), ToExSeverity(icon), ToExEnum(defaultButton));
            return Show(contents);
        } // Show: string, string, MessageBoxButtons, MessageBoxIcon, MessageBoxDefaultButton

        #endregion

        #region System.Windows.Forms.MessageBox compatibility (with owner)

        public static DialogResult Show(IWin32Window owner, string text)
        {
            var contents = new MsgBoxExContents(text);
            return Show(owner, contents);
        } // Show: IWin32Window, string

        public static DialogResult Show(IWin32Window owner, string text, string caption)
        {
            var contents = new MsgBoxExContents(text, caption);
            return Show(owner, contents);
        } // Show: IWin32Window, string, string

        public static DialogResult Show(IWin32Window owner, string text, string caption, MessageBoxButtons buttons)
        {
            var contents = new MsgBoxExContents(text, caption, ToExEnum(buttons));
            return Show(owner, contents);
        } // Show: IWin32Window, string, string, MessageBoxButtons

        public static DialogResult Show(IWin32Window owner, string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            var contents = new MsgBoxExContents(text, caption, ToExEnum(buttons), ToExSeverity(icon));
            return Show(owner, contents);
        } // Show: IWin32Window, string, string, MessageBoxButtons, MessageBoxIcon

        public static DialogResult Show(IWin32Window owner, string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton)
        {
            var contents = new MsgBoxExContents(text, caption, ToExEnum(buttons), ToExSeverity(icon), ToExEnum(defaultButton));
            return Show(owner, contents);
        } // Show: IWin32Window, string, string, MessageBoxButtons, MessageBoxIcon, MessageBoxDefaultButton

        #endregion

        public static DialogResult Show(Exception ex)
        {
            return Show((IWin32Window)null, ex);
        } // Show: Exception

        public static DialogResult Show(string text, Exception ex)
        {
            return Show(null, text, ex);
        } // Show: Exception

        public static DialogResult Show(IWin32Window owner, Exception ex)
        {
            var contents = new MsgBoxExContents(ex);
            return Show(owner, contents);
        } // Show: IWin32Owner, Exception

        public static DialogResult Show(IWin32Window owner, string text, Exception ex)
        {
            var contents = new MsgBoxExContents(ex)
            {
                Text = text,
            };
            return Show(owner, contents);
        } // Show: IWin32Owner, string, Exception

        public static DialogResult Show(MsgBoxExContents contents)
        {
            return Show(null, contents);
        } // Show: MessageBoxContents

        public static DialogResult Show(IWin32Window owner, MsgBoxExContents contents)
        {
            if (contents == null) throw new ArgumentNullException(nameof(contents));

            using (var box = new MsgBoxExForm(contents))
            {
                contents.Owner = owner;
                return box.ShowDialog(owner);
            } // using
        } // Show: IWin32Window, MessageBoxContents

        #region Convert MessageBox constants to MsgBoxEx constants
        private static MsgBoxExButtons ToExEnum(MessageBoxButtons buttons)
        {
            return (MsgBoxExButtons)buttons;
        } // ToExEnum

        private static MsgBoxExButton ToExEnum(MessageBoxDefaultButton button)
        {
            return (MsgBoxExButton)button;
        } // ToMessageBoxExEnum

        private static MsgBoxSeverity ToExSeverity(MessageBoxIcon icon)
        {
            switch (icon)
            {
                case MessageBoxIcon.Information: // MessageBoxIcon.Asterisk
                    return MsgBoxSeverity.Informational;
                case MessageBoxIcon.Warning: //MessageBoxIcon.Exclamation
                    return MsgBoxSeverity.Warning;
                case MessageBoxIcon.Error: //MessageBoxIcon.Hand, MessageBoxIcon.Stop
                    return MsgBoxSeverity.Error;
                case MessageBoxIcon.Question:
                    return MsgBoxSeverity.Question;
                default:
                    return MsgBoxSeverity.None;
            } // switch
        } // toExSeverity
        #endregion
    } // static class MessageBoxEx
} // namespace

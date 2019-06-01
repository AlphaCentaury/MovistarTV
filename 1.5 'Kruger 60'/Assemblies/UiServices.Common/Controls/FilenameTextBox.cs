// Copyright (C) 2014-2019, GitHub/Codeplex user AlphaCentaury
// 
// All rights reserved, except those granted by the governing license of this software.
// See 'license.txt' file in the project root for complete license information.
// 
// http://www.alphacentaury.org/movistartv https://github.com/AlphaCentaury

using IpTviewr.Common;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace IpTviewr.UiServices.Common.Controls
{
    [ToolboxBitmap(typeof(TextBox))]
    public class FilenameTextBox : TextBox
    {
        private char[] InvalidCharacters;
        private string InvalidCharacterText;
        private bool ManualUpdateOfValue;

        public FilenameTextBox()
        {
            InvalidCharacters = TextUtils.GetFilenameInvalidChars();
        } // constructor

        /// <summary>
        /// Sets the text and removes invalid characters if needed
        /// </summary>
        /// <param name="text">Text to set</param>
        public void SetText(string text, bool raiseTextChangedEvent)
        {
            ManualUpdateOfValue = true;
            this.Text = text;
            RemoveInvalidChars(false);
            ManualUpdateOfValue = false;

            if (raiseTextChangedEvent)
            {
                base.OnTextChanged(EventArgs.Empty);
            } // if
        } // SetText

        protected override void OnTextChanged(EventArgs e)
        {
            if (ManualUpdateOfValue) return;
            if (RemoveInvalidChars(true)) return;

            base.OnTextChanged(e);
        } // OnTextChanged

        private void DisplayInvalidCharacterWarning()
        {
            if (InvalidCharacterText == null)
            {
                StringBuilder buffer;

                buffer = new StringBuilder();
                buffer.Append(Properties.Filename.InputInvalidChar);
                buffer.AppendLine();
                buffer.AppendLine();

                var invalid = from c in InvalidCharacters
                              where c >= 31
                              orderby c
                              select c;

                foreach (var ch in invalid)
                {
                    buffer.Append(' ');
                    buffer.Append(ch);
                } // foreach
                InvalidCharacterText = buffer.ToString();
            } // if

            MessageBox.Show(this, InvalidCharacterText, Properties.Filename.InputInvalidCharCaption,
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
        } // DisplayInvalidCharacterWarning

        private bool RemoveInvalidChars(bool displayWarning)
        {
            bool modified;
            int caretPos;

            var newText = TextUtils.RemoveInvalidChars(this.Text, InvalidCharacters, null, out modified);
            if (!modified) return false;

            if (displayWarning)
            {
                DisplayInvalidCharacterWarning();
            } // if

            ManualUpdateOfValue = true;
            caretPos = this.SelectionStart;
            this.Text = newText;
            this.SelectionStart = (caretPos <= 0)? 0 : caretPos - 1;
            ManualUpdateOfValue = false;

            return true;
        } // RemoveInvalidChars
    } // class FilenameTextBox
} // namespace

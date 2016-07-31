// Copyright (C) 2014-2016, Codeplex user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Project.IpTv.UiServices.Common.Controls
{
    [ToolboxBitmap(typeof(TextBox))]
    public class FilenameTextBox : TextBox
    {
        private char[] InvalidCharacters;
        private string InvalidCharacterText;
        private bool ManualUpdateOfValue;

        public FilenameTextBox()
        {
            InvalidCharacters = Path.GetInvalidFileNameChars();
        } // constructor

        public static string RemoveOffendingChars(string text, char[] offendingChars)
        {
            bool modified;

            return RemoveOffendingChars(text, offendingChars, null, out modified);
        } // RemoveOffendingChars

        public static string RemoveOffendingChars(string text, char[] offendingChars, string replacementText)
        {
            bool modified;

            return RemoveOffendingChars(text, offendingChars, replacementText, out modified);
        } // RemoveOffendingChars

        public static string RemoveOffendingChars(string text, char[] offendingChars, string replacementText, out bool modified)
        {
            if ((offendingChars == null) || (offendingChars.Length == 0))
            {
                throw new ArgumentException("offendingChars");
            } // if

            return InternalRemoveOffendingChars(text, offendingChars, replacementText, out modified);
        } // RemoveOffendingChars

        /// <summary>
        /// Sets the text and removes invalid characters if needed
        /// </summary>
        /// <param name="text">Text to set</param>
        public void SetText(string text, bool raiseTextChangedEvent)
        {
            ManualUpdateOfValue = true;
            this.Text = text;
            RemoveOffendingChars(false);
            ManualUpdateOfValue = false;

            if (raiseTextChangedEvent)
            {
                base.OnTextChanged(EventArgs.Empty);
            } // if
        } // SetText

        protected override void OnTextChanged(EventArgs e)
        {
            if (ManualUpdateOfValue) return;
            if (RemoveOffendingChars(true)) return;

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

        private bool RemoveOffendingChars(bool displayWarning)
        {
            bool modified;
            int caretPos;

            var newText = InternalRemoveOffendingChars(this.Text, InvalidCharacters, null, out modified);
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
        } // RemoveOffendingChars

        private static string InternalRemoveOffendingChars(string text, char[] offendingChars, string replacementString, out bool modified)
        {
            StringBuilder buffer;
            int startIndex, index;

            modified = false;

            // do nothing is null or empty
            if (string.IsNullOrEmpty(text)) return text;

            // quick test: any offending char?
            index = text.IndexOfAny(offendingChars);
            if (index < 0) return text;

            buffer = new StringBuilder(text.Length * 2);
            startIndex = 0;
            while (index >= 0)
            {
                if (index != startIndex)
                {
                    buffer.Append(text.Substring(startIndex, (index - startIndex)));
                    if (replacementString != null)
                    {
                        buffer.Append(replacementString);
                    } // if
                } // if

                startIndex = index + 1;
                index = (startIndex < text.Length) ? text.IndexOfAny(offendingChars, startIndex) : -1;
            } // while

            // add final text
            if (startIndex < text.Length)
            {
                buffer.Append(text.Substring(startIndex, text.Length - startIndex));
            } // if

            modified = true;
            return buffer.ToString();
        } // RemoveOffendingChars
    } // class FilenameTextBox
} // namespace

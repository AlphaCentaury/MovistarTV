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

using IpTviewr.Common;
using System;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace IpTviewr.UiServices.Common.Controls
{
    [ToolboxBitmap(typeof(TextBox))]
    public class FilenameTextBox : TextBox
    {
        private readonly char[] _invalidCharacters;
        private string _invalidCharacterText;
        private bool _manualUpdateOfValue;

        public FilenameTextBox()
        {
            _invalidCharacters = TextUtils.GetFilenameInvalidChars();
        } // constructor

        /// <summary>
        /// Sets the text and removes invalid characters if needed
        /// </summary>
        /// <param name="text">Text to set</param>
        /// <param name="raiseTextChangedEvent"></param>
        public void SetText(string text, bool raiseTextChangedEvent)
        {
            _manualUpdateOfValue = true;
            Text = text;
            RemoveInvalidChars(false);
            _manualUpdateOfValue = false;

            if (raiseTextChangedEvent)
            {
                base.OnTextChanged(EventArgs.Empty);
            } // if
        } // SetText

        protected override void OnTextChanged(EventArgs e)
        {
            if (_manualUpdateOfValue) return;
            if (RemoveInvalidChars(true)) return;

            base.OnTextChanged(e);
        } // OnTextChanged

        private void DisplayInvalidCharacterWarning()
        {
            if (_invalidCharacterText == null)
            {
                StringBuilder buffer;

                buffer = new StringBuilder();
                buffer.Append(Properties.Filename.InputInvalidChar);
                buffer.AppendLine();
                buffer.AppendLine();

                var invalid = from c in _invalidCharacters
                              where c >= 31
                              orderby c
                              select c;

                foreach (var ch in invalid)
                {
                    buffer.Append(' ');
                    buffer.Append(ch);
                } // foreach
                _invalidCharacterText = buffer.ToString();
            } // if

            MessageBox.Show(this, _invalidCharacterText, Properties.Filename.InputInvalidCharCaption,
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
        } // DisplayInvalidCharacterWarning

        private bool RemoveInvalidChars(bool displayWarning)
        {
            int caretPos;

            var newText = TextUtils.RemoveInvalidChars(Text, _invalidCharacters, null, out var modified);
            if (!modified) return false;

            if (displayWarning)
            {
                DisplayInvalidCharacterWarning();
            } // if

            _manualUpdateOfValue = true;
            caretPos = SelectionStart;
            Text = newText;
            SelectionStart = (caretPos <= 0)? 0 : caretPos - 1;
            _manualUpdateOfValue = false;

            return true;
        } // RemoveInvalidChars
    } // class FilenameTextBox
} // namespace

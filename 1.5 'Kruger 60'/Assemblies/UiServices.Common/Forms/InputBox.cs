using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IpTviewr.UiServices.Common.Forms
{
    public partial class InputBox : Form
    {
        public InputBox()
        {
            InitializeComponent();
        } // constructor

        public string Message
        {
            get => labelInput.Text;
            set => labelInput.Text = value;
        } // Message

        public string Value
        {
            get => textBoxValue.Text;
            set
            {
                textBoxValue.Text = value;
                textBoxValue.SelectionStart = value?.Length ?? 0;
                textBoxValue.SelectionLength = 0;
            } // set
        } // Value

        public static bool ShowDialog(IWin32Window owner, string message, string caption, ref string value)
        {
            using var dialog = new InputBox
            {
                Message = message,
                Value = value
            };
            if (!string.IsNullOrEmpty(caption)) dialog.Text = caption;
            var ok = dialog.ShowDialog(owner) == DialogResult.OK;
            if (ok)
            {
                value = dialog.Value;
            } // if

            return ok;
        } // ShowDialog
    } // class InputBox
} // namespace

// Copyright (C) 2014-2019, GitHub/Codeplex user AlphaCentaury
// 
// All rights reserved, except those granted by the governing license of this software.
// See 'license.txt' file in the project root for complete license information.
// 
// http://www.alphacentaury.org/movistartv https://github.com/AlphaCentaury

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AlphaCentaury.Tools.SourceCodeMaintenance.Properties;
using IpTviewr.UiServices.Common.Controls;
using JetBrains.Annotations;

namespace AlphaCentaury.Tools.SourceCodeMaintenance.Batch
{
    public partial class ArgumentsListEditor : StringListEditor
    {
        private Action<TextBox> _selectFileAction;

        public ArgumentsListEditor()
        {
            InitializeComponent();
        } // ArgumentsListEditor

        [PublicAPI]
        public Image SelectFileButtonImage
        {
            get => buttonSelectFile.Image;
            set => buttonSelectFile.Image = value;
        } // SelectFileButtonImage

        [PublicAPI]
        public Action<TextBox> SelectFileAction
        {
            get => _selectFileAction;
            set
            {
                _selectFileAction = value;
                buttonSelectFile.Enabled = (value != null);
            } // set
        } // SelectFileAction

        private void buttonSelectFile_Click(object sender, EventArgs e)
        {
            SelectFileAction?.Invoke(textBoxArgument);
        } // buttonSelectFile_Click

        #region Overrides of StringListEditor

        protected override bool GetNewItem(out string newItem)
        {
            newItem = textBoxArgument.Text.Trim();
            if (newItem.Length == 0)
            {
                MessageBox.Show(this, Batch_Texts.ArgumentsNotOptional, Batch_Texts.ArgumentsNotOptionalCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            } // if
            if (newItem.IndexOf(' ') >= 0) newItem = '"' + newItem + '"';

            return true;
        } // GetNewItem

        protected override bool EditItem(string item, out string newItem)
        {
            var ok = base.EditItem(item, out newItem);
            if (ok && (newItem.IndexOf(' ') >= 0))
            {
                newItem = '"' + newItem + '"';
            } // if

            return ok;
        } // EditItem

        #endregion
    }
}

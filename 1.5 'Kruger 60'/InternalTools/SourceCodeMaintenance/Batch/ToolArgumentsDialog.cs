// Copyright (C) 2014-2019, GitHub/Codeplex user AlphaCentaury
// 
// All rights reserved, except those granted by the governing license of this software.
// See 'license.txt' file in the project root for complete license information.
// 
// http://www.alphacentaury.org/movistartv https://github.com/AlphaCentaury

using System;
using System.Drawing;
using System.Windows.Forms;
using JetBrains.Annotations;

namespace AlphaCentaury.Tools.SourceCodeMaintenance.Batch
{
    public partial class ToolArgumentsDialog : Form
    {
        public ToolArgumentsDialog()
        {
            InitializeComponent();
            buttonOk.Enabled = false;
        } // constructor

        [PublicAPI]
        public Image SelectFileButtonImage
        {
            get => argumentsListEditor.SelectFileButtonImage;
            set => argumentsListEditor.SelectFileButtonImage = value;
        } // SelectFileButtonImage

        [PublicAPI]
        public string[] Arguments
        {
            get => argumentsListEditor.Items;
            set
            {
                argumentsListEditor.Items = value;
                buttonOk.Enabled = (argumentsListEditor.ItemsCount > 0);
            } // set
        } // Arguments

        [PublicAPI]
        public Action<TextBox> SelectFileAction
        {
            get => argumentsListEditor.SelectFileAction;
            set => argumentsListEditor.SelectFileAction = value;
        } // SelectFileAction

        private void argumentsListEditor_DataChanged(object sender, EventArgs e)
        {
            buttonOk.Enabled = (argumentsListEditor.ItemsCount > 0);
        } // argumentsListEditor_DataChanged
    } // ToolArgumentsDialog
} // namespace

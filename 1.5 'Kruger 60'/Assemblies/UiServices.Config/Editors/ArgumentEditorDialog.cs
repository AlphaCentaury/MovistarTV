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

namespace IpTviewr.UiServices.Configuration.Editors
{
    public partial class ArgumentEditorDialog : Form
    {
        public ArgumentEditorDialog()
        {
            InitializeComponent();
        } // constructor

        public bool IsDataChanged
        {
            get;
            private set;
        } // IsDataChanged

        public string Parameter
        {
            get => parameterEditor.CommandLine;
            set
            {
                parameterEditor.CommandLine = value;
                buttonOk.Enabled = (parameterEditor.CommandLine != "");
                Text = buttonOk.Enabled ? Properties.SettingsTexts.ParameterEditCaption : Properties.SettingsTexts.ParameterNewCaption;
            }
        } // Parameter

        public string OpenBraceText
        {
            get => parameterEditor.OpenBraceText;
            set => parameterEditor.OpenBraceText = value;
        } // OpenBraceText

        public string CloseBraceText
        {
            get => parameterEditor.CloseBraceText;
            set => parameterEditor.CloseBraceText = value;
        } // CloseBraceText

        public string ParametersList
        {
            get => parameterEditor.ParametersList;
            set => parameterEditor.ParametersList = value;
        } // ParametersList

        private void parameterEditor_CommandLineChanged(object sender, EventArgs e)
        {
            buttonOk.Enabled = (parameterEditor.CommandLine != "");
            IsDataChanged = true;
        } // parameterEditor_CommandLineChanged
    } // class ArgumentEditorDialog
} // namespace

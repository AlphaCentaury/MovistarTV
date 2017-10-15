// Copyright (C) 2014-2017, GitHub/Codeplex user AlphaCentaury
// 
// All rights reserved, except those granted by the governing license of this software.
// See 'license.txt' file in the project root for complete license information.
// 
// http://movistartv.alphacentaury.org/ https://github.com/AlphaCentaury

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
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
            get
            {
                return parameterEditor.CommandLine;
            }
            set
            {
                parameterEditor.CommandLine = value;
                buttonOk.Enabled = (parameterEditor.CommandLine != "");
                Text = buttonOk.Enabled ? Properties.SettingsTexts.ParameterEditCaption : Properties.SettingsTexts.ParameterNewCaption;
            }
        } // Parameter

        public string OpenBraceText
        {
            get { return parameterEditor.OpenBraceText; }
            set { parameterEditor.OpenBraceText = value; }
        } // OpenBraceText

        public string CloseBraceText
        {
            get { return parameterEditor.CloseBraceText; }
            set { parameterEditor.CloseBraceText = value; }
        } // CloseBraceText

        public string ParametersList
        {
            get { return parameterEditor.ParametersList; }
            set { parameterEditor.ParametersList = value; }
        } // ParametersList

        private void parameterEditor_CommandLineChanged(object sender, EventArgs e)
        {
            buttonOk.Enabled = (parameterEditor.CommandLine != "");
            IsDataChanged = true;
        } // parameterEditor_CommandLineChanged
    } // class ArgumentEditorDialog
} // namespace

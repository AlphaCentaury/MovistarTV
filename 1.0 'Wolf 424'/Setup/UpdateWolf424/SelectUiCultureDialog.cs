// Copyright (C) 2015, Codeplex user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Project.DvbIpTv.Setup.UpdateWolf424
{
    public partial class SelectUiCultureDialog : Form
    {
        public SelectUiCultureDialog()
        {
            InitializeComponent();
            this.Icon = Properties.Resources.QuestionIcon;

            if (Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName == "es")
            {
                radioSpanish.Checked = true;
            }
            else
            {
                radioEnglish.Checked = true;
            }
            ApplyResources();
        }

        private void radioSpanish_CheckedChanged(object sender, EventArgs e)
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("es-es");
            ApplyResources();
        } // radioSpanish_CheckedChanged

        private void radioEnglish_CheckedChanged(object sender, EventArgs e)
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-us");

            ApplyResources();
        } // radioEnglish_CheckedChanged

        private void ApplyResources()
        {
            Properties.Resources.Culture = Thread.CurrentThread.CurrentUICulture;
            ComponentResourceManager resources = new ComponentResourceManager(typeof(SelectUiCultureDialog));
            resources.ApplyResources(this, "$this");
            resources.ApplyResources(this.labelSelect, "labelSelect");
            resources.ApplyResources(this.buttonOk, "buttonOk");
        } // ApplyResources
    }
}

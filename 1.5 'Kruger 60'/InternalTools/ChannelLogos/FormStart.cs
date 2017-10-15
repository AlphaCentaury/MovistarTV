// Copyright (C) 2014-2017, GitHub/Codeplex user AlphaCentaury
// 
// All rights reserved, except those granted by the governing license of this software.
// See 'license.txt' file in the project root for complete license information.
// 
// http://movistartv.alphacentaury.org/ https://github.com/AlphaCentaury

using IpTviewr.UiServices.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace IpTviewr.Internal.Tools.ChannelLogos
{
    public partial class FormStart : Form
    {
        public FormStart()
        {
            InitializeComponent();
            Icon = Properties.Resources.IPTViewr_Tool;
        } // constructor

        private void buttonGo_Click(object sender, EventArgs e)
        {
            Form form;

            if (AppUiConfiguration.Current == null)
            {
                labelLoadingConfiguration.Text = "Loading configuration...";
                labelLoadingConfiguration.Visible = true;
                labelLoadingConfiguration.Refresh();
                if (!LoadConfiguration()) return;

                labelLoadingConfiguration.Visible = false;
            } // if

            if (radioLogosGrid.Checked) form = new FormLogos();
            else if (radioConsistency.Checked) form = new FormConsistency();
            else form = null;

            if (form == null) return;

            form.Show();
        } // buttonGo_Click

        private bool LoadConfiguration()
        {
            var result = GetConfiguration();
            if (!result.IsOk)
            {
                LoadDisplayProgress(result.Message);
                Program.HandleException(this, result.Caption, result.Message, result.InnerException);
                return false;
            };

            return true;
        } // LoadConfiguration

        private InitializationResult GetConfiguration()
        {
            InitializationResult result;

            try
            {
                result = AppUiConfiguration.Load(null, LoadDisplayProgress);
                if (result.IsError) return result;

                return InitializationResult.Ok;
            }
            catch (Exception ex)
            {
                return new InitializationResult()
                {
                    Caption = "Application configuration error",
                    Message = "An unexpected error has occured while loading the application configuration.",
                    InnerException = ex
                };
            } // try-catch
        } // GetConfiguration
        private void LoadDisplayProgress(string text)
        {
            labelLoadingConfiguration.Text = text;
            labelLoadingConfiguration.Refresh();
        } // LoadDisplayProgress
    } // class FormStart
} // namespace

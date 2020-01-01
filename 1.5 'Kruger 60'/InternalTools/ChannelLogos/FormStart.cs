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

using IpTviewr.UiServices.Configuration;
using System;
using System.Windows.Forms;
using IpTviewr.Common.Configuration;

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

            if (AppConfig.Current == null)
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

            form?.Show();
        } // buttonGo_Click

        private bool LoadConfiguration()
        {
            var result = GetConfiguration();
            if (result.IsOk) return true;

            LoadDisplayProgress(result.Message);
            Program.HandleException(this, result.Caption, result.Message, result.InnerException);

            return false;
        } // LoadConfiguration

        private InitializationResult GetConfiguration()
        {
            try
            {
                var result = AppConfig.Load(null, LoadDisplayProgress);
                return result.IsError ? result : InitializationResult.Ok;
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

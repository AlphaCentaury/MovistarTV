// Copyright (C) 2014, Codeplex user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Project.DvbIpTv.Tools.FirstTimeConfig
{
    public partial class ConfigForm : Form
    {
        string ProgramFilesFolder;

        public ConfigForm()
        {
            InitializeComponent();
            this.Icon = Properties.Resources.InstallIcon;
        } // constructor

        private void ConfigForm_Load(object sender, EventArgs e)
        {
            selectFolder.Description = Properties.Texts.SelectFolderSaveDescription;
            openFile.Title = Properties.Texts.OpenFileVlcTitle;
            openFile.Filter = Properties.Texts.OpenFileVlcFilter;
            saveFile.Title = Properties.Texts.SaveFileXmlTitle;
            saveFile.Filter = Properties.Texts.SaveFileXmlFilter;

            foreach (Control control in groupPrerequisites.Controls)
            {
                control.Enabled = false;
            } // foreach

            labelVlc.Enabled = true;
            linkLabelPrerequisiteVlc.Enabled = true;

            labelNet.Enabled = true;
            buttonVerifyNet.Enabled = true;
            linkLabelPrerequisiteNet.Enabled = true;

            foreach (Control control in groupBasicConfig.Controls)
            {
                control.Enabled = false;
            } // foreach
            groupBasicConfig.Enabled = false;

            buttonConfig.Enabled = false;

            try
            {
                ProgramFilesFolder = Installation.GetProgramFilesx86Folder();
            }
            catch
            {
                // ignore
            } // try-catch

            try
            {
                textBoxSave.Text = Installation.GetCurrentUserVideosFolder();
            }
            catch
            {
                textBoxSave.Text = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            } // try-catch
        } // ConfigForm_Load

        private void linkLabelPrerequisiteNet_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenUrl(this, Properties.Texts.DownloadUrlNet);
        } // linkLabelPrerequisiteNet_LinkClicked

        private void linkLabelPrerequisiteEmb_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenUrl(this, Properties.Texts.DownloadUrlEmb);
        } // linkLabelPrerequisiteEmb_LinkClicked

        private void linkLabelPrerequisiteVlc_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenUrl(this, Properties.Texts.DownloadUrlVlc);
        } // linkLabelPrerequisiteVlc_LinkClicked

        private void buttonVerifyNet_Click(object sender, EventArgs e)
        {
            string message;

            var installed = Installation.IsNetInstalled(out message);
            MessageBox.Show(this, message, this.Text, MessageBoxButtons.OK,
                installed? MessageBoxIcon.Information : MessageBoxIcon.Warning);

            if (!installed) return;

            labelEmb.Enabled = true;
            buttonVerifyEmb.Enabled = true;
            linkLabelPrerequisiteEmb.Enabled = true;
            buttonVerifyEmb.Focus();
        } // buttonVerifyNet_Click

        private void buttonVerifyEmb_Click(object sender, EventArgs e)
        {
            string message;

            var installed = Installation.IsEmbInstalled(out message);
            MessageBox.Show(this, message, this.Text, MessageBoxButtons.OK,
                installed ? MessageBoxIcon.Information : MessageBoxIcon.Warning);

            if (!installed) return;

            if (ProgramFilesFolder != null)
            {
                var vlcDefaultPath = Path.Combine(ProgramFilesFolder,  @"VideoLAN\VLC\vlc.exe");
                if (File.Exists(vlcDefaultPath))
                {
                    textBoxVlc.Text = vlcDefaultPath;
                } // if
            } // if

            groupBasicConfig.Enabled = true;
            labelVlcPath.Enabled = true;
            textBoxVlc.Enabled = true;
            buttonFindVlc.Enabled = true;
            buttonVerifyVlc.Enabled = textBoxVlc.Text.Length > 0;

            var button = buttonVerifyVlc.Enabled ? buttonVerifyVlc : buttonFindVlc;
            button.Focus();
        } // buttonVerifyEmb_Click

        private void buttonFindVlc_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBoxVlc.Text))
            {
                openFile.InitialDirectory = Path.GetDirectoryName(textBoxVlc.Text);
                openFile.FileName = Path.GetFileName(textBoxVlc.Text); ;
            } // if

            if (openFile.ShowDialog(this) != DialogResult.OK) return;

            textBoxVlc.Text = openFile.FileName;
            buttonVerifyVlc.Enabled = true;
            buttonVerifyVlc.Focus();
        } // buttonFindVlc_Click

        private void buttonVerifyVlc_Click(object sender, EventArgs e)
        {
            string message;

            var testMedia = Installation.GetTestMedia();
            var installed = Installation.IsVlcInstalled(out message, textBoxVlc.Text, testMedia);
            MessageBox.Show(this, message, this.Text, MessageBoxButtons.OK,
                installed ? MessageBoxIcon.Information : MessageBoxIcon.Warning);

            if (!installed) return;

            labelSaveFolder.Enabled = true;
            textBoxSave.Enabled = true;
            buttonBrowseSave.Enabled = true;
            buttonBrowseSave.Focus();
        } // buttonVerifyVlc_Click

        private void buttonBrowseSave_Click(object sender, EventArgs e)
        {
            selectFolder.NewStyle = true;
            selectFolder.SelectedPath = textBoxSave.Text;
            selectFolder.RootFolder = Environment.SpecialFolder.Desktop;
            if (selectFolder.ShowDialog(this) != DialogResult.OK) return;

            textBoxSave.Text = selectFolder.SelectedPath;

            labelSaveSubFolder.Enabled = true;
            textSaveSubFolder.Enabled = true;
            buttonConfig.Enabled = true;
            buttonConfig.Focus();
        } // buttonBrowseSave_Click

        private void buttonConfig_Click(object sender, EventArgs e)
        {
            string message;
            string xmlConfigPath;

            saveFile.InitialDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "DvbIpTv-MovistarTV");
            saveFile.FileName = "user-config.xml";
            if (saveFile.ShowDialog(this) != DialogResult.OK) return;
            xmlConfigPath = saveFile.FileName;

            var rootFolder = textBoxSave.Text;
            var subFolder = textSaveSubFolder.Text.Trim();
            if (subFolder != string.Empty)
            {
                rootFolder = Path.Combine(rootFolder, subFolder);
            } // if

            xmlConfigPath = Path.Combine(Path.GetDirectoryName(xmlConfigPath), "user-config.xml");

            if (File.Exists(xmlConfigPath))
            {
                if (MessageBox.Show(this, Properties.Texts.OverwriteXmlConfigFile, this.Text,
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != DialogResult.Yes)
                {
                    return;
                } // if
            } // if

            var success = Configuration.Create(textBoxVlc.Text, rootFolder, xmlConfigPath, out message);
            MessageBox.Show(this, message, this.Text, MessageBoxButtons.OK,
                success ? MessageBoxIcon.Information : MessageBoxIcon.Warning);

            if (!success) return;

            DialogResult = DialogResult.OK;
            Close();
        } // buttonConfig_Click

        private static void OpenUrl(Form parent, string url)
        {
            try
            {
                using (var process = new Process())
                {
                    process.StartInfo = new ProcessStartInfo()
                    {
                        FileName = url,
                        UseShellExecute = true,
                        ErrorDialog = true,
                        ErrorDialogParentHandle = parent.Handle,
                    };
                    process.Start();
                } // using process
            }
            catch (Exception ex)
            {
                MessageBox.Show(parent,
                    string.Format(Properties.Texts.OpenUrlError, url, ex.ToString()),
                    parent.Text, MessageBoxButtons.OK, MessageBoxIcon.Stop);
            } // try-catch
        } // OpenUrl

    } // class ConfigForm
} // namespace

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

using AlphaCentaury.Tools.SourceCodeMaintenance.Licensing.Actions;
using IpTviewr.Common.Serialization;
using IpTviewr.UiServices.Common.Forms;
using System;
using System.Windows.Forms;

namespace AlphaCentaury.Tools.SourceCodeMaintenance.Licensing
{
    public partial class LicensingToolOptionsDialog : CommonBaseForm
    {
        private LicensingToolOptions _options;
        private int _writeScope;

        public LicensingToolOptionsDialog()
        {
            InitializeComponent();
        } // constructor

        public LicensingToolOptions Options { get; set; }

        #region Overrides of Form

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            _options = Options?.Clone() ?? new LicensingToolOptions();
            _options.Checker ??= new CheckerOptions();
            _options.Writer ??= new WriterOptions();
            _options.SolutionWriter ??= new WriterOptions();
            _writeScope = -1;

            SetOptionsToDialog(_options);
        } // OnLoad

        #endregion

        #region Controls Events

        private void checkBoxWritePlainText_CheckedChanged(object sender, EventArgs e)
        {
            checkBoxWriteTranslatedPlainText.Enabled = checkBoxWritePlainText.Checked;
        } // checkBoxWritePlainText_CheckedChanged

        private void checkBoxWriteMarkdown_CheckedChanged(object sender, EventArgs e)
        {
            checkBoxWriteTranslatedMarkdown.Enabled = checkBoxWriteMarkdown.Checked;
        } // checkBoxWriteMarkdown_CheckedChanged

        private void checkBoxWriteHtml_CheckedChanged(object sender, EventArgs e)
        {
            checkBoxWriteTranslatedHtml.Enabled = checkBoxWriteHtml.Checked;
        } // checkBoxWriteHtml_CheckedChanged

        private void checkBoxWriteRtf_CheckedChanged(object sender, EventArgs e)
        {
            checkBoxWriteTranslatedRtf.Enabled = checkBoxWriteRtf.Checked;
        } // checkBoxWriteRtf_CheckedChanged

        private void buttonLoad_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog(this) != DialogResult.OK) return;
            SafeCall(() =>
            {
                var options = XmlSerialization.Deserialize<LicensingToolOptions>(openFileDialog.FileName);
                SetOptionsToDialog(options);
            });
        } // buttonLoad_Click

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (saveFileDialog.ShowDialog(this) != DialogResult.OK) return;
            SafeCall(() =>
            {
                GetOptionsFromDialog(_options);
                XmlSerialization.Serialize(saveFileDialog.FileName, _options);
            });
        } // buttonSave_Click

        private void buttonClear_Click(object sender, EventArgs e)
        {
            Clear();
        } // buttonClear_Click

        private void buttonOk_Click(object sender, EventArgs e)
        {
            GetOptionsFromDialog(_options);
            Options = _options;
        } // buttonOk_Click

        #endregion

        private void SetOptionsToDialog(LicensingToolOptions options)
        {
            if (options == null) return;

            if (options.Checker != null)
            {
                checkBoxOverrideAuthors.Checked = options.Checker.OverrideAuthors;
                checkBoxOverrideCopyright.Checked = options.Checker.OverrideCopyright;
                checkBoxOverrideLicense.Checked = options.Checker.OverrideLicense;
                checkBoxOverrideProduct.Checked = options.Checker.OverrideProduct;
                checkBoxOverrideTerms.Checked = options.Checker.OverrideTerms;
                checkBoxOverrideRemarks.Checked = options.Checker.OverrideRemarks;
                checkBoxOverrideNotes.Checked = options.Checker.OverrideNotes;
            } // if

            comboBoxWriteScope.SelectedIndex = -1;
            comboBoxWriteScope.SelectedIndex = 0;
        } // SetOptionsToDialog

        private void SetWriterOptionsToDialog(WriterOptions options)
        {
            checkBoxWritePlainText.Checked = options.PlainText;
            checkBoxWriteMarkdown.Checked = options.Markdown;
            checkBoxWriteHtml.Checked = options.Html;
            checkBoxWriteRtf.Checked = options.Rtf;

            checkBoxWriteTranslatedPlainText.Checked = options.TranslatedPlainText;
            checkBoxWriteTranslatedMarkdown.Checked = options.TranslatedMarkdown;
            checkBoxWriteTranslatedHtml.Checked = options.TranslatedHtml;
            checkBoxWriteTranslatedRtf.Checked = options.TranslatedRtf;

            checkBoxWriteTranslatedPlainText.Enabled = options.PlainText;
            checkBoxWriteTranslatedMarkdown.Enabled = options.Markdown;
            checkBoxWriteTranslatedHtml.Enabled = options.Html;
            checkBoxWriteTranslatedRtf.Enabled = options.Rtf;

            checkBoxWriteLicensingHtml.Checked = options.LicensingHtml;
            checkBoxWriteLicensingRtf.Checked = options.LicensingRtf;

            checkBoxWriteDeleteOld.Checked = options.DeleteOldFiles;
        } // SetWriterOptionsToDialog

        private void GetOptionsFromDialog(LicensingToolOptions options)
        {
            if (options == null) return;

            options.Checker.OverrideAuthors = checkBoxOverrideAuthors.Checked;
            options.Checker.OverrideCopyright = checkBoxOverrideCopyright.Checked;
            options.Checker.OverrideLicense = checkBoxOverrideLicense.Checked;
            options.Checker.OverrideProduct = checkBoxOverrideProduct.Checked;
            options.Checker.OverrideTerms = checkBoxOverrideTerms.Checked;
            options.Checker.OverrideRemarks = checkBoxOverrideRemarks.Checked;
            options.Checker.OverrideNotes = checkBoxOverrideNotes.Checked;

            switch (_writeScope)
            {
                case 0:
                    GetWriterOptionsFromDialog(_options.Writer);
                    break;
                case 1:
                    GetWriterOptionsFromDialog(_options.SolutionWriter);
                    break;
            } // switch
        } // GetOptionsFromDialog

        private void GetWriterOptionsFromDialog(WriterOptions options)
        {
            options.PlainText = checkBoxWritePlainText.Checked;
            options.Markdown = checkBoxWriteMarkdown.Checked;
            options.Html = checkBoxWriteHtml.Checked;
            options.Rtf = checkBoxWriteRtf.Checked;

            options.TranslatedPlainText = checkBoxWriteTranslatedPlainText.Checked;
            options.TranslatedMarkdown = checkBoxWriteTranslatedMarkdown.Checked;
            options.TranslatedHtml = checkBoxWriteTranslatedHtml.Checked;
            options.TranslatedRtf = checkBoxWriteTranslatedRtf.Checked;

            options.LicensingHtml = checkBoxWriteLicensingHtml.Checked;
            options.LicensingRtf = checkBoxWriteLicensingRtf.Checked;

            options.DeleteOldFiles = checkBoxWriteDeleteOld.Checked;
        } // WriterOptions

        private void Clear()
        {
            _options = new LicensingToolOptions(true);

            SetOptionsToDialog(_options);
        } // Clear

        private void comboBoxWriteScope_SelectedIndexChanged(object sender, EventArgs e)
        {
            // save changes, if any
            switch (_writeScope)
            {
                case 0:
                    GetWriterOptionsFromDialog(_options.Writer);
                    break;
                case 1:
                    GetWriterOptionsFromDialog(_options.SolutionWriter);
                    break;
            } // switch

            switch (comboBoxWriteScope.SelectedIndex)
            {
                case 0:
                    SetWriterOptionsToDialog(_options.Writer);
                    break;
                case 1:
                    SetWriterOptionsToDialog(_options.SolutionWriter);
                    break;
            } // switch

            _writeScope = comboBoxWriteScope.SelectedIndex;
        } // comboBoxWriteScope_SelectedIndexChanged
    } // class LicensingToolOptionsDialog
} // namespace

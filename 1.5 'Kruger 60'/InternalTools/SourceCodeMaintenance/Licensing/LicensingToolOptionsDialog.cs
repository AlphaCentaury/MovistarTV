using System;
using System.Windows.Forms;
using AlphaCentaury.Tools.SourceCodeMaintenance.Licensing.Actions;
using IpTviewr.Common;
using IpTviewr.Common.Serialization;
using IpTviewr.UiServices.Common.Forms;

namespace AlphaCentaury.Tools.SourceCodeMaintenance.Licensing
{
    public partial class LicensingToolOptionsDialog : CommonBaseForm
    {
        public LicensingToolOptionsDialog()
        {
            InitializeComponent();
        } // constructor

        internal LicensingToolOptions Options { get; set; }

        #region Overrides of Form

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            SetOptionsToDialog(Options);
        } // OnLoad

        #endregion

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
                var options = new LicensingToolOptions();
                GetOptionsFromDialog(options);
                XmlSerialization.Serialize(saveFileDialog.FileName, options);
            });
        } // buttonSave_Click

        private void buttonClear_Click(object sender, EventArgs e)
        {
            Clear();
        } // buttonClear_Click

        private void buttonOk_Click(object sender, EventArgs e)
        {
            GetOptionsFromDialog(Options);
        } // buttonOk_Click

        private void Clear()
        {
            var options = new LicensingToolOptions(true);

            SetOptionsToDialog(options);
        } // Clear

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

            if (options.Writer != null)
            {
                checkBoxWriteHtml.Checked = options.Writer.WriteHtml;
                checkBoxWriteSkipLicensingHtml.Checked = options.Writer.SkipLicensingHtml;
            } // if
        } // SetOptionsToDialog

        private void GetOptionsFromDialog(LicensingToolOptions options)
        {
            if (options == null) return;
            options.Checker ??= new CheckerOptions();
            options.Writer ??= new WriterOptions();

            options.Checker.OverrideAuthors = checkBoxOverrideAuthors.Checked;
            options.Checker.OverrideCopyright = checkBoxOverrideCopyright.Checked;
            options.Checker.OverrideLicense = checkBoxOverrideLicense.Checked;
            options.Checker.OverrideProduct = checkBoxOverrideProduct.Checked;
            options.Checker.OverrideTerms = checkBoxOverrideTerms.Checked;
            options.Checker.OverrideRemarks = checkBoxOverrideRemarks.Checked;
            options.Checker.OverrideNotes = checkBoxOverrideNotes.Checked;

            options.Writer.WriteHtml = checkBoxWriteHtml.Checked;
            options.Writer.SkipLicensingHtml = checkBoxWriteSkipLicensingHtml.Checked;
        } // GetOptionsFromDialog
    } // class LicensingToolOptionsDialog
} // namespace

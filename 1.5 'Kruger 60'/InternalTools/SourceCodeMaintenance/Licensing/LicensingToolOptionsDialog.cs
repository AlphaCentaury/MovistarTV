using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IpTviewr.Common;
using IpTviewr.UiServices.Common.Forms;

namespace AlphaCentaury.Tools.SourceCodeMaintenance.Licensing
{
    public partial class LicensingToolOptionsDialog : CommonBaseForm
    {
        public LicensingToolOptionsDialog()
        {
            InitializeComponent();
        } // constructor

        public CheckerOptions CheckerOptions { get; set; }

        #region Overrides of Form

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (CheckerOptions == null)
            {
                HandleException(new ExceptionEventData(new ArgumentNullException(nameof(CheckerOptions))));
                Close();
                return;
            } // if

            checkBoxOverrideAuthors.Checked = CheckerOptions.OverrideAuthors;
            checkBoxOverrideCopyright.Checked = CheckerOptions.OverrideCopyright;
            checkBoxOverrideLicense.Checked = CheckerOptions.OverrideLicense;
            checkBoxOverrideProduct.Checked = CheckerOptions.OverrideProduct;
            checkBoxOverrideTerms.Checked = CheckerOptions.OverrideTerms;
        } // OnLoad

        #endregion

        private void buttonOk_Click(object sender, EventArgs e)
        {
            CheckerOptions.OverrideAuthors = checkBoxOverrideAuthors.Checked;
            CheckerOptions.OverrideCopyright = checkBoxOverrideCopyright.Checked;
            CheckerOptions.OverrideLicense = checkBoxOverrideLicense.Checked;
            CheckerOptions.OverrideProduct = checkBoxOverrideProduct.Checked;
            CheckerOptions.OverrideTerms = checkBoxOverrideTerms.Checked;
        } // buttonOk_Click
    } // class LicensingToolOptionsDialog
} // namespace

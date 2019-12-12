using AlphaCentaury.Licensing.Data.Serialization;
using AlphaCentaury.Licensing.Data.Ui.Properties;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace AlphaCentaury.Licensing.Data.Ui
{
    internal partial class ReversedLicensingFileViewer : UserControl
    {
        private LicensingUsage _usage;
        private LicensingUiImages _licensingImages;
        private bool _isLoaded;

        public ReversedLicensingFileViewer()
        {
            InitializeComponent();
        }

        public LicensingUsage Usage
        {
            get => _usage;
            set
            {
                if (!_isLoaded)
                {
                    _usage = value;
                }
                else
                {
                    _usage = value;
                    OnFileChanged();
                } // if-else
            } // set
        } // File

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            // load image lists
            imageListLicenses.Images.Clear();
            imageListLicenses.ImageSize = new Size(24, 24);
            imageListLicenses.Images.Add("Certificate", Resources.Certificate_24x);

            LicensingUiImages.GetImageListSmall(imageListComponents);
            _licensingImages = new LicensingUiImages(imageListComponents.Images);

            OnFileChanged();
            _isLoaded = true;
        } // OnLoad

        private void OnFileChanged()
        {
            listViewLicenses.Items.Clear();
            listViewComponents.Items.Clear();
            listViewProperties.Items.Clear();
            listViewComponents.Enabled = false;
            listViewProperties.Enabled = false;
            splitContainerVerticalBottom.Panel2Collapsed = true;
            richTextBoxLicense.Text = null;

            if (_usage == null)
            {
                listViewLicenses.Enabled = false;
                return;
            } // if

            FillLicenses();
        } // OnFileChanged

        private void FillLicenses()
        {
            foreach (var license in _usage.Usage.Where(license => license != null))
            {
                listViewLicenses.Items.Add(license.License?.Name ?? Resources.NoName, 0);
            } // foreach
        } // FillLicenses
    }
}

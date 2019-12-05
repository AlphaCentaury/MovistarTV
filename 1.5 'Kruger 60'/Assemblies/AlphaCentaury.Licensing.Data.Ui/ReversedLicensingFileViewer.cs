using AlphaCentaury.Licensing.Data.Serialization;
using AlphaCentaury.Licensing.Data.Ui.Properties;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace AlphaCentaury.Licensing.Data.Ui
{
    public partial class ReversedLicensingFileViewer : UserControl
    {
        private ReversedLicensingFile _file;
        private LicensingUiImages _licensingImages;
        private bool _isLoaded;

        public ReversedLicensingFileViewer()
        {
            InitializeComponent();
        }

        public ReversedLicensingFile File
        {
            get => _file;
            set
            {
                if (!_isLoaded)
                {
                    _file = value;
                }
                else
                {
                    _file = value;
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

            if (_file == null)
            {
                listViewLicenses.Enabled = false;
                return;
            } // if

            FillLicenses();
        } // OnFileChanged

        private void FillLicenses()
        {
            foreach (var license in _file.Licenses.Where(license => license != null))
            {
                listViewLicenses.Items.Add(license.Text?.Name ?? Resources.NoName, 0);
            } // foreach
        } // FillLicenses
    }
}

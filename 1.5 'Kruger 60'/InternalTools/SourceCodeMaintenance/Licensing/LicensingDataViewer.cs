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

using System.Collections.Generic;
using AlphaCentaury.Licensing.Data.Serialization;
using AlphaCentaury.Licensing.Data.Ui;
using AlphaCentaury.Tools.SourceCodeMaintenance.Properties;
using IpTviewr.UiServices.Common.Controls;
using System.ComponentModel;
using System.Globalization;
using System.Text;
using System.Windows.Forms;
using TheArtOfDev.HtmlRenderer.WinForms;
using License = AlphaCentaury.Licensing.Data.Serialization.License;
using Property = System.Collections.Generic.KeyValuePair<string, string>;

namespace AlphaCentaury.Tools.SourceCodeMaintenance.Licensing
{
    public sealed partial class LicensingDataViewer : CommonBaseUserControl
    {
        private LicensingData _licensingData;
        private string _licensingDataName;
        private DetailsModeEnum _detailsMode;
        private object _selectedTag;

        private enum DetailsModeEnum
        {
            None,
            Licensed,
            TermsConditions,
            Library,
            ThirdParty,
            License,
            FormattedText,
        } // DetailsModeEnum

        public LicensingDataViewer()
        {
            InitializeComponent();
            components ??= new Container();

            ImageList = new ImageList(components);
            treeViewLicensingData.ImageList = ImageList;
            LicensingUiImages.GetImageListMedium(ImageList);
            UiImages = new LicensingUiImages(ImageList);
            Ui = new LicensingDataUi(UiImages);
            LicensingData = null;

            htmlPanelLicensingText.BaseStylesheet = LicensingResources.HtmlDefaultStylesheet;
        } // constructor

        public ImageList ImageList { get; }

        public LicensingUiImages UiImages { get; }

        public LicensingDataUi Ui { get; }

        public string LicensingDataName
        {
            get => _licensingDataName;
            set
            {
                _licensingDataName = value;
                if (LicensingData == null) return;

                treeViewLicensingData.Nodes[0].Text = value;
            } // set
        } // LicensingDataName

        public LicensingData LicensingData
        {
            get => _licensingData;
            set
            {
                _licensingData = value;
                var root = (value == null) switch
                {
                    true => new TreeNode(LicensingResources.NoLicensingSelected, UiImages.LicensingData, UiImages.LicensingData),
                    false => Ui.DataToTree(LicensingDataName, value)
                };

                treeViewLicensingData.BeginUpdate();
                treeViewLicensingData.Nodes.Clear();
                treeViewLicensingData.Nodes.Add(root);
                treeViewLicensingData.EndUpdate();
                splitContainerLicensing.Panel2.Visible = false;
            } // set
        } // namespace

        private DetailsModeEnum DetailsMode
        {
            get => _detailsMode;
            set
            {
                if (_detailsMode == value) return;

                _detailsMode = value;
                splitContainerLicensing.Panel2.Visible = true;
                var enable = (value != DetailsModeEnum.None);
                propertiesViewer.Enabled = enable;
                tabControlLicensingDetailsText.Enabled = enable;
                propertiesViewer.Properties = null;
                DisplayTextProperty(null, null);

                if (enable)
                {
                    splitContainer1.SplitterDistance = value switch
                    {
                        DetailsModeEnum.Licensed => ((splitContainer1.Height * 2) / 3),
                        DetailsModeEnum.TermsConditions => ((splitContainer1.Height * 1) / 4),
                        DetailsModeEnum.License => ((splitContainer1.Height * 1) / 4),
                        DetailsModeEnum.FormattedText => ((splitContainer1.Height * 1) / 4),
                        _ => (splitContainer1.Height / 2)
                    };
                } // if
            } // set
        } // DetailsMode;

        private void DisplayTextProperty(string text, string format)
        {
            textBoxLicensingText.Text = text?.Trim().Replace("\n", "\r\n");
            htmlPanelLicensingText.Text = (format == "HTML") ? text : null;
            richTextBoxLicensingText.Rtf = (format == "RTF") ? text : null;

            tabControlLicensingDetailsText.SelectedTab = format switch
            {
                "HTML" => tabPageLicensingHtml,
                "RTF" => tabPageLicensingRtf,
                _ => tabPageLicensingRaw,
            };
        } // DisplayTextProperty

        private void DisplayTextProperty(FormattedMultilineText text)
        {
            if (text is null)
            {
                DisplayTextProperty(null, null);
            }
            else
            {
                DisplayTextProperty(text.Text, text.Format);
            } // if-else
        } // DisplayTextProperty

        private void treeViewLicensingData_AfterSelect(object sender, TreeViewEventArgs e)
        {
            _selectedTag = e.Node.Tag;
            switch (e.Node.Tag)
            {
                case LicensedItem licensed:
                    SafeCall(OnLicensedSelected, licensed);
                    break;

                case LibraryDependency library:
                    SafeCall(OnDependenciesLibrarySelected, library);
                    break;

                case ThirdPartyDependency component:
                    SafeCall(OnDependenciesComponentSelected, component);
                    break;

                case License license:
                    SafeCall(OnLicenseSelected, license);
                    break;

                case TermsAndConditions terms:
                    SafeCall(OnTermsAndConditionsSelected, terms);
                    break;

                case FormattedMultilineText text:
                    SafeCall(OnFormattedTextSelected, text);
                    break;

                default:
                    DetailsMode = DetailsModeEnum.None;
                    break;
            } // switch
        } // treeViewLicensingData_AfterSelect

        private void OnLicensedSelected(LicensedItem licensed)
        {
            DetailsMode = DetailsModeEnum.Licensed;

            propertiesViewer.Properties = new[]
            {
                new Property("Name", licensed.Name),
                new Property("Type", licensed.Type.ToString()),
                new Property("Assembly", licensed.Assembly),
                new Property("Product", licensed.Product),
                new Property("LicenseId", licensed.LicenseId),
                new Property("Copyright", licensed.Copyright),
                new Property("Authors", licensed.Authors),
                new Property("TermsConditions", (licensed.TermsConditionsSpecified).ToString(CultureInfo.CurrentUICulture)),
                new Property("Remarks", string.IsNullOrEmpty(licensed.Remarks) ? null : $"Format: {licensed.Remarks.Format ?? "(default)"}"),
                new Property("Notes", string.IsNullOrEmpty(licensed.Notes) ? null : $"Format: {licensed.Notes.Format ?? "(default)"}"),
            };
        } // OnLicensed

        private void OnDependenciesLibrarySelected(LibraryDependency library)
        {
            DetailsMode = DetailsModeEnum.Library;

            propertiesViewer.Properties = new[]
            {
                new Property("Name", library.Name),
                new Property("Type", library.Type.ToString()),
                new Property("DependencyType", library.DependencyType.ToString()),
                new Property("Assembly", library.Assembly),
                new Property("LicenseId", library.LicenseId),
                new Property("Copyright", library.Copyright),
                new Property("Authors", library.Authors),
                new Property("Remarks", string.IsNullOrEmpty(library.Remarks) ? null : $"Format: {library.Remarks.Format ?? "(default)"}"),
            };
        } // OnDependenciesLibrarySelected

        private void OnDependenciesComponentSelected(ThirdPartyDependency component)
        {
            DetailsMode = DetailsModeEnum.ThirdParty;

            propertiesViewer.Properties = new[]
            {
                new Property("Name", component.Name),
                new Property("Type", component.Type.ToString()),
                new Property("DependencyType", component.DependencyType.ToString()),
                new Property("LicenseId", component.LicenseId),
                new Property("Copyright", component.Copyright),
                new Property("Authors", component.Authors),
                new Property("Description", component.Description),
                new Property("Remarks", string.IsNullOrEmpty(component.Remarks) ? null : $"Format: {component.Remarks.Format ?? "(default)"}"),
            };
        } // OnDependenciesComponentSelected

        private void OnLicenseSelected(License license)
        {
            DetailsMode = DetailsModeEnum.License;
            DisplayTextProperty(license.Text, license.Format);

            propertiesViewer.Properties = new[]
            {
                new Property("Id", license.Id),
                new Property("Name", license.Name),
                new Property("Format", license.Format),
            };
        } // OnLicenseSelected

        private void OnTermsAndConditionsSelected(TermsAndConditions terms)
        {
            DetailsMode = DetailsModeEnum.TermsConditions;
            DisplayTextProperty(terms.Text, terms.Format);

            propertiesViewer.Properties = new[]
            {
                new Property("Language", terms.Language),
                new Property("Format", terms.Format),
            };
        } // OnTermsAndConditionsSelected

        private void OnFormattedTextSelected(FormattedMultilineText text)
        {
            DetailsMode = DetailsModeEnum.FormattedText;
            DisplayTextProperty(text.Text, text.Format);

            propertiesViewer.Properties = new[]
            {
                new Property("Format", text.Format),
            };
        } // OnFormattedTextSelected

        private void propertiesViewer_PropertySelected(object sender, PropertySelectedEventArgs e)
        {
            switch (DetailsMode)
            {
                case DetailsModeEnum.Licensed:
                    var licensed = (LicensedItem)_selectedTag;
                    switch (e.Name)
                    {
                        case "Remarks":
                            DisplayTextProperty(licensed.Remarks);
                            return;
                        case "Notes":
                            DisplayTextProperty(licensed.Notes);
                            return;
                    } // switch
                    DisplayTextProperty(e.Value, null);
                    break;

                case DetailsModeEnum.Library:
                    var library = (LibraryDependency)_selectedTag;
                    switch (e.Name)
                    {
                        case "Remarks":
                            DisplayTextProperty(library.Remarks);
                            return;
                    } // switch
                    DisplayTextProperty(e.Value, null);
                    break;

                case DetailsModeEnum.ThirdParty:
                    var component = (ThirdPartyDependency)_selectedTag;
                    switch (e.Name)
                    {
                        case "Remarks":
                            DisplayTextProperty(component.Remarks);
                            return;
                    } // switch
                    DisplayTextProperty(e.Value, null);
                    break;
            } // switch
        } // propertiesViewer_PropertySelected
    } // class LicensingDataViewer
} // namespace

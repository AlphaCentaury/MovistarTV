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

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using IpTviewr.Internal.Tools.UiFramework.Properties;

namespace IpTviewr.Internal.Tools.UiFramework
{
    public partial class SelectToolDialog : Form
    {
        private string _guiDescription;
        private string _cliDescription;

        public SelectToolDialog()
        {
            InitializeComponent();
        } // constructor

        public Lazy<IGuiTool, IToolMetadata> SelectedGuiTool { get; private set; }

        public Lazy<ICliTool, IToolMetadata> SelectedCliTool { get; private set; }

        public Image OkButtonImage
        {
            get => buttonOk.Image;
            set => buttonOk.Image = value;
        } // OkButtonImage

        public Image CancelButtonImage
        {
            get => buttonCancel.Image;
            set => buttonCancel.Image = value;
        } // CancelButtonImage

        public bool SelectCliTool { get; set; }

        protected override void OnLoad(EventArgs e)
        {
            buttonUsage.Enabled = false;

            imageListSmall.Images.Add("Gui", Resources.GuiTool_24x);
            imageListSmall.Images.Add("Cli", Resources.GuiTool_24x);

            FillList<IGuiTool, IToolMetadata, IGuiToolDataProvider, GuiToolData>(listViewGuiTools,
                ToolsContainer.Current.GetSortedGuiTools(CultureInfo.CurrentCulture),
                true);

            FillList<ICliTool, IToolMetadata, ICliToolDataProvider, CliToolData>(listViewGuiTools,
                ToolsContainer.Current.GetSortedCliTools(CultureInfo.CurrentCulture),
                false);

            kryptonPageGuiTools.Visible = listViewGuiTools.Items.Count > 0;
            kryptonPageCliTools.Visible = listViewCliTools.Items.Count > 0;

            if (kryptonPageGuiTools.Visible || kryptonPageCliTools.Visible)
            {
                kryptonNavigator.SelectedPage = (SelectCliTool && kryptonPageCliTools.Visible) ? kryptonPageCliTools : kryptonPageGuiTools;
                buttonOk.Enabled = true;
            }
            else
            {
                kryptonPageGuiTools.Visible = true;
                listViewGuiTools.Items.Add("No tools are available", 0);
                buttonOk.Enabled = false;
                kryptonPageCliTools.Visible = false;
                splitContainer.Panel2Collapsed = true;
            } // if-else
            kryptonNavigator.SelectedPage = (SelectCliTool) ? kryptonPageCliTools : kryptonPageGuiTools;
        } // DialogSelectTool_Load

        private void kryptonNavigator_SelectedPageChanged(object sender, EventArgs e)
        {
            if (kryptonNavigator.SelectedPage == kryptonPageGuiTools)
            {
                textBoxInfo.Text = _guiDescription;
                buttonUsage.Visible = false;
                buttonOk.Enabled = listViewGuiTools.SelectedIndices.Count > 0;
                SelectCliTool = false;
            }
            else if (kryptonNavigator.SelectedPage == kryptonPageCliTools)
            {
                textBoxInfo.Text = _cliDescription;
                buttonUsage.Visible = true;
                buttonOk.Enabled = listViewCliTools.SelectedIndices.Count > 0;
                SelectCliTool = true;
            } // if-else if
        } // kryptonNavigator_SelectedPageChanged

        private void listViewGuiTools_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectedGuiTool = (Lazy<IGuiTool, IToolMetadata>)((listViewGuiTools.SelectedIndices.Count > 0) ? listViewGuiTools.SelectedItems[0].Tag : null);
            _guiDescription = GetToolDescription(SelectedGuiTool);
            textBoxInfo.Text = _guiDescription;
        } // listViewGuiTools_SelectedIndexChanged

        private void listViewCliTools_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectedCliTool = (Lazy<ICliTool, IToolMetadata>)((listViewCliTools.SelectedIndices.Count > 0) ? listViewCliTools.SelectedItems[0].Tag : null);
            _cliDescription = GetToolDescription(SelectedCliTool);
            textBoxInfo.Text = _cliDescription;
        } // listViewGuiTools_SelectedIndexChanged

        private void listViewGuiTools_DoubleClick(object sender, EventArgs e)
        {
            buttonOk.PerformClick();
        } // listViewGuiTools_DoubleClick

        private void listViewCliTools_DoubleClick(object sender, EventArgs e)
        {
            buttonOk.PerformClick();
        } // listViewCliTools_DoubleClick

        private void buttonUsage_Click(object sender, EventArgs e)
        {
            /*
            var writer = new Helpers.TextBoxOutputWriter(textBoxInfo);
            writer.Start();
            SelectedTool.Value.ShowUsage(writer);
            writer.Stop();
            */
        } // buttonUsage_Click

        private void buttonOk_Click(object sender, EventArgs e)
        {
            if (kryptonNavigator.SelectedPage == kryptonPageGuiTools)
            {
                SelectedCliTool = null;
            }
            else if (kryptonNavigator.SelectedPage == kryptonPageCliTools)
            {
                SelectedGuiTool = null;
            } // if-else if
        } // buttonOk_Click

        private void listViewGuiTools_Resize(object sender, EventArgs e)
        {
            columnHeaderGuiToolName.Width = listViewGuiTools.ClientSize.Width;
        } // listViewGuiTools_Resize

        private void listViewCliTools_Resize(object sender, EventArgs e)
        {
            columnHeaderCliToolName.Width = listViewCliTools.ClientSize.Width;
        } // listViewCliTools_Resize

        private void FillList<TTool, TMetadata, TProvider, TToolData>(ListView listView,
            IEnumerable<(string Category, TToolData[] Data)> tools,
            bool isGui)
            where TTool : ITool
            where TMetadata : IToolMetadata
            where TProvider : IToolDataProvider
            where TToolData : ToolData<TTool, TMetadata, TProvider>
        {
            listView.BeginUpdate();
            foreach (var (category, toolData) in tools)
            {
                var group = listView.Groups.Add(category ?? "", category);
                foreach (var data in toolData)
                {
                    var image = data.GetLogo(imageListSmall.ImageSize.Width);
                    var imgIndex = (image == null) ? (isGui ? 0 : 1) : imageListSmall.Images.Count;
                    if (image != null) imageListSmall.Images.Add(data.Guid.ToString("N") + isGui, image);

                    var listItem = new ListViewItem(data.Name, group)
                    {
                        Tag = data.Tool,
                        ImageIndex = imgIndex,
                    };

                    listView.Items.Add(listItem);
                } // foreach data
            } // foreach item

            if (listView.Items.Count > 0)
            {
                listView.Items[0].Selected = true;
            }
            else
            {
                listView.Enabled = false;
                buttonOk.Enabled = false;
            } // if-else

            listView.EndUpdate();
        } // FillList

        private string GetToolDescription(Lazy<IGuiTool, IToolMetadata> selectedGuiTool)
        {
            // TODO: implement

            return "";
        } // GetToolDescription

        private string GetToolDescription(Lazy<ICliTool, IToolMetadata> selectedCliTool)
        {
            // TODO: implement

            return "";
        } // GetToolDescription
    } // class SelectToolDialog
} // namespace

using Project.DvbIpTv.Common.Telemetry;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Project.DvbIpTv.UiServices.Configuration
{
    public partial class ConfigurationForm : Form
    {
        private ListViewItem SelectedConfigurationListItem;

        private class ConfigurationItem
        {
            public IConfigurationItemRegistration Registration;
            public IConfigurationItem ExistingData;
            public IConfigurationItem NewData;
            public IConfigurationItemEditor Editor;
        } // ConfigurationItem

        public static DialogResult ShowConfigurationForm(IWin32Window owner, bool autoSave = true)
        {
            DialogResult result;
            bool changed;

            var q = from item in AppUiConfiguration.Current.ItemsRegistry
                    let registration = item.Value
                    orderby registration.EditorPriority
                    select new ConfigurationItem()
                    {
                        Registration = registration,
                        ExistingData = AppUiConfiguration.Current[registration.DirectIndex]
                    };
            var items = q.ToList();

            using (var form = new ConfigurationForm())
            {
                BasicGoogleTelemetry.SendScreenHit(form, "(default)");
                form.ConfigurationItems = items;
                result = form.ShowDialog(owner);
                if (result != DialogResult.OK)
                {
                    return result;
                } // if
            } // using

            // save new settings
            changed = false;
            foreach (var item in items)
            {
                if (item.NewData == null) continue;
                changed = true;
                AppUiConfiguration.Current[item.Registration.DirectIndex] = item.NewData;    
            } // foreach
            if ((changed) && (autoSave))
            {
                AppUiConfiguration.Current.Save();
            } // if

            return result;
        } // ShowConfigurationForm

        public static T ShowConfigurationForm<T>(IWin32Window owner, Guid id, T overrideSettings) where T : class, IConfigurationItem
        {
            var registration = AppUiConfiguration.Current.ItemsRegistry[id];
            var data = new ConfigurationItem()
            {
                Registration = registration,
                ExistingData = overrideSettings ?? AppUiConfiguration.Current[registration.DirectIndex]
            };
            var items = new List<ConfigurationItem>(1);
            items.Add(data);

            

            using (var form = new ConfigurationForm())
            {
                BasicGoogleTelemetry.SendScreenHit(form, data.Registration.ItemType.Name);
                form.ConfigurationItems = items;
                var dialogResult = form.ShowDialog(owner);
                if (dialogResult != DialogResult.OK)
                {
                    return null;
                }
                else
                {
                    return (T)items[0].NewData;
                } // if-else
            } // using
        } // ShowConfigurationForm

        public ConfigurationForm()
        {
            InitializeComponent();
            ConfigurationItems = new List<ConfigurationItem>();
        } // constructor

        private IList<ConfigurationItem> ConfigurationItems
        {
            get;
            set;
        } // ConfigurationItems

        private void ConfigurationForm_Load(object sender, EventArgs e)
        {
            listViewConfigItems.TileSize = new Size(listViewConfigItems.Width -SystemInformation.VerticalScrollBarWidth - 2, listViewConfigItems.LargeImageList.ImageSize.Height + 6);

            foreach (var configItem in ConfigurationItems)
            {
                var registration = configItem.Registration;
                using (var img = registration.EditorImage)
                {
                    listViewConfigItems.LargeImageList.Images.Add(img);
                } // using

                configItem.Editor = registration.CreateEditor(AppUiConfiguration.CloneSettings<IConfigurationItem>(configItem.ExistingData));

                var item = new ListViewItem(registration.EditorDisplayName);
                item.ImageIndex = imageListConfigItems.Images.Count - 1;
                item.Tag = configItem.Editor;

                listViewConfigItems.Items.Add(item);
            } // foreach

            if (listViewConfigItems.Items.Count > 0)
            {
                listViewConfigItems.Items[0].Selected = true;
            } // if
        }  // ConfigurationForm_Load

        private void ConfigurationForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            bool cancelClose;

            if (DialogResult == DialogResult.OK)
            {
                foreach (var configItem in ConfigurationItems)
                {
                    configItem.Editor.EditorClosing(out cancelClose);
                    if (cancelClose)
                    {
                        e.Cancel = true;
                        return;
                    } // if
                } // foreach
            } // if
        } // ConfigurationForm_FormClosing

        private void ConfigurationForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            foreach (var editor in ConfigurationItems)
            {
                editor.Editor.EditorClosed(DialogResult != DialogResult.OK);
            } // foreach editorData
        } // ConfigurationForm_FormClosed

        private void listViewConfigItems_SelectedIndexChanged(object sender, EventArgs e)
        {
            var newSelection = (listViewConfigItems.SelectedItems.Count > 0) ? listViewConfigItems.SelectedItems[0] : null;
            if (newSelection == null) return;

            SelectedConfigurationListItem = newSelection;
            var configItem = SelectedConfigurationListItem.Tag as IConfigurationItemEditor;

            panelConfigItemUi.Controls.Clear();
            var ui = configItem.UserInterfaceItem;
            panelConfigItemUi.Controls.Add(ui);
            ui.Dock = DockStyle.Fill;
        } // listViewConfigItems_SelectedIndexChanged

        private void buttonOk_Click(object sender, EventArgs e)
        {
            buttonOk.DialogResult = DialogResult.None;
            foreach (var configItem in ConfigurationItems)
            {
                if (configItem.Editor.IsDataChanged)
                {
                    if (configItem.Editor.SupportsWinFormsValidation)
                    {
                        if (!configItem.Editor.UserInterfaceItem.Validate(false)) return;
                    }
                    else
                    {
                        if (!configItem.Editor.Validate()) return;
                    } // if-else
                } // if
            } // foreach

            foreach (var configItem in ConfigurationItems)
            {
                if (configItem.Editor.IsDataChanged)
                {
                    configItem.NewData = configItem.Editor.GetNewData();
                } // if
            } // foreach

            buttonOk.DialogResult = DialogResult.OK;
        } // buttonOk_Click
    } // class ConfigurationForm
} // namespace

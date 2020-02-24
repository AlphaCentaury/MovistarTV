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

using IpTviewr.Common.Telemetry;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace IpTviewr.UiServices.Configuration
{
    public partial class ConfigurationForm : Form
    {
        private ListViewItem _selectedConfigurationListItem;

        private class ConfigurationItem
        {
            public IConfigurationItemRegistration Registration;
            public IConfigurationItem ExistingData;
            public IConfigurationItem NewData;
            public IConfigurationItemEditor Editor;
        } // ConfigurationItem

        public static DialogResult ShowConfigurationForm(IWin32Window owner, bool autoSave = true, IDictionary<Guid, Action> applyChanges = null)
        {
            DialogResult result;
            bool changed;

            var q = from item in AppConfig.Current.ItemsRegistry
                    let registration = item.Value
                    where registration.HasEditor
                    orderby registration.EditorPriority
                    select new ConfigurationItem()
                    {
                        Registration = registration,
                        ExistingData = AppConfig.Current[registration.DirectIndex]
                    };
            var items = q.ToList();

            using (var form = new ConfigurationForm())
            {
                AppTelemetry.FormEvent(AppTelemetry.LoadEvent, form, "(default)");
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
                var newData = item.NewData;
                if (newData == null) continue;

                changed = true;
                AppConfig.Current[item.Registration.DirectIndex] = newData;
            } // foreach

            // autosave if settings changed
            if ((changed) && (autoSave))
            {
                AppConfig.Current.Save();
            } // if

            // apply changes
            if ((changed) && (applyChanges != null) && (applyChanges.Count > 0))
            {
                foreach (var item in items)
                {
                    if (item.NewData == null) continue;

                    if (applyChanges.TryGetValue(item.Registration.Id, out var applyChangesMethod))
                    {
                        applyChangesMethod();
                    } // if
                } // foreach
            } // if

            return result;
        } // ShowConfigurationForm

        public static T ShowConfigurationForm<T>(IWin32Window owner, string settingsGuid, T overrideSettings) where T : class, IConfigurationItem
        {
            var registration = AppConfig.Current.ItemsRegistry[new Guid(settingsGuid)];
            var data = new ConfigurationItem()
            {
                Registration = registration,
                ExistingData = overrideSettings ?? AppConfig.Current[registration.DirectIndex]
            };
            var items = new List<ConfigurationItem>(1);
            items.Add(data);

            using (var form = new ConfigurationForm())
            {
                AppTelemetry.FormEvent(AppTelemetry.LoadEvent, form, data.Registration.ItemType.Name);
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

        public bool IsAppRestartNeeded
        {
            get;
            private set;
        } // IsAppRestartNeeded

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

                configItem.Editor = registration.CreateEditor(AppConfig.CloneSettings<IConfigurationItem>(configItem.ExistingData));

                var item = new ListViewItem(registration.EditorDisplayName)
                {
                    ImageIndex = imageListConfigItems.Images.Count - 1,
                    Tag = configItem.Editor
                };

                listViewConfigItems.Items.Add(item);
            } // foreach

            if (listViewConfigItems.Items.Count > 0)
            {
                listViewConfigItems.Items[0].Selected = true;
            } // if
        }  // ConfigurationForm_Load

        private void ConfigurationForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult == DialogResult.OK)
            {
                foreach (var configItem in ConfigurationItems)
                {
                    if (configItem.Editor == null) return;

                    configItem.Editor.EditorClosing(out var cancelClose);
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
            foreach (var configItem in ConfigurationItems)
            {
                if (configItem.Editor == null) return;

                configItem.Editor.EditorClosed(DialogResult != DialogResult.OK);
            } // foreach editorData
        } // ConfigurationForm_FormClosed

        private void listViewConfigItems_SelectedIndexChanged(object sender, EventArgs e)
        {
            var newSelection = (listViewConfigItems.SelectedItems.Count > 0) ? listViewConfigItems.SelectedItems[0] : null;
            if (newSelection == null) return;

            _selectedConfigurationListItem = newSelection;
            var configItem = _selectedConfigurationListItem.Tag as IConfigurationItemEditor;

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
                if (configItem.Editor == null) continue;

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

            IsAppRestartNeeded = false;
            foreach (var configItem in ConfigurationItems)
            {
                if (configItem.Editor == null) continue;

                if (configItem.Editor.IsDataChanged)
                {
                    configItem.NewData = configItem.Editor.GetNewData();
                    IsAppRestartNeeded |= configItem.Editor.IsAppRestartNeeded;
                } // if
            } // foreach

            if (IsAppRestartNeeded)
            {
                MessageBox.Show(this, Properties.SettingsTexts.ConfigFormAppRestartRequired, Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            } // if

            buttonOk.DialogResult = DialogResult.OK;
        } // buttonOk_Click
    } // class ConfigurationForm
} // namespace

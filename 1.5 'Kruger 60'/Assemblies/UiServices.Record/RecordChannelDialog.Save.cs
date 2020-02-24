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

using IpTviewr.UiServices.Configuration;
using IpTviewr.UiServices.Configuration.Schema2014.Config;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace IpTviewr.UiServices.Record
{
    partial class RecordChannelDialog
    {
        private const string ListLocationsSelectedImageKey = "selected";
        private const string ListLocationsDefaultImageKey = "folder";

        #region "Save" tab events / setup & get data

        private void InitSaveData()
        {
            // Fill extensions combo
            comboFileExtension.Items.AddRange(GetFilenameExtensions());

            // Name (filename)
            textFilename.SetText(string.IsNullOrEmpty(Task.Action.Filename) ? Task.Channel.Name : Task.Action.Filename, false);

            // Extension
            comboFileExtension.Text = string.IsNullOrEmpty(Task.Action.FileExtension) ? comboFileExtension.Items[0] as string : Task.Action.FileExtension;

            // Save locations
            var defaultItem = SetListLocations(AppConfig.Current.User.Record.SaveLocations);
            SelectSaveLocation(Task.Action.SaveLocationName, Task.Action.SaveLocationPath, defaultItem);
        } // InitSaveData

        private void GetSaveData()
        {
            var locationName = listViewLocations.SelectedItems[0].SubItems[0].Text;
            if (locationName == Properties.RecordChannel.SaveDefaultLocation) locationName = null;
            var locationPath = listViewLocations.SelectedItems[0].SubItems[1].Text;

            Task.Action.Filename = textFilename.Text.Trim();
            Task.Action.FileExtension = comboFileExtension.Text.Trim();
            Task.Action.SaveLocationName = locationName;
            Task.Action.SaveLocationPath = locationPath;
            Task.Action.Recorder = RecordHelper.GetDefaultRecorder();
        } // GetSaveData

        private void textFilename_Validating(object sender, CancelEventArgs e)
        {
            var text = textFilename.Text.Trim();
            e.Cancel = (text.Length == 0);

            if (e.Cancel)
            {
                ControlValidationFailed(Properties.RecordChannel.EmptyFileName, sender as Control);
            } // if
        } // textFilename_Validating

        private void comboFileExtension_Validating(object sender, CancelEventArgs e)
        {
            var text = comboFileExtension.Text.Trim();
            e.Cancel = (text.Length == 0);

            if (e.Cancel)
            {
                ControlValidationFailed(Properties.RecordChannel.EmptyFileExtension, sender as Control);
            } // if
        } // comboFileExtension_Validating

        private void listViewLocations_SelectedIndexChanged(object sender, EventArgs e)
        {
            _currentSelectedLocationIndex = (listViewLocations.SelectedIndices.Count > 0) ? listViewLocations.SelectedIndices[0] : -1;

            for (var index = 0; index < listViewLocations.Items.Count; index++)
            {
                var item = listViewLocations.Items[index];
                if (item.Index == _currentSelectedLocationIndex)
                {
                    item.ImageKey = ListLocationsSelectedImageKey;
                }
                else if (item.ImageKey != ListLocationsDefaultImageKey)
                {
                    item.ImageKey = ListLocationsDefaultImageKey;
                } // if-else
            } // for index
        } // listViewLocations_SelectedIndexChanged

        private void listViewLocations_Validating(object sender, CancelEventArgs e)
        {
            e.Cancel = (listViewLocations.SelectedIndices.Count == 0);
            if (e.Cancel)
            {
                ControlValidationFailed(Properties.RecordChannel.NoSaveLocation, sender as Control);
            } // if
        } // listViewLocations_Validating

        private void buttonSelectFolder_Click(object sender, EventArgs e)
        {
            if (listViewLocations.SelectedItems.Count == 0)
            {
                selectFolder.SelectedPath = null;
            }
            else
            {
                selectFolder.SelectedPath = listViewLocations.SelectedItems[0].SubItems[1].Text;
            } // if-else

            if (selectFolder.ShowDialog(this) != DialogResult.OK) return;

            SelectAppendSaveLocation(selectFolder.SelectedPath);
        } // buttonSelectFolder_Click

        private ListViewItem SetListLocations(IList<RecordSaveLocation> locations)
        {
            ListViewItem defaultItem;

            if (locations == null) throw new ArgumentNullException();

            defaultItem = null;
            listViewLocations.BeginUpdate();
            listViewLocations.Items.Clear();

            _currentSelectedLocationIndex = (locations.Count == 0) ? -1 : 0;
            for (var index = 0; index < locations.Count; index++)
            {
                var name = locations[index].Name;
                if (string.IsNullOrEmpty(name)) name = Properties.RecordChannel.SaveDefaultLocation;

                var item = listViewLocations.Items.Add(name);
                item.ImageKey = ListLocationsDefaultImageKey;
                item.SubItems.Add(locations[index].Path.Trim());

                if (string.IsNullOrEmpty(locations[index].Name)) defaultItem = item;
            } // for

            var sortColumn = (listViewLocations.CurrentSortColumn < 0) ? 0 : listViewLocations.CurrentSortColumn;
            listViewLocations.Sort(sortColumn, !listViewLocations.CurrentSortIsDescending);
            listViewLocations.EndUpdate();

            return defaultItem;
        } // SetListLocations

        private void SelectSaveLocation(string locationName, string path, ListViewItem defaultItem)
        {
            ListViewItem item;

            if ((locationName == null) && (!string.IsNullOrEmpty(path)))
            {
                locationName = Properties.RecordChannel.SaveDefaultLocation;
            } // if

            var find = from ListViewItem listItem in listViewLocations.Items
                       where string.Compare(listItem.Text, locationName, StringComparison.InvariantCultureIgnoreCase) == 0
                       select listItem;
            item = find.FirstOrDefault();

            if (item == null)
            {
                if (string.IsNullOrEmpty(path))
                {
                    if (listViewLocations.Items.Count > 0)
                    {
                        item = defaultItem;
                    } // if
                }
                else
                {
                    locationName = (locationName != null) ? Properties.RecordChannel.SaveMissingLocation : Properties.RecordChannel.SaveCustomLocation;
                    item = listViewLocations.Items.Add(locationName);
                    item.ImageKey = ListLocationsDefaultImageKey;
                    item.SubItems.Add(Task.Action.SaveLocationPath);
                } // if-else
            } // if-else

            if (item == null)
            {
                _currentSelectedLocationIndex = -1;
            }
            else
            {
                item.Selected = true;
                _currentSelectedLocationIndex = item.Index;
            } // if-else
        } // SelectSaveLocation

        private void SelectAppendSaveLocation(string path)
        {
            var find = from ListViewItem listItem in listViewLocations.Items
                       where string.Compare(listItem.SubItems[1].Text, path, StringComparison.InvariantCultureIgnoreCase) == 0
                       select listItem;
            var item = find.FirstOrDefault();

            if (item == null)
            {
                item = listViewLocations.Items.Add(Properties.RecordChannel.SaveCustomLocation);
                item.ImageKey = ListLocationsDefaultImageKey;
                item.SubItems.Add(path);
            } // if

            item.Selected = true;
            _currentSelectedLocationIndex = item.Index;
        } // SelectAppendSaveLocation

        #endregion
    } // partial class RecordChannelDialog
} // namespace

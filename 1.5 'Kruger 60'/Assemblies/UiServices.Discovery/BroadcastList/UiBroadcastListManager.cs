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
using System.Linq;
using System.Windows.Forms;
using System.Drawing;
using IpTviewr.UiServices.Common.Controls;
using IpTviewr.UiServices.Configuration.Logos;
using IpTviewr.UiServices.Configuration;
using IpTviewr.Common.Serialization;

namespace IpTviewr.UiServices.Discovery.BroadcastList
{
    public class UiBroadcastListManager : IDisposable
    {
        public const LogoSize SmallLogoSize = LogoSize.Size32;
        public const LogoSize LargeLogoSize = LogoSize.Size48;

        private IList<UiBroadcastService> _broadcastServices;
        private IList<UiBroadcastService> _displayedBroadcastServices;
        private IEnumerable<UiBroadcastService> _sortedBroadcastServices;
        private UiBroadcastListSettings _oldSettings;
        private UiBroadcastListSettings _settings;
        private UiBroadcastService _selectedService;
        private const int MinColumnWidth = 25;
        private readonly ImageList _smallImageList;
        private readonly ImageList _largeImageList;

        // TODO: Use fonts from Settings
        private readonly Font _fontDetails;
        private readonly Font _fontDetailsBold;
        private readonly Font _fontNormal;
        private readonly Font _fontNormalHidden;
        private readonly Font _fontNormalNotAvailable;

        #region Static public methods

        public static List<KeyValuePair<UiBroadcastListColumn, string>> GetColumnNames(bool withNone = false)
        {
            var result = new List<KeyValuePair<UiBroadcastListColumn, string>>(withNone ? 22 : 21);

            if (withNone)
            {
                result.Add(new KeyValuePair<UiBroadcastListColumn, string>(UiBroadcastListColumn.None, Properties.ListViewManager.ColumnNone));
            } // if
            result.Add(new KeyValuePair<UiBroadcastListColumn, string>(UiBroadcastListColumn.Name, Properties.ListViewManager.ColumnName));
            result.Add(new KeyValuePair<UiBroadcastListColumn, string>(UiBroadcastListColumn.Number, Properties.ListViewManager.ColumnNumber));
            result.Add(new KeyValuePair<UiBroadcastListColumn, string>(UiBroadcastListColumn.NumberAndName, Properties.ListViewManager.ColumnNumberAndName));
            result.Add(new KeyValuePair<UiBroadcastListColumn, string>(UiBroadcastListColumn.NumberAndNameCrlf, Properties.ListViewManager.ColumnNumberAndNameCrlf));
            result.Add(new KeyValuePair<UiBroadcastListColumn, string>(UiBroadcastListColumn.NameAndNumber, Properties.ListViewManager.ColumnNameAndNumber));
            result.Add(new KeyValuePair<UiBroadcastListColumn, string>(UiBroadcastListColumn.Description, Properties.ListViewManager.ColumnDescription));
            result.Add(new KeyValuePair<UiBroadcastListColumn, string>(UiBroadcastListColumn.DvbType, Properties.ListViewManager.ColumnDvbType));
            result.Add(new KeyValuePair<UiBroadcastListColumn, string>(UiBroadcastListColumn.LocationUrl, Properties.ListViewManager.ColumnLocationUrl));
            result.Add(new KeyValuePair<UiBroadcastListColumn, string>(UiBroadcastListColumn.ShortName, Properties.ListViewManager.ColumnShortName));
            result.Add(new KeyValuePair<UiBroadcastListColumn, string>(UiBroadcastListColumn.Genre, Properties.ListViewManager.ColumnGenre));
            result.Add(new KeyValuePair<UiBroadcastListColumn, string>(UiBroadcastListColumn.GenreCode, Properties.ListViewManager.ColumnGenreCode));
            result.Add(new KeyValuePair<UiBroadcastListColumn, string>(UiBroadcastListColumn.ParentalRating, Properties.ListViewManager.ColumnParentalRating));
            result.Add(new KeyValuePair<UiBroadcastListColumn, string>(UiBroadcastListColumn.ParentalRatingCode, Properties.ListViewManager.ColumnParentalRatingCode));
            result.Add(new KeyValuePair<UiBroadcastListColumn, string>(UiBroadcastListColumn.ServiceId, Properties.ListViewManager.ColumnServiceId));
            result.Add(new KeyValuePair<UiBroadcastListColumn, string>(UiBroadcastListColumn.FullServiceId, Properties.ListViewManager.ColumnFullServiceId));
            result.Add(new KeyValuePair<UiBroadcastListColumn, string>(UiBroadcastListColumn.UserName, Properties.ListViewManager.ColumnUserName));
            result.Add(new KeyValuePair<UiBroadcastListColumn, string>(UiBroadcastListColumn.UserNumber, Properties.ListViewManager.ColumnUserNumber));
            result.Add(new KeyValuePair<UiBroadcastListColumn, string>(UiBroadcastListColumn.OriginalName, Properties.ListViewManager.ColumnOriginalName));
            result.Add(new KeyValuePair<UiBroadcastListColumn, string>(UiBroadcastListColumn.OriginalNumber, Properties.ListViewManager.ColumnOriginalNumber));
            result.Add(new KeyValuePair<UiBroadcastListColumn, string>(UiBroadcastListColumn.IsActive, Properties.ListViewManager.ColumnIsActive));
            result.Add(new KeyValuePair<UiBroadcastListColumn, string>(UiBroadcastListColumn.IsEnabled, Properties.ListViewManager.ColumnIsEnabled));
            result.Add(new KeyValuePair<UiBroadcastListColumn, string>(UiBroadcastListColumn.LockLevel, Properties.ListViewManager.ColumnLockLevel));

            return result;
        } // ColumnNames

        public static List<KeyValuePair<UiBroadcastListColumn, string>> GetSortedColumnNames(bool withNone = false)
        {
            var names = GetColumnNames();
            var q = from item in names
                    orderby item.Value
                    select item;

            var count = withNone ? (names.Count + 1) : names.Count;
            var result = new List<KeyValuePair<UiBroadcastListColumn, string>>(count);
            if (withNone)
            {
                result.Add(new KeyValuePair<UiBroadcastListColumn, string>(UiBroadcastListColumn.None, Properties.ListViewManager.ColumnNone));
            } // if
            result.AddRange(q);

            return result;
        } // SortedColumnNames

        public static string GetColumnName(UiBroadcastListColumn column)
        {
            switch (column)
            {
                case UiBroadcastListColumn.Number: return Properties.ListViewManager.ColumnNumberShort;
                case UiBroadcastListColumn.Name: return Properties.ListViewManager.ColumnNameShort;
                case UiBroadcastListColumn.NumberAndName: return Properties.ListViewManager.ColumnNumberAndNameShort;
                case UiBroadcastListColumn.NumberAndNameCrlf: return Properties.ListViewManager.ColumnNumberAndNameShort;
                case UiBroadcastListColumn.NameAndNumber: return Properties.ListViewManager.ColumnNameAndNumberShort;
                case UiBroadcastListColumn.Description: return Properties.ListViewManager.ColumnDescriptionShort;
                case UiBroadcastListColumn.DvbType: return Properties.ListViewManager.ColumnDvbTypeShort;
                case UiBroadcastListColumn.LocationUrl: return Properties.ListViewManager.ColumnLocationUrlShort;
                case UiBroadcastListColumn.ShortName: return Properties.ListViewManager.ColumnShortNameShort;
                case UiBroadcastListColumn.Genre: return Properties.ListViewManager.ColumnGenreShort;
                case UiBroadcastListColumn.GenreCode: return Properties.ListViewManager.ColumnGenreCodeShort;
                case UiBroadcastListColumn.ParentalRating: return Properties.ListViewManager.ColumnParentalRatingShort;
                case UiBroadcastListColumn.ParentalRatingCode: return Properties.ListViewManager.ColumnParentalRatingCodeShort;
                case UiBroadcastListColumn.ServiceId: return Properties.ListViewManager.ColumnServiceIdShort;
                case UiBroadcastListColumn.FullServiceId: return Properties.ListViewManager.ColumnFullServiceIdShort;
                case UiBroadcastListColumn.UserName: return Properties.ListViewManager.ColumnUserNameShort;
                case UiBroadcastListColumn.UserNumber: return Properties.ListViewManager.ColumnUserNumberShort;
                case UiBroadcastListColumn.OriginalName: return Properties.ListViewManager.ColumnOriginalNameShort;
                case UiBroadcastListColumn.OriginalNumber: return Properties.ListViewManager.ColumnOriginalNumberShort;
                case UiBroadcastListColumn.IsActive: return Properties.ListViewManager.ColumnIsActiveShort;
                case UiBroadcastListColumn.IsEnabled: return Properties.ListViewManager.ColumnIsEnabledShort;
                case UiBroadcastListColumn.LockLevel: return Properties.ListViewManager.ColumnLockLevelShort;
                default:
                    throw new IndexOutOfRangeException();
            } // switch
        } // GetColumnName

        public static int GetColumnDefaultWidth(UiBroadcastListColumn column)
        {
            switch (column)
            {
                case UiBroadcastListColumn.Number: return 45;
                case UiBroadcastListColumn.Name: return 180;
                case UiBroadcastListColumn.NumberAndName: return 210;
                case UiBroadcastListColumn.NumberAndNameCrlf: return 210;
                case UiBroadcastListColumn.NameAndNumber: return 210;
                case UiBroadcastListColumn.Description: return 170;
                case UiBroadcastListColumn.DvbType: return 90;
                case UiBroadcastListColumn.LocationUrl: return 150;
                case UiBroadcastListColumn.ShortName: return 90;
                case UiBroadcastListColumn.Genre: return 150;
                case UiBroadcastListColumn.GenreCode: return 100;
                case UiBroadcastListColumn.ParentalRating: return 150;
                case UiBroadcastListColumn.ParentalRatingCode: return 100;
                case UiBroadcastListColumn.ServiceId: return 75;
                case UiBroadcastListColumn.FullServiceId: return 150;
                case UiBroadcastListColumn.UserName: return 180;
                case UiBroadcastListColumn.UserNumber: return 45;
                case UiBroadcastListColumn.OriginalName: return 180;
                case UiBroadcastListColumn.OriginalNumber: return 45;
                case UiBroadcastListColumn.IsActive: return 45;
                case UiBroadcastListColumn.IsEnabled: return 45;
                case UiBroadcastListColumn.LockLevel: return 120;
                default:
                    throw new IndexOutOfRangeException();
            } // switch
        } // GetColumnDefaultWidth

        public static string GetColumnData(UiBroadcastService service, UiBroadcastListColumn column)
        {
            switch (column)
            {
                case UiBroadcastListColumn.Number:
                    return service.DisplayLogicalNumber;
                case UiBroadcastListColumn.Name:
                    return service.DisplayName;
                case UiBroadcastListColumn.NumberAndName:
                    return string.Format(Properties.ListViewManager.NumberAndNameFormat, service.DisplayLogicalNumber, service.DisplayName);
                case UiBroadcastListColumn.NumberAndNameCrlf:
                    return string.Format(Properties.ListViewManager.NumberAndNameCrlfFormat, service.DisplayLogicalNumber, service.DisplayName);
                case UiBroadcastListColumn.NameAndNumber:
                    return string.Format(Properties.ListViewManager.NameAndNumberFormat, service.DisplayName, service.DisplayLogicalNumber);
                case UiBroadcastListColumn.Description:
                    return service.DisplayDescription;
                case UiBroadcastListColumn.DvbType:
                    return service.DisplayServiceType;
                case UiBroadcastListColumn.LocationUrl:
                    return service.DisplayLocationUrl;
                case UiBroadcastListColumn.ShortName:
                    return service.DisplayShortName;
                case UiBroadcastListColumn.Genre:
                    return service.DisplayGenre;
                case UiBroadcastListColumn.GenreCode:
                    return service.DisplayGenreCode;
                case UiBroadcastListColumn.ParentalRating:
                    return service.DisplayParentalRating;
                case UiBroadcastListColumn.ParentalRatingCode:
                    return service.DisplayParentalRatingCode;
                case UiBroadcastListColumn.ServiceId:
                    return service.ServiceName;
                case UiBroadcastListColumn.FullServiceId:
                    return service.FullServiceName;
                case UiBroadcastListColumn.UserName:
                    return service.UserDisplayName ?? "";
                case UiBroadcastListColumn.UserNumber:
                    return service.UserLogicalNumber ?? Properties.Texts.ChannelNumberNone;
                case UiBroadcastListColumn.OriginalName:
                    return service.DisplayOriginalName;
                case UiBroadcastListColumn.OriginalNumber:
                    return service.ServiceLogicalNumber ?? Properties.Texts.ChannelNumberNone;
                case UiBroadcastListColumn.IsActive:
                    return (service.IsInactive) ? Properties.Texts.No : Properties.Texts.Yes;
                case UiBroadcastListColumn.IsEnabled:
                    return (service.IsHidden) ? Properties.Texts.No : Properties.Texts.Yes;
                case UiBroadcastListColumn.LockLevel:
                    return service.DisplayLockLevel;
                default:
                    throw new IndexOutOfRangeException();
            } // switch
        } // GetColumnData

        #endregion

        public UiBroadcastListManager(ListViewSortable listView, UiBroadcastListSettings settings, ImageList smallImageList, ImageList largeImageList, bool overrideDesignSettings)
        {
            if (settings == null) throw new ArgumentNullException("settings");

            ListView = listView ?? throw new ArgumentNullException("listView");
            _smallImageList = smallImageList;
            _largeImageList = largeImageList;

            _fontNormal = new Font("Segoe UI Semibold", 10.0f, FontStyle.Regular);
            _fontNormalHidden = new Font("Segoe UI Semibold", 9.5f, FontStyle.Regular);
            _fontNormalNotAvailable = new Font("Segoe UI", 9, FontStyle.Regular);
            _fontDetails = new Font(ListView.Font.OriginalFontName, ListView.Font.SizeInPoints, FontStyle.Regular);
            _fontDetailsBold = new Font("Tahoma", 10.5f, FontStyle.Bold);

            InitListViewControl(overrideDesignSettings);
            ApplySettings(settings);
        } // constructor

        public event EventHandler<ListStatusChangedEventArgs> StatusChanged;
        public event EventHandler<ListSelectionChangedEventArgs> SelectionChanged;

        public bool Disposed
        {
            get;
            private set;
        } // Disposed

        public ListViewSortable ListView
        {
            get;
            protected set;
        } // ListView

        public IList<UiBroadcastService> BroadcastServices
        {
            get => _broadcastServices;
            set
            {
                _broadcastServices = value;
                _sortedBroadcastServices = null;
                _selectedService = null;
                ListView.Items.Clear();
                ApplySorting();
                FireStatusChanged();
                FireSelectionChanged();
            } // set
        } // BroadcastServices

        public UiBroadcastListSettings Settings
        {
            get => _settings;
            set => ApplySettings(value);
        } // Settings

        public UiBroadcastService SelectedService
        {
            get => _selectedService;
            set
            {
                var selected = value;

                // remove current selection
                for (var index = 0; index < ListView.SelectedItems.Count; index++)
                {
                    ListView.SelectedItems[0].Selected = false;
                } // for

                // select given item
                if (value != null)
                {
                    var listItem = ListView.Items[value.Key];
                    if (listItem != null)
                    {
                        listItem.Selected = true;
                        listItem.EnsureVisible();
                    }
                    else
                    {
                        selected = null;
                    } // if-else
                } // if

                // save selected service
                _selectedService = selected;
            } // set
        } // SelectedService

        public UiBroadcastListSortColumn SelectedSort
        {
            get;
            private set;
        } // SelectedSort

        public bool HasItems => BroadcastServices != null && BroadcastServices.Count > 0;

        public void Refesh()
        {
            _displayedBroadcastServices = null;
            ListView.BeginUpdate();
            FillList();
            ListView.EndUpdate();
        } // Refresh

        public bool EnableService(UiBroadcastService service, bool isInactive, bool isHidden)
        {
            if (service == null) throw new ArgumentNullException();

            // not changed?
            if ((service.IsInactive == isInactive) && (service.IsHidden == isHidden)) return true;

            // save requested status change
            service.IsInactive = isInactive;
            service.IsHidden = isHidden;

            // is int list?
            var item = ListView.Items[service.Key];
            if (item == null) return false;

            var remove = false;
            if ((isInactive) && (!Settings.ShowInactiveServices)) remove = true;
            if ((isHidden) && (!Settings.ShowHiddenServices)) remove = true;

            ListView.BeginUpdate();

            if (remove)
            {
                ListView.Items.Remove(item);
            }
            else
            {
                EnableListItem(item, service, Settings.CurrentColumns);
            } // if-else

            ListView.EndUpdate();

            return true;
        } // EnableService

        public IList<UiBroadcastService> GetDisplayedBroadcastList()
        {
            return _displayedBroadcastServices ?? (_displayedBroadcastServices = GetDisplayBroadcastList());
        } // GetDisplayedBroadcastList

        public UiBroadcastListSettings ShowSettingsEditor(IWin32Window owner, bool autoApplyChanges)
        {
            var result = ConfigurationForm.ShowConfigurationForm(owner, UiBroadcastListSettingsRegistration.RegistrationGuid, _settings);

            if ((result == null) || (!autoApplyChanges)) return result;
            SaveSettings(result);
            Settings = result;

            return result;
        } // ShowSettingsEditor

        #region Events

        protected virtual void OnStatusChanged(object sender, ListStatusChangedEventArgs e)
        {
            StatusChanged?.Invoke(sender, e);
        } // OnStatusChanged

        protected virtual void OnSelectionChanged(object sender, ListSelectionChangedEventArgs e)
        {
            SelectionChanged?.Invoke(sender, e);
        } // OnSelectionChanged

        private void FireStatusChanged()
        {
            OnStatusChanged(this, new ListStatusChangedEventArgs(ListView.Items.Count > 0));
            FireSelectionChanged();
        } // FireStatusChanged

        private void FireSelectionChanged()
        {
            var listItem = (ListView.SelectedItems.Count == 0) ? null : ListView.SelectedItems[0];
            _selectedService = (UiBroadcastService)listItem?.Tag;

            OnSelectionChanged(this, new ListSelectionChangedEventArgs(_selectedService));
        } // FireSelectionChanged

        #endregion

        #region IDisposable implementation

        public virtual void Dispose()
        {
            if (Disposed) return;

            HookupEvents(false);
            _broadcastServices = null;
            ListView.Items.Clear();
            ListView = null;

            _fontNormal?.Dispose();
            _fontNormalHidden?.Dispose();
            _fontNormalNotAvailable?.Dispose();
            _fontDetails?.Dispose();
            _fontDetailsBold?.Dispose();

            Disposed = true;
        } // Dispose

        #endregion

        #region ListView event handlers

        private void HookupEvents(bool add)
        {
            if (add)
            {
                ListView.SelectedIndexChanged += ListView_SelectedIndexChanged;
                ListView.ColumnClick += ListView_ColumnClick;
            }
            else
            {
                ListView.SelectedIndexChanged -= ListView_SelectedIndexChanged;
                ListView.ColumnClick -= ListView_ColumnClick;
            } // if-else
        }  // HookupEvents

        private void ListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            FireSelectionChanged();
        } // ListView_SelectedIndexChanged

        private void ListView_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            var sortColumn = new UiBroadcastListSortColumn
            {
                Column = Settings.CurrentColumns[e.Column]
            };
            sortColumn.IsAscending = sortColumn.Column == SelectedSort.Column ? !SelectedSort.IsAscending : true;

            if (Settings.UseGlobalSortColumns)
            {
                Settings.GlobalSortColumns = ServiceSortComparer.GetSuggestedSortColumns(sortColumn.Column, sortColumn.IsAscending, 3);
            }
            else
            {
                Settings[Settings.CurrentMode].Sort = ServiceSortComparer.GetSuggestedSortColumns(sortColumn.Column, sortColumn.IsAscending, 3);
            } // if-else

            SaveSettings(Settings);
            ApplySorting();
        } // ListView_ColumnClick

        #endregion

        #region ListView management

        private void InitListViewControl(bool overrideDesignSettings)
        {
            ListView.MultiSelect = false;
            ListView.SmallImageList = _smallImageList;
            ListView.LargeImageList = _largeImageList;
            ListView.SelfSorting = false;
            ListView.OwnerDraw = true;

            if (overrideDesignSettings)
            {
                ListView.HeaderStyle = ColumnHeaderStyle.Clickable;
                ListView.ShowItemToolTips = true;
                ListView.HeaderCustomFont = new Font(ListView.Font.OriginalFontName, 9, FontStyle.Bold);
                ListView.HeaderCustomTextAlignment = ContentAlignment.MiddleCenter;
                ListView.HeaderUsesCustomFont = true;
                ListView.HeaderUsesCustomTextAlignment = true;
                ListView.IsDoubleBuffered = true;
                ListView.FullRowSelect = true;
                ListView.HideSelection = false;
            } // if

            HookupEvents(true);
        } // InitListViewControl

        private void ApplySettings(UiBroadcastListSettings newSettings)
        {
            // save existing settings
            var oldSettings = _oldSettings;

            // save new settings
            _oldSettings = XmlSerialization.Clone(newSettings);
            _settings = newSettings;

            // rebuild list?
            if (NeedToRebuildList(oldSettings, newSettings))
            {
                BuildListLayout();
            }
            else
            {
                ListView.BeginUpdate();

                // apply new sorting?
                if (NeedToApplySorting(oldSettings, newSettings))
                {
                    ApplySorting();
                }
                else
                {
                    // apply cosmetic changes?
                    ApplyCosmeticSettings(oldSettings, newSettings);
                } // if-else

                ListView.EndUpdate();
            } // if-else
        } // ApplySettings

        private static void SaveSettings(UiBroadcastListSettings settings)
        {
            UiBroadcastListSettingsRegistration.Settings = settings;
            AppConfig.Current.Save();
        } // SaveSettings

        private void BuildListLayout()
        {
            ListView.BeginUpdate();
            ListView.Items.Clear();
            ListView.Columns.Clear();
            ListView.View = Settings.CurrentMode;
            ListView.Font = (Settings.CurrentMode == View.Details) ? _fontDetails : _fontNormal;

            var columns = Settings.CurrentColumns;
            foreach (var column in columns)
            {
                var name = GetColumnName(column);
                var listColumn = new ColumnHeader()
                {
                    Name = Enum.GetName(typeof(UiBroadcastListColumn), column),
                    Text = GetColumnName(column),
                    Width = GetColumnWidth(column),
                };
                ListView.Columns.Add(listColumn);
            } // foreach
            ListView.Columns[0].Width += _smallImageList.ImageSize.Width + 5;

            ApplyCosmeticSettings(null, _settings);
            ApplySorting();
            FillList();
            ListView.EndUpdate();
        } // BuildListLayout

        public IList<UiBroadcastService> GetDisplayBroadcastList()
        {
            if ((BroadcastServices == null) || (BroadcastServices.Count == 0))
            {
                return new List<UiBroadcastService>(0);
            } // if

            var result = new List<UiBroadcastService>(_broadcastServices.Count);
            var q = from service in _sortedBroadcastServices
                    where (!service.IsInactive) || (Settings.ShowInactiveServices)
                    where (!service.IsHidden) || (Settings.ShowHiddenServices)
                    select service;
            result.AddRange(q);

            return result;
        } // GetDisplayBroadcastList

        private void FillList()
        {
            // save selected service
            var savedSelectedService = _selectedService;

            ListView.Items.Clear();
            var services = GetDisplayedBroadcastList();
            if (services.Count == 0)
            {
                return;
            } // if

            var items = new List<ListViewItem>(services.Count);
            var columns = Settings.CurrentColumns;
            if (columns.Count < 1) throw new InvalidOperationException();

            foreach (var service in services)
            {
                var item = new ListViewItem
                {
                    Tag = service,
                    Name = service.Key,
                    Text = GetColumnData(service, columns[0]),
                };

                var subItems = new ListViewItem.ListViewSubItem[columns.Count - 1];
                var index = 0;
                foreach (var column in columns.Skip(1))
                {
                    subItems[index++] = new ListViewItem.ListViewSubItem
                    {
                        Text = GetColumnData(service, column),
                        Name = Enum.GetName(typeof(UiBroadcastListColumn), column)
                    };
                } // foreach
                item.SubItems.AddRange(subItems);

                EnableListItem(item, service, columns);

                items.Add(item);
            } // foreach

            ListView.Items.AddRange(items.ToArray());

            // restore selected service and ensure is visible
            if (savedSelectedService != null)
            {
                SelectedService = savedSelectedService;
            } // if
        } // FillList

        private bool ApplyCosmeticSettings(UiBroadcastListSettings oldSettings, UiBroadcastListSettings newSettings)
        {
            var changed = false;

            if ((oldSettings == null) || (oldSettings.ShowGridlines != newSettings.ShowGridlines))
            {
                ListView.GridLines = newSettings.ShowGridlines;
                changed = true;
            } // if

            if ((oldSettings != null) && (oldSettings.TilesPerRow == newSettings.TilesPerRow)) return changed;

            var tilesPerRow = Math.Max(newSettings.TilesPerRow, 1);
            ListView.TileSize = new Size((ListView.Width - SystemInformation.VerticalScrollBarWidth - 6) / tilesPerRow,
                ListView.LargeImageList.ImageSize.Height + 6);

            return changed;
        } // ApplyCosmeticSettings

        private void ApplySorting()
        {
            if ((BroadcastServices == null) || (BroadcastServices.Count == 0)) return;

            // select sorting column
            var sortingColumns = Settings.UseGlobalSortColumns ? Settings.GlobalSortColumns : Settings[Settings.CurrentMode].Sort;
            var sortingColumn = sortingColumns[0];
            SelectedSort = sortingColumn;

            if (sortingColumn.Column == UiBroadcastListColumn.None)
            {
                _sortedBroadcastServices = _broadcastServices;
            }
            else
            {
                var comparer = new ServiceSortComparer(Settings, sortingColumns);
                _sortedBroadcastServices = _broadcastServices.OrderBy(item => item, comparer);
            } // if-else

            // force use of SortedBroadcastServices when filling the list
            _displayedBroadcastServices = null;
            FillList();

            // Update the 'arrow' in the header
            if (Settings.CurrentMode != View.Details) return;

            var q = from index in Enumerable.Range(0, Settings.CurrentColumns.Count)
                where Settings.CurrentColumns[index] == sortingColumn.Column
                select index;

            var columnIndex = q.DefaultIfEmpty(-1).First();
            ListView.Sort(columnIndex, sortingColumn.IsAscending);
        } // ApplySorting

        private static bool NeedToRebuildList(UiBroadcastListSettings oldSettings, UiBroadcastListSettings newSettings)
        {
            // new settings? 
            if (oldSettings == null) return true;

            //view mode changed?
            if (oldSettings.CurrentMode != newSettings.CurrentMode) return true;

            // number of columns changed?
            var oldColumns = oldSettings.CurrentColumns;
            var newColumns = newSettings.CurrentColumns;
            if (oldColumns.Count != newColumns.Count) return true;

            // columns changed
            if (oldColumns.Where((oldColumn, index) => oldColumn != newColumns[index]).Any()) return true;

            // show/hide changed
            if (oldSettings.ShowHiddenServices != newSettings.ShowHiddenServices) return true;
            if (oldSettings.ShowInactiveServices != newSettings.ShowInactiveServices) return true;
            if (oldSettings.ShowOutOfPackage != newSettings.ShowOutOfPackage) return true;

            return false;
        } // NeedToRebuildList

        private bool NeedToApplySorting(UiBroadcastListSettings oldSettings, UiBroadcastListSettings newSettings)
        {
            // global sort column changed?
            if (oldSettings.UseGlobalSortColumns != newSettings.UseGlobalSortColumns) return true;

            if (newSettings.UseGlobalSortColumns)
            {
                return !CompareSortColumns(oldSettings.GlobalSortColumns, newSettings.GlobalSortColumns);
            } // if

            // current mode sort column changed?
            var oldModeSettings = oldSettings[oldSettings.CurrentMode];
            var newModeSettings = newSettings[newSettings.CurrentMode];

            return !CompareSortColumns(oldModeSettings.Sort, newModeSettings.Sort);
        } // NeedToApplySorting

        private bool CompareSortColumns(List<UiBroadcastListSortColumn> oldColumns, List<UiBroadcastListSortColumn> newColumns)
        {
            if (oldColumns.Count != newColumns.Count) return false;

            return !oldColumns.Where((oldColumn, index) => oldColumn != newColumns[index]).Any();
        } // CompareSortColumns

        private int GetColumnWidth(UiBroadcastListColumn column)
        {
            var width = Settings.ColumnWidth[(int)column];
            if (width <= 0) return GetColumnDefaultWidth(column);
            else if (width < MinColumnWidth) return MinColumnWidth;
            else return width;
        } // GetColumnWidth

        private void EnableListItem(ListViewItem item, UiBroadcastService service, IList<UiBroadcastListColumn> columns)
        {
            if (service.IsInactive || service.IsHidden)
            {
                item.UseItemStyleForSubItems = (Settings.CurrentMode == View.Details);
                item.ForeColor = SystemColors.GrayText;
                item.Font = service.IsHidden ? _fontNormalHidden : _fontNormalNotAvailable;
                item.ImageKey = GetServiceLogoImageKey(service, true);
            }
            else
            {
                item.UseItemStyleForSubItems = false;
                item.ForeColor = ListView.ForeColor;
                item.Font = (Settings.CurrentMode == View.Details) ? _fontDetails : _fontNormal;
                item.ImageKey = GetServiceLogoImageKey(service, false);
            } // if-else

            // set certains columns as bold
            if ((Settings.CurrentMode != View.Details) || (item.UseItemStyleForSubItems)) return;

            var index = 0;
            foreach (var column in columns)
            {
                switch (column)
                {
                    case UiBroadcastListColumn.Name:
                    case UiBroadcastListColumn.NameAndNumber:
                    case UiBroadcastListColumn.Number:
                    case UiBroadcastListColumn.NumberAndName:
                    case UiBroadcastListColumn.NumberAndNameCrlf:
                    case UiBroadcastListColumn.OriginalName:
                    case UiBroadcastListColumn.OriginalNumber:
                    case UiBroadcastListColumn.UserName:
                    case UiBroadcastListColumn.UserNumber:
                        item.SubItems[index].Font = _fontDetailsBold;
                        break;
                } // switch
                index++;
            } // foreach
        } // EnableListItem

        private string GetServiceLogoImageKey(UiBroadcastService service, bool disabled)
        {
            var logo = service.Logo;
            if (_smallImageList.Images.ContainsKey(logo.Key))
            {
                return disabled ? "<Disabled> " + logo.Key : logo.Key;
            } // if

            var key = logo.Key;
            var disabledKey = "<Disabled> " + key;

            // load small logo and add it to small image list
            using (var image = logo.GetImage(SmallLogoSize))
            {
                _smallImageList.Images.Add(logo.Key, image);
                using (var disabledImage = PictureBoxEx.ToGreyscale(image))
                {
                    _smallImageList.Images.Add(disabledKey, disabledImage);
                } // using image
            } // using image

            // load large logo and add it to large image list
            using (var image = logo.GetImage(LargeLogoSize))
            {
                _largeImageList.Images.Add(logo.Key, image);
                using (var disabledImage = PictureBoxEx.ToGreyscale(image))
                {
                    _largeImageList.Images.Add(disabledKey, disabledImage);
                } // using image
            } // using image

            return disabled ? disabledKey : key;
        } // GetChannelLogoKey

        #endregion
    } // UiBroadcastListViewManager
} // namespace

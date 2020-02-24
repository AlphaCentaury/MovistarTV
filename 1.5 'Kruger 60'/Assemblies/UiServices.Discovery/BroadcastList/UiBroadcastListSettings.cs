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
using System.Windows.Forms;
using System.Xml.Serialization;
using IpTviewr.Common.Configuration;

namespace IpTviewr.UiServices.Discovery.BroadcastList
{
    [Serializable]
    [XmlRoot("UiBroadcastList", Namespace = ConfigCommon.ConfigXmlNamespace)]
    public class UiBroadcastListSettings : IConfigurationItem
    {
        private int[] _columnWidth;

        public class ModeViewSettings
        {
            public UiBroadcastListModeSettings Details
            {
                get;
                set;
            } // Details

            public UiBroadcastListModeSettings LargeIcon
            {
                get;
                set;
            } // LargeIcon

            public UiBroadcastListModeSettings SmallIcon
            {
                get;
                set;
            } // SmallIcon

            public UiBroadcastListModeSettings List
            {
                get;
                set;
            } // List

            public UiBroadcastListModeSettings Tile
            {
                get;
                set;
            } // Tile
        } // class ModeViewSettings

        #region Static methods

        public static UiBroadcastListSettings GetDefaultSettings()
        {
            var result = new UiBroadcastListSettings
            {
                CurrentMode = View.Tile,
                ShowGridlines = true,
                ShowInactiveServices = true,
                ShowOutOfPackage = true,
                TilesPerRow = 4,

                ViewSettings = new ModeViewSettings
                {
                    Details = UiBroadcastListModeSettings.GetDefaultSettings(View.Details),
                    LargeIcon = UiBroadcastListModeSettings.GetDefaultSettings(View.LargeIcon),
                    SmallIcon = UiBroadcastListModeSettings.GetDefaultSettings(View.SmallIcon),
                    List = UiBroadcastListModeSettings.GetDefaultSettings(View.List),
                    Tile = UiBroadcastListModeSettings.GetDefaultSettings(View.Tile)
                },

                GlobalSortColumns = ServiceSortComparer.GetSuggestedSortColumns(UiBroadcastListColumn.Number, true, 3),
                UseGlobalSortColumns = false
            };

            // force creation of ColumnWidth field
            _ = result.ColumnWidth[0];

            return result;
        } // GetDefaultSettings

        #endregion

        public View CurrentMode
        {
            get;
            set;
        } // CurrentMode

        [XmlIgnore]
        public UiBroadcastListModeSettings this[View mode]
        {
            get
            {
                return mode switch
                {
                    View.Details => ViewSettings.Details,
                    View.LargeIcon => ViewSettings.LargeIcon,
                    View.SmallIcon => ViewSettings.SmallIcon,
                    View.List => ViewSettings.List,
                    View.Tile => ViewSettings.Tile,
                    _ => throw new IndexOutOfRangeException(),
                };
            } // get
            set
            {
                switch (mode)
                {
                    case View.Details: ViewSettings.Details = value; break;
                    case View.LargeIcon: ViewSettings.LargeIcon = value; break;
                    case View.SmallIcon: ViewSettings.SmallIcon = value; break;
                    case View.List: ViewSettings.List = value; break;
                    case View.Tile: ViewSettings.Tile = value; break;
                    default:
                        throw new IndexOutOfRangeException();
                } // switch
            } // set
        } // UiBroadcastListViewModeSettings

        public ModeViewSettings ViewSettings
        {
            get;
            set;
        } // ViewSettings

        [XmlArray("OverrideSortBy")]
        [XmlArrayItem("Column")]
        public List<UiBroadcastListSortColumn> GlobalSortColumns
        {
            get;
            set;
        } // GlobalSortColumns

        [DefaultValue(false)]
        [XmlElement("UseOverrideSortBy")]
        public bool UseGlobalSortColumns
        {
            get;
            set;
        } // UseGlobalSortColumn

        [DefaultValue(false)]
        public bool ShowInactiveServices
        {
            get;
            set;
        } // ShowInactiveServices

        [DefaultValue(false)]
        public bool ShowHiddenServices
        {
            get;
            set;
        } // ShowHiddenServices

        [DefaultValue(false)]
        public bool ShowOutOfPackage
        {
            get;
            set;
        } // ShowOutOfPackage

        [DefaultValue(false)]
        public bool ShowGridlines
        {
            get;
            set;
        } // ShowGridlines

        public int TilesPerRow
        {
            get;
            set;
        } // TilesPerRow

        [XmlArrayItem("Width")]
        public int[] ColumnWidth
        {
            get => _columnWidth ??= new int[23];
            set => _columnWidth = value;
        } // ColumnWidth

        [XmlIgnore]
        public IList<UiBroadcastListColumn> CurrentColumns => this[CurrentMode].Columns.AsReadOnly();

        #region IConfigurationItem implementation

        bool IConfigurationItem.SupportsInitialization => false;

        bool IConfigurationItem.SupportsValidation => false;

        InitializationResult IConfigurationItem.Initialize()
        {
            throw new NotSupportedException();
        } // IConfigurationItem.Initialize

        string IConfigurationItem.Validate(string ownerTag)
        {
            throw new NotSupportedException();
        } // IConfigurationItem.Validate

        #endregion
    } // class UiBroadcastListViewSettings
} // namespace

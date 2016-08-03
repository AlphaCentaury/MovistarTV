// Copyright (C) 2014-2015, Codeplex user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using Project.IpTv.Common.Serialization;
using Project.IpTv.UiServices.Configuration;
using Project.IpTv.UiServices.Configuration.Schema2014.Config;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace Project.IpTv.UiServices.Discovery.BroadcastList
{
    [Serializable]
    [XmlRoot("UiBroadcastList", Namespace=ConfigCommon.ConfigXmlNamespace)]
    public class UiBroadcastListSettings : IConfigurationItem
    {
        private int[] fieldColumnWidth;

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
            var result = new UiBroadcastListSettings();

            result.CurrentMode = View.Tile;
            result.ShowGridlines = true;
            result.ShowInactiveServices = true;
            result.ShowOutOfPackage = true;
            result.TilesPerRow = 4;

            result.ViewSettings = new ModeViewSettings();
            result.ViewSettings.Details = UiBroadcastListModeSettings.GetDefaultSettings(View.Details);
            result.ViewSettings.LargeIcon = UiBroadcastListModeSettings.GetDefaultSettings(View.LargeIcon);
            result.ViewSettings.SmallIcon = UiBroadcastListModeSettings.GetDefaultSettings(View.SmallIcon);
            result.ViewSettings.List = UiBroadcastListModeSettings.GetDefaultSettings(View.List);
            result.ViewSettings.Tile = UiBroadcastListModeSettings.GetDefaultSettings(View.Tile);

            result.GlobalSortColumns = ServiceSortComparer.GetSuggestedSortColumns(UiBroadcastListColumn.Number, true, 3);
            result.UseGlobalSortColumns = false;

            // force creation of ColumnWidth field
            var dummy = result.ColumnWidth[0];

            return result;
        } // GetDefaultSettings

        #endregion

        public UiBroadcastListSettings()
        {
            // no op
        } // constructor

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
                switch (mode)
                {
                    case View.Details: return ViewSettings.Details;
                    case View.LargeIcon: return ViewSettings.LargeIcon;
                    case View.SmallIcon: return ViewSettings.SmallIcon;
                    case View.List: return ViewSettings.List;
                    case View.Tile: return ViewSettings.Tile;
                    default:
                        throw new IndexOutOfRangeException();
                } // switch
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
            get
            {
                if (fieldColumnWidth == null)
                {
                    fieldColumnWidth = new int[23];
                } // if
                return fieldColumnWidth;
            }
            set
            {
                fieldColumnWidth = value;
            } // set
        } // ColumnWidth

        [XmlIgnore]
        public IList<UiBroadcastListColumn> CurrentColumns
        {
            get { return this[CurrentMode].Columns.AsReadOnly(); }
        } // CurrentColumns

        #region IConfigurationItem implementation

        bool IConfigurationItem.SupportsInitialization
        {
            get { return false; }
        } // IConfigurationItem.SupportsInitialization

        bool IConfigurationItem.SupportsValidation
        {
            get { return false; }
        } // IConfigurationItem.CanValidate

        InitializationResult IConfigurationItem.Initializate()
        {
            throw new NotSupportedException();
        } // IConfigurationItem.Initializate

        string IConfigurationItem.Validate(string ownerTag)
        {
            throw new NotSupportedException();
        } // IConfigurationItem.Validate

        #endregion
    } // class UiBroadcastListViewSettings
} // namespace

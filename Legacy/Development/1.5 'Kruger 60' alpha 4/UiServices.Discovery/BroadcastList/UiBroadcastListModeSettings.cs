// Copyright (C) 2014-2015, Codeplex user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using Project.IpTv.Common.Serialization;
using Project.IpTv.UiServices.Configuration.Logos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace Project.IpTv.UiServices.Discovery.BroadcastList
{
    [Serializable]
    public class UiBroadcastListModeSettings
    {
        public static UiBroadcastListModeSettings GetDefaultSettings(View mode)
        {
            var result = new UiBroadcastListModeSettings();
            result.Sort = ServiceSortComparer.GetSuggestedSortColumns(UiBroadcastListColumn.Number, true, 3);

            switch (mode)
            {
                case View.Details:
                    result.Columns = new List<UiBroadcastListColumn>(5);
                    result.Columns.Add(UiBroadcastListColumn.Number);
                    result.Columns.Add(UiBroadcastListColumn.Name);
                    result.Columns.Add(UiBroadcastListColumn.Description);
                    result.Columns.Add(UiBroadcastListColumn.DvbType);
                    result.Columns.Add(UiBroadcastListColumn.LocationUrl);
                    result.LogoSize = LogoSize.Size32;
                    break;
                case View.LargeIcon:
                    result.Columns = new List<UiBroadcastListColumn>(1);
                    result.Columns.Add(UiBroadcastListColumn.NumberAndNameCrlf);
                    result.LogoSize = LogoSize.Size48;
                    break;
                case View.SmallIcon:
                    result.Columns = new List<UiBroadcastListColumn>(1);
                    result.Columns.Add(UiBroadcastListColumn.Number);
                    result.LogoSize = LogoSize.Size32;
                    break;
                case View.List:
                    result.Columns = new List<UiBroadcastListColumn>(1);
                    result.Columns.Add(UiBroadcastListColumn.NumberAndName);
                    result.LogoSize = LogoSize.Size32;
                    break;
                case View.Tile:
                    result.Columns = new List<UiBroadcastListColumn>(2);
                    result.Columns.Add(UiBroadcastListColumn.NumberAndName);
                    result.Columns.Add(UiBroadcastListColumn.DvbType);
                    result.LogoSize = LogoSize.Size48;
                    break;
                default:
                    throw new IndexOutOfRangeException();
            } // switch

            return result;
        } // GetDefaultSettings

        [XmlArrayItem("Column")]
        public List<UiBroadcastListColumn> Columns
        {
            get;
            set;
        } // Columns

        [XmlArray("SortBy")]
        [XmlArrayItem("Column")]
        public List<UiBroadcastListSortColumn> Sort
        {
            get;
            set;
        } // Sort

        public LogoSize LogoSize
        {
            get;
            set;
        } // LogoSize
    } // class UiBroadcastListViewModeSettings
} // namespace

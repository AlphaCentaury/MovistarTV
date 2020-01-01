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
using System.ComponentModel;
using System.Xml.Serialization;

namespace IpTviewr.UiServices.Discovery.BroadcastList
{
    [Serializable]
    public struct UiBroadcastListSortColumn
    {
        public UiBroadcastListSortColumn(UiBroadcastListColumn column, bool descending)
            : this()
        {
            Column = column;
            Descending = (column == UiBroadcastListColumn.None)? false : descending;
        } // constructor

        [XmlAttribute("name")]
        public UiBroadcastListColumn Column
        {
            get;
            set;
        } // Column

        [DefaultValue(false)]
        [XmlAttribute("descending")]
        public bool Descending
        {
            get;
            set;
        } // Descending

        [XmlIgnore]
        public bool IsAscending
        {
            get => !Descending;
            set => Descending = !value;
        } // IsAscending

        public override bool Equals(object obj)
        {
            // if parameter is null return false
            if (obj is null) return false;

            try
            {
                // return true if the fields match
                return Equals((UiBroadcastListSortColumn)obj);
            }
            catch (InvalidCastException)
            {
                // if parameter cannot be cast to this type return false
                return false;
            } // try-catch
        } // Equals

        public bool Equals(UiBroadcastListSortColumn column)
        {
            if (Column != column.Column) return false;
            if (IsAscending == column.IsAscending) return true;
            // special case
            if (Column == UiBroadcastListColumn.None) return true;

            return false;
        } // Equals

        public override int GetHashCode()
        {
            return IsAscending ? (int)Column : -1 * (int)Column;
        } // GetHashCode

        public static bool operator == (UiBroadcastListSortColumn column1, UiBroadcastListSortColumn column2)
        {
            return column1.Equals(column2);
        } // operator ==

        public static bool operator !=(UiBroadcastListSortColumn column1, UiBroadcastListSortColumn column2)
        {
            return !column1.Equals(column2);
        } // operator !=
    } // class UiBroadcastListViewSortColumn
} // namespace

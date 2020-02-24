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
using System.Text;

namespace IpTviewr.UiServices.Discovery.BroadcastList
{
    public class ServiceSortComparer: IComparer<UiBroadcastService>
    {
        private readonly UiBroadcastListSettings _settings;
        private readonly IList<UiBroadcastListSortColumn> _sort;
        private readonly StringBuilder _buffer;

        public ServiceSortComparer(UiBroadcastListSettings settings, IList<UiBroadcastListSortColumn> sort)
        {
            _settings = settings;
            _sort = sort;
            _buffer = new StringBuilder(512);
        } // constructor

        public int Compare(UiBroadcastService x, UiBroadcastService y)
        {
            var compare = 0;

            foreach (var sort in _sort)
            {
                var sortColumn = sort.Column;
                if (sortColumn == UiBroadcastListColumn.None) break;

                var data1 = UiBroadcastListManager.GetColumnData(x, sortColumn);
                var data2 = UiBroadcastListManager.GetColumnData(y, sortColumn);

                data1 = GetTextWithNumberForTextSorting(data1, _buffer);
                data2 = GetTextWithNumberForTextSorting(data2, _buffer);

                compare = data1.CompareTo(data2);
                compare *= (sort.IsAscending ? 1 : -1);

                if (compare != 0) break;
            } // foreach sort

            return compare;
        } // Compare

        public static List<UiBroadcastListSortColumn> GetSuggestedSortColumns(UiBroadcastListColumn column, bool ascending, int max)
        {
            var sortColumn = column;
            var result = new List<UiBroadcastListSortColumn>(max);

            var loop = 0;
            do
            {
                result.Add(new UiBroadcastListSortColumn(sortColumn, !ascending));
                sortColumn = GetSuggestedNextSortColumn(sortColumn);
                loop++;
            }
            while ((sortColumn != UiBroadcastListColumn.None) && (loop < max));

            return result;
        } // GetSuggestedSortColumns

        public static UiBroadcastListColumn GetSuggestedNextSortColumn(UiBroadcastListColumn column)
        {
            switch (column)
            {
                case UiBroadcastListColumn.None: return UiBroadcastListColumn.None;
                case UiBroadcastListColumn.Name: return UiBroadcastListColumn.None;
                case UiBroadcastListColumn.Number: return UiBroadcastListColumn.Name;
                case UiBroadcastListColumn.NumberAndName: return UiBroadcastListColumn.None;
                case UiBroadcastListColumn.NumberAndNameCrlf: return UiBroadcastListColumn.None;
                case UiBroadcastListColumn.NameAndNumber: return UiBroadcastListColumn.None;
                case UiBroadcastListColumn.Description: return UiBroadcastListColumn.Name;
                case UiBroadcastListColumn.DvbType: return UiBroadcastListColumn.Number;
                case UiBroadcastListColumn.LocationUrl: return UiBroadcastListColumn.DvbType;
                case UiBroadcastListColumn.ShortName: return UiBroadcastListColumn.Name;
                case UiBroadcastListColumn.Genre: return UiBroadcastListColumn.Number;
                case UiBroadcastListColumn.GenreCode: return UiBroadcastListColumn.Number;
                case UiBroadcastListColumn.ParentalRating: return UiBroadcastListColumn.Number;
                case UiBroadcastListColumn.ParentalRatingCode: return UiBroadcastListColumn.Number;
                case UiBroadcastListColumn.ServiceId: return UiBroadcastListColumn.None;
                case UiBroadcastListColumn.FullServiceId: return UiBroadcastListColumn.None;
                case UiBroadcastListColumn.UserName: return UiBroadcastListColumn.OriginalName;
                case UiBroadcastListColumn.UserNumber: return UiBroadcastListColumn.Name;
                case UiBroadcastListColumn.OriginalName: return UiBroadcastListColumn.None;
                case UiBroadcastListColumn.OriginalNumber: return UiBroadcastListColumn.Name;
                case UiBroadcastListColumn.IsActive: return UiBroadcastListColumn.Number;
                case UiBroadcastListColumn.IsEnabled: return UiBroadcastListColumn.Number;
                case UiBroadcastListColumn.LockLevel: return UiBroadcastListColumn.Number;
                default:
                    throw new IndexOutOfRangeException();
            } // switch
        } // GetSuggestedNextSortColumn

        public static string GetTextWithNumberForTextSorting(string text)
        {
            var buffer = new StringBuilder();
            return GetTextWithNumberForTextSorting(text, buffer);
        } // GetTextWithNumberForTextSorting

        public static string GetTextWithNumberForTextSorting(string textWithNumbers, StringBuilder buffer)
        {
            int pos;

            buffer.Length = 0;
            var start = -1;

            for (pos = 0; pos < textWithNumbers.Length; pos++)
            {
                if (char.IsDigit(textWithNumbers[pos]))
                {
                    if (start < 0) start = pos;
                }
                else
                {
                    if (start >= 0)
                    {
                        AddPartialNumber(textWithNumbers, buffer, ref start, pos - 1);
                    } // if
                    buffer.Append(textWithNumbers[pos]);
                } // if-else
            } // for

            if (start < 0) return textWithNumbers;

            AddPartialNumber(textWithNumbers, buffer, ref start, pos - 1);
            return buffer.ToString();
        } // GetTextWithNumberForTextSorting

        private static int AddPartialNumber(string textWithNumbers, StringBuilder buffer, ref int start, int pos)
        {
            var partial = textWithNumbers.Substring(start, (pos - start) + 1);
            var number = long.Parse(partial);
            partial = number.ToString("00000000000000000000");
            buffer.Append(partial);
            start = -1;

            return start;
        } // AddPartialNumber
    } // class ServiceSortComparer
} // namespace

// Copyright (C) 2014-2016, Codeplex/GitHub user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Project.IpTv.UiServices.Common.Controls
{
    public class ListViewColumnItemComparer : IComparer
    {
        private int ColumnIndex;
        private int ResultSign;

        public ListViewColumnItemComparer(int columnIndex, bool descending)
        {
            ColumnIndex = columnIndex;
            ResultSign = descending ? -1 : 1;
        } // ListViewColumnItemSorter

        public int Compare(object x, object y)
        {
            ListViewItem item1, item2;
            int compare;

            item1 = x as ListViewItem;
            item2 = y as ListViewItem;

            if (object.Equals(item1, item2)) return 0;

            compare = ResultSign * string.Compare(GetSafeText(item1, ColumnIndex), GetSafeText(item2, ColumnIndex));
            if (compare == 0)
            {
                compare = string.Compare(item1.SubItems[0].Text, item2.SubItems[0].Text);
            } // if

            return compare;
        } // Compare

        private static string GetSafeText(ListViewItem item, int subItemIndex)
        {
            if (subItemIndex >= item.SubItems.Count) return null;
            return item.SubItems[subItemIndex].Text;
        } // GetSafeText
    } // class ListViewColumnItemComparer
} // namespace

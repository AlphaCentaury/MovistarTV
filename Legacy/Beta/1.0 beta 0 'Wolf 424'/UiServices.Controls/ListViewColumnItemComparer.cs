// Copyright (C) 2014, Codeplex user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Project.DvbIpTv.UiServices.Controls
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

            compare = ResultSign * string.Compare(item1.SubItems[ColumnIndex].Text, item2.SubItems[ColumnIndex].Text);
            if (compare == 0)
            {
                compare = string.Compare(item1.SubItems[0].Text, item2.SubItems[0].Text);
            } // if

            return compare;
        } // Compare
    } // class ListViewColumnItemComparer
} // namespace

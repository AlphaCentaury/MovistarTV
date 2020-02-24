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

using System.Collections;
using System.Windows.Forms;

namespace IpTviewr.UiServices.Common.Controls
{
    public class ListViewColumnItemComparer : IComparer
    {
        private readonly int _columnIndex;
        private readonly int _resultSign;

        public ListViewColumnItemComparer(int columnIndex, bool descending)
        {
            _columnIndex = columnIndex;
            _resultSign = descending ? -1 : 1;
        } // ListViewColumnItemSorter

        public int Compare(object x, object y)
        {
            var item1 = x as ListViewItem;
            var item2 = y as ListViewItem;

            if (Equals(item1, item2)) return 0;

            var compare = _resultSign * string.Compare(GetSafeText(item1, _columnIndex), GetSafeText(item2, _columnIndex));
            if ((compare == 0) && (_columnIndex > 0))
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

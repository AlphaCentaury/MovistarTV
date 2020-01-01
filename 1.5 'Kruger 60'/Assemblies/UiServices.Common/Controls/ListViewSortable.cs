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
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace IpTviewr.UiServices.Common.Controls
{
    [ToolboxBitmap(typeof(ListView))]
    public class ListViewSortable : ListView
    {
        public enum EllipsisStyle
        {
            Default,
            None,
            Path,
            End
        } // enum EllipsisStyle

        public ListViewSortable()
        {
            SelfSorting = true;
            IsDoubleBuffered = true;
            CurrentSortColumn = -1;
            HeaderCustomTextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
        } // constructor

        public event EventHandler AfterSorting;

        public bool IsDoubleBuffered
        {
            get => base.DoubleBuffered;
            set => base.DoubleBuffered = value;
        } // IsDoubleBuffered


        [DefaultValue(false)]
        public bool HeaderUsesCustomFont
        {
            get;
            set;
        } // HeaderUsesCustomFont

        public Font HeaderCustomFont
        {
            get;
            set;
        } // HeaderCustomFont

        [DefaultValue(false)]
        public bool HeaderUsesCustomForeColor
        {
            get;
            set;
        } // HeaderUsesCustomForeColor

        public Color HeaderCustomForeColor
        {
            get;
            set;
        } // HeaderCustomForeColor

        [DefaultValue(false)]
        public bool HeaderUsesCustomTextAlignment
        {
            get;
            set;
        } // HeaderUsesCustomTextAlignment

        [DefaultValue(System.Drawing.ContentAlignment.MiddleLeft)]
        public System.Drawing.ContentAlignment HeaderCustomTextAlignment
        {
            get;
            set;
        } // HeaderCustomTextAlignment

        [DefaultValue(false)]
        public bool HeaderCustomWordBreak
        {
            get;
            set;
        } // HeaderCustomWordBreak

        [DefaultValue(EllipsisStyle.Default)]
        public EllipsisStyle HeaderCustomEllipsis
        {
            get;
            set;
        } // HeaderCustomEllipsis

        [DefaultValue(true)]
        public bool SelfSorting
        {
            get;
            set;
        } // SelfSorting

        public int CurrentSortColumn
        {
            get;
            private set;
        } // CurrentSortColumn

        public bool CurrentSortIsDescending
        {
            get;
            private set;
        } // CurrentSortIsDescending

        /// <summary>
        /// Sorts the list
        /// </summary>
        /// <param name="sortColumnIndex">Column to sort</param>
        /// <param name="sortAscending">null = toggle current column sort order (ascending->descending; descending->ascending); true = sort ascending; false = sort descending</param>
        public void Sort(int sortColumnIndex, bool? sortAscending)
        {
            bool ascending;

            if (sortColumnIndex >= Columns.Count) throw new ArgumentOutOfRangeException("columnIndex");

            if (sortColumnIndex < 0)
            {
                CurrentSortColumn = -1;
                CurrentSortIsDescending = false;
                ListViewItemSorter = null;
            }
            else
            {
                if (!sortAscending.HasValue)
                {
                    ascending = true;
                    if (sortColumnIndex == CurrentSortColumn)
                    {
                        ascending = CurrentSortIsDescending;
                    } // if
                }
                else
                {
                    ascending = sortAscending.Value;
                } // if-else

                CurrentSortColumn = sortColumnIndex;
                CurrentSortIsDescending = !ascending;

                if (SelfSorting)
                {
                    ListViewItemSorter = new ListViewColumnItemComparer(CurrentSortColumn, CurrentSortIsDescending);
                } // if
            } // if-else

            // force redraw to update the "arrow" on the header
            RedrawHeader();

            AfterSorting?.Invoke(this, EventArgs.Empty);
        } // Sort

        protected override void OnColumnClick(ColumnClickEventArgs e)
        {
            bool ascending;

            base.OnColumnClick(e);
            if (!SelfSorting) return;

            ascending = (CurrentSortColumn == e.Column)? CurrentSortIsDescending: true;
            Sort(e.Column, ascending);
        } // OnColumnClick

        protected override void OnDrawColumnHeader(DrawListViewColumnHeaderEventArgs e)
        {
            TextFormatFlags format;

            var style = e.State switch
            {
                // render background
                ListViewItemStates.Hot => VisualStyleElement.Header.Item.Hot,
                ListViewItemStates.Selected => VisualStyleElement.Header.Item.Pressed,
                _ => VisualStyleElement.Header.Item.Normal
            };

            var renderer = new VisualStyleRenderer(style);
            renderer.DrawBackground(e.Graphics, e.Bounds);

            // build text style
            if (HeaderUsesCustomTextAlignment)
            {
                format = HeaderCustomTextAlignment switch
                {
                    System.Drawing.ContentAlignment.TopLeft => TextFormatFlags.Left | TextFormatFlags.Top,
                    System.Drawing.ContentAlignment.TopCenter => TextFormatFlags.HorizontalCenter | TextFormatFlags.Top,
                    System.Drawing.ContentAlignment.TopRight => TextFormatFlags.Right | TextFormatFlags.Top,
                    System.Drawing.ContentAlignment.MiddleLeft => TextFormatFlags.Left | TextFormatFlags.VerticalCenter,
                    System.Drawing.ContentAlignment.MiddleCenter => TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter,
                    System.Drawing.ContentAlignment.MiddleRight => TextFormatFlags.Right | TextFormatFlags.VerticalCenter,
                    System.Drawing.ContentAlignment.BottomLeft => TextFormatFlags.Left | TextFormatFlags.Bottom,
                    System.Drawing.ContentAlignment.BottomCenter => TextFormatFlags.HorizontalCenter | TextFormatFlags.Bottom,
                    System.Drawing.ContentAlignment.BottomRight => TextFormatFlags.Right | TextFormatFlags.Bottom,
                    _ => TextFormatFlags.Default,
                };
                switch (HeaderCustomEllipsis)
                {
                    case EllipsisStyle.None: break;
                    case EllipsisStyle.End: format |= TextFormatFlags.EndEllipsis; break;
                    case EllipsisStyle.Path: format |= TextFormatFlags.PathEllipsis; break;
                    default:
                        format |= TextFormatFlags.WordEllipsis; break;
                } // switch

                format |= HeaderCustomWordBreak ? TextFormatFlags.WordBreak : TextFormatFlags.SingleLine;
            }
            else
            {
                var textAlign = e.Header.TextAlign;
                format = (textAlign == HorizontalAlignment.Left) ? TextFormatFlags.Left : ((textAlign == HorizontalAlignment.Center) ? TextFormatFlags.HorizontalCenter : TextFormatFlags.Right);
                format |= TextFormatFlags.WordEllipsis;
                format |= TextFormatFlags.VerticalCenter;
                format |= TextFormatFlags.SingleLine;
            } // if-else

            // select text drawing attributes
            var font = HeaderUsesCustomFont ? HeaderCustomFont : e.Font;
            var foreColor = HeaderUsesCustomForeColor ? HeaderCustomForeColor : e.ForeColor;

            // measure sort arrow size
            style = (CurrentSortIsDescending) ? VisualStyleElement.Header.SortArrow.SortedUp : VisualStyleElement.Header.SortArrow.SortedDown;
            renderer = new VisualStyleRenderer(style);
            var arrowSize = renderer.GetPartSize(e.Graphics, ThemeSizeType.True);
            var bounds = new Rectangle(e.Bounds.Left, e.Bounds.Top, e.Bounds.Width - arrowSize.Width - 3, e.Bounds.Height);

            // render text
            TextRenderer.DrawText(e.Graphics, e.Header.Text, font, bounds, foreColor, format);

            // render sort arrow
            if (e.ColumnIndex == CurrentSortColumn)
            {
                renderer.DrawBackground(e.Graphics,
                    new Rectangle(e.Bounds.Left + bounds.Width,
                        e.Bounds.Top,
                        arrowSize.Width,
                        e.Bounds.Height));
            } // if
        } // OnDrawColumnHeader

        protected override void OnDrawItem(DrawListViewItemEventArgs e)
        {
            e.DrawDefault = true;
            base.OnDrawItem(e);
        } // OnDrawItem

        protected override void OnDrawSubItem(DrawListViewSubItemEventArgs e)
        {
            e.DrawDefault = true;
            base.OnDrawSubItem(e);
        } // OnDrawSubItem

        protected void RedrawHeader()
        {
            BeginUpdate();
            var old = HeaderStyle;
            HeaderStyle = ColumnHeaderStyle.None;
            HeaderStyle = old;
            EndUpdate();
        } // RedrawHeader
    } // ListViewSortable
} // namespace

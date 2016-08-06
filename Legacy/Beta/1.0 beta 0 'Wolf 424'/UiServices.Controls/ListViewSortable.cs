// Copyright (C) 2014, Codeplex user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace Project.DvbIpTv.UiServices.Controls
{
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
            : base()
        {
            SelfSorting = true;
            CurrentSortColumn = -1;
            HeaderCustomTextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
        } // constructor

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

        public void Sort(int columnIndex, bool? sortAscending)
        {
            bool ascending;

            ascending = true;
            if (columnIndex == CurrentSortColumn)
            {
                if (sortAscending == null)
                {
                    sortAscending = CurrentSortIsDescending;
                }
                else
                {
                    ascending = sortAscending.Value;
                } // if-else
            } // if-else

            CurrentSortColumn = columnIndex;
            CurrentSortIsDescending = !ascending;

            // force redraw to update the "arrow" on the header
            Visible = false;
            Visible = true;

            this.ListViewItemSorter = new ListViewColumnItemComparer(CurrentSortColumn, CurrentSortIsDescending);
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
            VisualStyleRenderer renderer;
            VisualStyleElement style;
            Size arrowSize;
            TextFormatFlags format;
            Rectangle bounds;

            // render background
            if (e.State == ListViewItemStates.Hot)
                style = VisualStyleElement.Header.Item.Hot;
            else if (e.State == ListViewItemStates.Selected)
                style = VisualStyleElement.Header.Item.Pressed;
            else
                style = VisualStyleElement.Header.Item.Normal;

            renderer = new VisualStyleRenderer(style);
            renderer.DrawBackground(e.Graphics, e.Bounds);

            // build text style
            if (HeaderUsesCustomTextAlignment)
            {
                format = TextFormatFlags.Default;
                switch (HeaderCustomTextAlignment)
                {
                    case System.Drawing.ContentAlignment.TopLeft:
                        format = TextFormatFlags.Left | TextFormatFlags.Top; break;
                    case System.Drawing.ContentAlignment.TopCenter:
                        format = TextFormatFlags.HorizontalCenter | TextFormatFlags.Top; break;
                    case System.Drawing.ContentAlignment.TopRight:
                        format = TextFormatFlags.Right | TextFormatFlags.Top; break;
                    case System.Drawing.ContentAlignment.MiddleLeft:
                        format = TextFormatFlags.Left | TextFormatFlags.VerticalCenter; break;
                    case System.Drawing.ContentAlignment.MiddleCenter:
                        format = TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter; break;
                    case System.Drawing.ContentAlignment.MiddleRight:
                        format = TextFormatFlags.Right | TextFormatFlags.VerticalCenter; break;
                    case System.Drawing.ContentAlignment.BottomLeft:
                        format = TextFormatFlags.Left | TextFormatFlags.Bottom; break;
                    case System.Drawing.ContentAlignment.BottomCenter:
                        format = TextFormatFlags.HorizontalCenter | TextFormatFlags.Bottom; break;
                    case System.Drawing.ContentAlignment.BottomRight:
                        format = TextFormatFlags.Right | TextFormatFlags.Bottom; break;
                    default:
                        format = TextFormatFlags.Default; break;
                } // case

                switch (this.HeaderCustomEllipsis)
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
                HorizontalAlignment textAlign = e.Header.TextAlign;
                format = (textAlign == HorizontalAlignment.Left) ? TextFormatFlags.Default : ((textAlign == HorizontalAlignment.Center) ? TextFormatFlags.HorizontalCenter : TextFormatFlags.Right);
                format |= TextFormatFlags.WordEllipsis;
                format |= TextFormatFlags.VerticalCenter;
                format |= TextFormatFlags.SingleLine;
            } // if-else

            // select text drawing attributtes
            var font = HeaderUsesCustomFont ? HeaderCustomFont : e.Font;
            var foreColor = HeaderUsesCustomForeColor ? HeaderCustomForeColor : e.ForeColor;

            // measure sort arrow size
            style = (CurrentSortIsDescending) ? VisualStyleElement.Header.SortArrow.SortedUp : VisualStyleElement.Header.SortArrow.SortedDown;
            renderer = new VisualStyleRenderer(style);
            arrowSize = renderer.GetPartSize(e.Graphics, ThemeSizeType.Draw);
            bounds = e.Bounds;
            bounds.Inflate(-(arrowSize.Width + 3), 0);

            // render text
            TextRenderer.DrawText(e.Graphics, e.Header.Text, font, bounds, foreColor, format);

            // render sort arrow
            if (e.ColumnIndex == CurrentSortColumn)
            {
                renderer.DrawBackground(e.Graphics,
                    new Rectangle(e.Bounds.Left + e.Bounds.Width - arrowSize.Width - 3, // give extra right space
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
    } // ListViewSortable
} // namespace

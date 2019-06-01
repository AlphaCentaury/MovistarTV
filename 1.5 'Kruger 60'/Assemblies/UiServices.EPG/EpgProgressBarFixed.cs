// Copyright (C) 2014-2019, GitHub/Codeplex user AlphaCentaury
// 
// All rights reserved, except those granted by the governing license of this software.
// See 'license.txt' file in the project root for complete license information.
// 
// http://www.alphacentaury.org/movistartv https://github.com/AlphaCentaury

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace IpTviewr.UiServices.EPG
{
    public class EpgProgressBarFixed: Control
    {
        private Image BarBase, BarFilled;
        private double min, max, progressValue;

        public EpgProgressBarFixed()
        {
            min = 0;
            max = 100;
            progressValue = min;
        } // constructor

        [DefaultValue(0.0)]
        public double MinimumValue
        {
            get
            {
                return min;
            } // get
            set
            {
                if (min > max) throw new ArgumentOutOfRangeException();
                min = value;
                if (progressValue < min) Value = min;
            } // set
        } // MinimumValue

        [DefaultValue(100.0)]
        public double MaximumValue
        {
            get
            {
                return max;
            } // get
            set
            {
                if (max < min) throw new ArgumentOutOfRangeException();
                max = value;
                if (progressValue > max) Value = max;
            } // set
        } // MaximumValue

        public double ValueRange
        {
            get { return max - min; }
        } // ValueRange

        [DefaultValue(0.0)]
        public double Value
        {
            get
            {
                return progressValue;
            } // get
            set
            {
                var old = progressValue;
                progressValue = value;
                if (progressValue < min) progressValue = min;
                if (progressValue > max) progressValue = max;
                if (progressValue != old)
                {
                    Invalidate();
                } // if
            } // set
        } // Value

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            BarBase = Properties.Resources.ProgressBarBase;
            BarFilled = Properties.Resources.ProgressBarFilled;
        } // OnCreateControl

        protected override Size DefaultSize
        {
            get { return new Size(125, 20); }
        } // DefaultSize

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (disposing)
            {
                if (BarBase != null) BarBase.Dispose();
                if (BarFilled != null) BarFilled.Dispose();
                BarBase = null;
                BarFilled = null;
            } // if
        } // Dispose

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (BarBase == null) return;

            var width = (int)(((progressValue - min) * BarFilled.Width) / ValueRange);

            e.Graphics.DrawImage(BarFilled, 0, 0, new Rectangle(0, 0, width, BarFilled.Height), GraphicsUnit.Pixel);
            e.Graphics.DrawImage(BarBase, width, 0, new Rectangle(width, 0, BarBase.Width - width, BarBase.Height), GraphicsUnit.Pixel);
        } // OnPaint
    } // class EpgProgressBarFixed
} // namespace

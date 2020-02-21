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

namespace IpTviewr.UiServices.EPG
{
    public class EpgProgressBarFixed: Control
    {
        private Image _barBase, _barFilled;
        private double _min, _max, _progressValue;

        public EpgProgressBarFixed()
        {
            _min = 0;
            _max = 100;
            _progressValue = _min;
        } // constructor

        [DefaultValue(0.0)]
        public double MinimumValue
        {
            get => _min;
            set
            {
                if (_min > _max) throw new ArgumentOutOfRangeException();
                _min = value;
                if (_progressValue < _min) Value = _min;
            } // set
        } // MinimumValue

        [DefaultValue(100.0)]
        public double MaximumValue
        {
            get => _max;
            set
            {
                if (_max < _min) throw new ArgumentOutOfRangeException();
                _max = value;
                if (_progressValue > _max) Value = _max;
            } // set
        } // MaximumValue

        public double ValueRange => _max - _min;

        [DefaultValue(0.0)]
        public double Value
        {
            get => _progressValue;
            set
            {
                var old = _progressValue;
                _progressValue = value;
                if (_progressValue < _min) _progressValue = _min;
                if (_progressValue > _max) _progressValue = _max;
                if (Math.Abs(_progressValue - old) > 0.01)
                {
                    Invalidate();
                } // if
            } // set
        } // Value

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            _barBase = Properties.Resources.ProgressBarBase;
            _barFilled = Properties.Resources.ProgressBarFilled;
        } // OnCreateControl

        protected override Size DefaultSize => new Size(125, 20);

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (!disposing) return;

            _barBase?.Dispose();
            _barFilled?.Dispose();
            _barBase = null;
            _barFilled = null;
        } // Dispose

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (_barBase == null) return;

            var width = (int)(((_progressValue - _min) * _barFilled.Width) / ValueRange);

            e.Graphics.DrawImage(_barFilled, 0, 0, new Rectangle(0, 0, width, _barFilled.Height), GraphicsUnit.Pixel);
            e.Graphics.DrawImage(_barBase, width, 0, new Rectangle(width, 0, _barBase.Width - width, _barBase.Height), GraphicsUnit.Pixel);
        } // OnPaint
    } // class EpgProgressBarFixed
} // namespace

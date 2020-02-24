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
using System.Text;
using System.Windows.Forms;

namespace IpTviewr.Internal.Tools.ChannelLogos
{
    public sealed class TextBoxOutputWriter : BaseOutputWriter, IToolOutputWriter
    {
        private readonly TextBox _textBox;
        private readonly Timer _timer;
        private readonly StringBuilder _buffer;

        public TextBoxOutputWriter(TextBox textBox, Timer timer = null, int indentSize = 4): base(indentSize)
        {
            _textBox = textBox ?? throw new ArgumentNullException(nameof(textBox));
            _timer = timer;
            _buffer = new StringBuilder();
        } // constructor

        public void Clear()
        {
            _textBox.Text = null;
            Reset();
        } // Clear

        public void Start(bool buffered = true)
        {
            Buffered = buffered;
            if (_timer != null)
            {
                _timer.Tick += TimerOnTick;
                _timer.Start();
            } // if
            lock (_buffer)
            {
                _buffer.Clear();
            } // lock

            Started = true;
        } // Start

        public void Stop()
        {
            EmptyBuffer();
            Started = false;

            if (_timer == null) return;

            _timer.Tick -= TimerOnTick;
            _timer.Stop();
        } // Stop

        public bool Buffered { get; private set; }

        private void TimerOnTick(object sender, EventArgs e)
        {
            EmptyBuffer();
        } // TimerOnTick

        private void EmptyBuffer()
        {
            lock (_buffer)
            {
                _textBox.Invoke(new Action(() =>
                {
                    _textBox.Text += _buffer.ToString();
                    _textBox.SelectionStart = _textBox.Text.Length;
                    _textBox.SelectionLength = 0;
                    _textBox.ScrollToCaret();
                }));

                _buffer.Clear();
            } // lock
        } // EmptyBuffer


        #region Implementation of IToolOutputWriter

        public void WriteLine()
        {
            if (!Started) return;

            if (!Buffered)
            {
                _textBox.BeginInvoke(new Action(() =>
                    _textBox.Text += Environment.NewLine));
            }
            else
            {
                lock (_buffer)
                {
                    _buffer.AppendLine();
                } // lock
            } // if-else
        } // WriteLine

        public override void WriteLine(string text)
        {
            if (!Started) return;

            if (!Buffered)
            {
                _textBox.BeginInvoke(new Action(() =>
                    _textBox.Text = _textBox.Text + Environment.NewLine + GetTimestamp() + GetIndent() + text));
            }
            else
            {
                lock (_buffer)
                {
                    AppendText(_buffer, text);
                } // lock
            } // if-else
        } // WriteLine

        public void WriteLine(string format, params object[] objects)
        {
            if (!Started) return;

            if (!Buffered)
            {
                _textBox.BeginInvoke(new Action(() =>
                    _textBox.Text = $@"{_textBox.Text}{Environment.NewLine}{GetTimestamp()}{GetIndent()}{string.Format(format, objects)}"));
            }
            else
            {
                lock (_buffer)
                {
                    AppendText(_buffer, null);
                    _buffer.AppendFormat(format, objects);
                    _buffer.AppendLine();
                } // lock
            } // if-else
        } // WriteLine

        #endregion
    } // class TextBoxOutputWriter
} // namespace

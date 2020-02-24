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
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AlphaCentaury.Tools.SourceCodeMaintenance.Helpers
{
    public sealed class AsyncHelper: IDisposable
    {
        private readonly Form _form;
        private readonly ToolStrip _toolStrip;
        private readonly List<bool> _toolStripItemsEnabled;
        private readonly ToolStripButton _cancelButton;
        private CancellationTokenSource _cancellationTokenSource;

        public AsyncHelper(Form form, ToolStrip toolStrip, ToolStripButton cancelButton, bool autoCancel = true)
        {
            _form = form ?? throw new ArgumentNullException(nameof(form));
            _toolStrip = toolStrip ?? throw new ArgumentNullException(nameof(form));
            _cancelButton = cancelButton ?? throw new ArgumentNullException(nameof(cancelButton));

            _toolStripItemsEnabled = new List<bool>(_toolStrip.Items.Count);
            _cancelButton.Enabled = false;
            if (autoCancel) _cancelButton.Click += CancelButtonOnClick;
        } // constructor

        public bool IsToolStripEnabled
        {
            get => _toolStripItemsEnabled.Count == 0;
            set
            {
                if (value && IsToolStripEnabled) return;
                if (value)
                {
                    // restore controls enabled state
                    for (var index = 0; index < _toolStripItemsEnabled.Count; index++)
                    {
                        _toolStrip.Items[index].Enabled = _toolStripItemsEnabled[index];
                    } // for index
                    _toolStripItemsEnabled.Clear();
                }
                else
                {
                    // saved controls enabled state
                    foreach (var item in _toolStrip.Items.Cast<ToolStripItem>())
                    {
                        _toolStripItemsEnabled.Add(item.Enabled);
                        item.Enabled = false;
                    } // foreach
                } // if-else

                _cancelButton.Enabled = !value;
            } // set
        } // IsToolStripEnabled

        public void BeginAsyncOperation()
        {
            IsToolStripEnabled = false;
            _form.UseWaitCursor = true;
            _toolStrip.UseWaitCursor = false;
            _toolStrip.Cursor = Cursors.Arrow;
            _cancellationTokenSource = new CancellationTokenSource();
        } // BeginAsyncOperation

        public CancellationToken GetCancellationToken()
        {
            if (_cancellationTokenSource == null) throw new NotSupportedException();
            return _cancellationTokenSource.Token;
        } // GetCancellationToken

        public void CancelAsyncOperation()
        {
            _cancellationTokenSource?.Cancel();
        } // CancelAsyncOperation

        public void EndAsyncOperation()
        {
            _cancellationTokenSource.Dispose();
            _cancellationTokenSource = null;
            _form.UseWaitCursor = false;
            _toolStrip.Cursor = Cursors.Default;
            Cursor.Current = Cursors.Default;
            IsToolStripEnabled = true;
        } // EndAsyncOperation

        public async void ExecuteAsync(TextBoxOutputWriter writer, Func<CancellationToken, Task> asyncAction)
        {
            try
            {
                writer.Clear();
                writer.Start();
                BeginAsyncOperation();

                await asyncAction(GetCancellationToken());
            }
            catch (OperationCanceledException)
            {
                // ignore
            } // catch
            catch (Exception ex)
            {
                writer.WriteException(ex);
            } // try-catch

            EndAsyncOperation();
            writer.Stop();
        } // ExecuteAsync

        private void CancelButtonOnClick(object sender, EventArgs e)
        {
            _cancelButton.Enabled = false;
            CancelAsyncOperation();
        } // CancelButtonOnClick

        #region Implementation of IDisposable

        public void Dispose()
        {
            _cancellationTokenSource?.Dispose();
            _cancelButton.Click -= CancelButtonOnClick;
        } // Dispose

        #endregion
    } // class AsyncHelper
} // namespace

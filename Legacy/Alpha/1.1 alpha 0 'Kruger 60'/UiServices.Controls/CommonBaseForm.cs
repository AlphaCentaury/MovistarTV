// Copyright (C) 2014-2015, Codeplex user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Project.DvbIpTv.UiServices.Controls
{
    public class CommonBaseForm : Form
    {
        #region Helper methods

        public event EventHandler<CommonBaseFormExceptionThrownEventArgs> ExceptionThrown;

        protected void HandleException(Exception ex)
        {
            OnExceptionThrown(this, new CommonBaseFormExceptionThrownEventArgs(ex));
        } // HandleException

        protected void HandleException(string message, Exception ex)
        {
            OnExceptionThrown(this, new CommonBaseFormExceptionThrownEventArgs(message, ex));
        } // HandleException

        /// <summary>
        /// Handles a caught exception, by displaying a (rather ugly) MessageBox. Descendants are encouraged to provide their own implementation.
        /// </summary>
        /// <param name="e">Caught exception information</param>
        /// <remarks>Descendants who override this method should not call base.HandleException</remarks>
        protected virtual void OnExceptionThrown(object sender, CommonBaseFormExceptionThrownEventArgs e)
        {
            if (ExceptionThrown != null)
            {
                ExceptionThrown(this, e);
            }
            else
            {
                var boxMessage = (e.Message == null) ? e.Exception.Message : e.Message + "\r\n" + e.Exception.Message;
                MessageBox.Show(this, Properties.Texts.UncaughtExceptionCaption, boxMessage, MessageBoxButtons.OK, MessageBoxIcon.Stop);
            } // if-else
        } // OnExceptionThrown

        protected void SafeDispose(IDisposable disposable)
        {
            if (disposable == null) return;
            disposable.Dispose();
        } // SafeDispose

        protected bool SafeCall(Action implementation)
        {
            try
            {
                implementation();

                return true;
            }
            catch (Exception ex)
            {
                HandleException(ex);
                return false;
            } // try-catch
        } // SafeCall

        protected bool SafeCall<T>(Action<T> implementation, T arg)
        {
            try
            {
                implementation(arg);

                return true;
            }
            catch (Exception ex)
            {
                HandleException(ex);
                return false;
            } // try-catch
        } // SafeCall<T>

        protected bool SafeCall<T1, T2>(Action<T1, T2> implementation, T1 arg1, T2 arg2)
        {
            try
            {
                implementation(arg1, arg2);

                return true;
            }
            catch (Exception ex)
            {
                HandleException(ex);
                return false;
            } // try-catch
        } // SafeCall<T1, T2>

        #endregion
    } // class CommonBaseForm
} // namespace

// Copyright (C) 2014-2019, GitHub/Codeplex user AlphaCentaury
// 
// All rights reserved, except those granted by the governing license of this software.
// See 'license.txt' file in the project root for complete license information.
// 
// http://www.alphacentaury.org/movistartv https://github.com/AlphaCentaury

using IpTviewr.Common;
using IpTviewr.Common.Telemetry;
using Microsoft.SqlServer.MessageBox;
using System;
using System.Windows.Forms;

namespace IpTviewr.UiServices.Common.Forms
{
    public class CommonBaseForm : Form
    {
        #region Exceptions

        protected bool HandleOwnedFormsExceptions
        {
            get;
            set;
        } // HandleOwnedFormsExceptions

        public Action<ExceptionEventData> GetExceptionHandler()
        {
            return HandleException;
        } // GetExceptionHandler

        /// <summary>
        /// Provides an unified way of handling exceptions.
        /// </summary>
        /// <param name="ex">The data about the exception, including additional information for the user, such a caption or a text</param>
        /// <remarks>If a parent CommonForm exists, the exception will be passed along.
        /// If no parent is found, the virtual method ExceptionHandler will be called</remarks>
        protected void HandleException(ExceptionEventData ex)
        {
            HandleOwnedFormException(this, ex);
        } // HandleException

        private void HandleOwnedFormException(CommonBaseForm form, ExceptionEventData ex)
        {
            if (!HandleOwnedFormsExceptions)
            {
                var parent = form.ParentForm as CommonBaseForm;
                parent?.HandleOwnedFormException(this, ex);
            } // if

            ExceptionHandler(form, ex);
        } // HandleOwnedFormException

        /// <summary>
        /// Exception handler.
        /// By default displays an ExceptionMessageBox. Descendants are encouraged to provide their own implementation.
        /// </summary>
        /// <param name="form">The form that 'throws' the exception</param>
        /// <param name="ex">The data about the exception, including additional information for the user, such a caption or a text</param>
        /// <remarks>Descendants who override this method MUST NOT call base.ExceptionHandler.
        /// This method MUST NOT be called directly. To handle and exception, HandleException MUST be used instead.
        /// </remarks>
        protected virtual void ExceptionHandler(CommonBaseForm form, ExceptionEventData ex)
        {
            BasicGoogleTelemetry.SendExtendedExceptionHit(ex.Exception, true, ex.Message, GetType().Name);

            var box = new ExceptionMessageBox()
            {
                Caption = Properties.CommonForm.UncaughtExceptionCaption,
                Buttons = ExceptionMessageBoxButtons.OK,
                Symbol = ExceptionMessageBoxSymbol.Stop,
            };

            if (ex.Message == null)
            {
                box.Text = ex.Exception.Message;
                box.Message = ex.Exception;
            }
            else
            {
                box.Text = ex.Message;
                box.InnerException = ex.Exception;
            } // if-else

            box.Show(form);
        } // ExceptionHandler

        #endregion

        #region 'Safe' functions

        protected void SafeDispose(IDisposable disposable)
        {
            disposable?.Dispose();
        } // SafeDispose

        protected bool SafeCall(Action callAction)
        {
            try
            {
                callAction();

                return true;
            }
            catch (Exception ex)
            {
                HandleException(new ExceptionEventData(ex));
                return false;
            } // try-catch
        } // SafeCall

        protected bool SafeCall<TEventArgs>(Action<object, TEventArgs> callAction, object sender, TEventArgs e) where TEventArgs : EventArgs
        {
            try
            {
                callAction(sender, e);

                return true;
            }
            catch (Exception ex)
            {
                HandleException(new ExceptionEventData(ex));
                return false;
            } // try-catch
        } // SafeCall

        protected bool SafeCall<T>(Action<T> callAction, T arg)
        {
            try
            {
                callAction(arg);

                return true;
            }
            catch (Exception ex)
            {
                HandleException(new ExceptionEventData(ex));
                return false;
            } // try-catch
        } // SafeCall<T>

        protected bool SafeCall<T1, T2>(Action<T1, T2> callAction, T1 arg1, T2 arg2)
        {
            try
            {
                callAction(arg1, arg2);

                return true;
            }
            catch (Exception ex)
            {
                HandleException(new ExceptionEventData(ex));
                return false;
            } // try-catch
        } // SafeCall<T1, T2>

        #endregion
    } // class CommonBaseForm
} // namespace

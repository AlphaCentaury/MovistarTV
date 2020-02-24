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

using IpTviewr.Common;
using IpTviewr.Common.Telemetry;
using IpTviewr.UiServices.Common.Forms;
using Microsoft.SqlServer.MessageBox;
using System;
using System.Windows.Forms;

namespace IpTviewr.UiServices.Common.Controls
{
    public class CommonBaseUserControl: UserControl
    {
        #region Exceptions

        public Action<ExceptionEventData> GetExceptionHandler()
        {
            return HandleException;
        } // GetExceptionHandler

        protected bool HandleMyOwnExceptions
        {
            get;
            set;
        } // HandleMyOwnExceptions

        /// <summary>
        /// Provides an unified way of handling exceptions.
        /// </summary>
        /// <param name="ex">The data about the exception, including additional information for the user, such a caption or a text</param>
        /// <remarks>If a parent CommonForm exists, the exception will be passed along.
        /// If no parent is found, the virtual method ExceptionHandler will be called</remarks>
        protected void HandleException(ExceptionEventData ex)
        {
            HandleOwnedFormException(ex);
        } // HandleException

        private void HandleOwnedFormException(ExceptionEventData ex)
        {
            if (!HandleMyOwnExceptions)
            {
                var parent = ParentForm as CommonBaseForm;
                parent?.GetExceptionHandler()(ex);
            } // if

            ExceptionHandler(ex);
        } // HandleOwnedFormException

        /// <summary>
        /// Exception handler.
        /// By default displays an ExceptionMessageBox. Descendants are encouraged to provide their own implementation.
        /// </summary>
        /// <param name="ex">The data about the exception, including additional information for the user, such a caption or a text</param>
        /// <remarks>Descendants who override this method MUST NOT call base.ExceptionHandler.
        /// This method MUST NOT be called directly. To handle and exception, HandleException MUST be used instead.
        /// </remarks>
        protected virtual void ExceptionHandler(ExceptionEventData ex)
        {
            AppTelemetry.ScreenException(ex.Exception, GetType().FullName, ex.Message);

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

            box.Show(ParentForm);
        } // ExceptionHandler

#endregion

        #region 'Safe' functions

        protected void SafeDispose(IDisposable disposable)
        {
            disposable?.Dispose();
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
                HandleException(new ExceptionEventData(ex));
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
                HandleException(new ExceptionEventData(ex));
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
                HandleException(new ExceptionEventData(ex));
                return false;
            } // try-catch
        } // SafeCall<T1, T2>

        #endregion
    } // UserControl
} // namespace

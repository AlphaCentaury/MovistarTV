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
using Microsoft.SqlServer.MessageBox;
using System;
using System.Windows.Forms;

namespace IpTviewr.UiServices.Common.Forms
{
    public class CommonBaseForm : SafeForm
    {
        #region Telemetry

        #region Overrides of Form

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (SendLoadEvent) AppTelemetry.FormEvent(AppTelemetry.LoadEvent, this);
        } // OnLoad

        protected virtual bool SendLoadEvent => true;

#if DEBUG
        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            Text = @$"[DEBUG] {Text}";
        } // OnShown
#endif

        #endregion

        #endregion

        #region Exceptions handling

        protected override void ExceptionHandler(SafeForm form, ExceptionEventData ex)
        {
            AppTelemetry.FormException(ex.Exception, this);

            base.ExceptionHandler(form, ex);
        } // ExceptionHandler

        #endregion

        #region Helper methods

        protected void BeginInvoke(Action action)
        {
            BeginInvoke((Delegate)action);
        } // BeginInvoke

        protected void BeginInvoke<T>(Action<T> action, T param)
        {
            BeginInvoke((Delegate)action, param);
        } // BeginInvoke

        protected void BeginInvoke<T1, T2>(Action<T1, T2> action, T1 param1, T2 param2)
        {
            BeginInvoke((Delegate)action, param1, param2);
        } // BeginInvoke

        protected void BeginInvoke<T1, T2, T3>(Action<T1, T2, T3> action, T1 param1, T2 param2, T3 param3)
        {
            BeginInvoke((Delegate)action, param1, param2, param3);
        } // BeginInvoke

        public static void BeginInvoke(Form form, Action action)
        {
            form.BeginInvoke(action);
        } // BeginInvoke

        public static void BeginInvoke<T>(Form form, Action<T> action, T param)
        {
            form.BeginInvoke(action, param);
        } // BeginInvoke

        public static void BeginInvoke<T1, T2>(Form form, Action<T1, T2> action, T1 param1, T2 param2)
        {
            form.BeginInvoke(action, param1, param2);
        } // BeginInvoke

        public static void BeginInvoke<T1, T2, T3>(Form form, Action<T1, T2, T3> action, T1 param1, T2 param2, T3 param3)
        {
            form.BeginInvoke(action, param1, param2, param3);
        } // BeginInvoke

        #endregion
    } // class CommonBaseForm
} // namespace

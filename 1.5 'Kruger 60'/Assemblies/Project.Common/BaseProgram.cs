using System;
using System.Windows.Forms;
using Microsoft.SqlServer.MessageBox;

namespace IpTviewr.Common
{
    public class BaseProgram
    {
        public static void HandleException(IWin32Window owner, string message, Exception ex)
        {
            HandleException(owner,
                null,
                message,
                MessageBoxIcon.Error,
                ex);
        } // HandleException

        public static void HandleException(IWin32Window owner, string caption, string message, Exception ex)
        {
            HandleException(owner,
                caption,
                message,
                MessageBoxIcon.Error,
                ex);
        } // HandleException

        public static void HandleException(IWin32Window owner, string caption, string message, MessageBoxIcon icon, Exception ex)
        {
            AddExceptionAdvancedInformation(ex);

            var box = new ExceptionMessageBox()
            {
                Caption = caption ?? Properties.Texts.MyAppHandleExceptionDefaultCaption,
                Text = message ?? Properties.Texts.MyAppHandleExceptionDefaultMessage,
                InnerException = ex,
                Beep = true,
                Symbol = TranslateIconToSymbol(icon),
            };
            box.Show(owner);
        } // HandleException

        public static void HandleException(object sender, HandleExceptionEventArgs e)
        {
            HandleException(e.OwnerForm, e.Caption, e.Message, e.Exception);
        } // HandleException

        private static ExceptionMessageBoxSymbol TranslateIconToSymbol(MessageBoxIcon icon)
        {
            switch (icon)
            {
                case MessageBoxIcon.Asterisk: return ExceptionMessageBoxSymbol.Asterisk;
                case MessageBoxIcon.Error: return ExceptionMessageBoxSymbol.Error;
                case MessageBoxIcon.Exclamation: return ExceptionMessageBoxSymbol.Exclamation;
                //case MessageBoxIcon.Hand: return ExceptionMessageBoxSymbol.Hand;
                //case MessageBoxIcon.Information: return ExceptionMessageBoxSymbol.Information;
                case MessageBoxIcon.Question: return ExceptionMessageBoxSymbol.Question;
                //case MessageBoxIcon.Stop: return ExceptionMessageBoxSymbol.Stop;
                //case MessageBoxIcon.Warning: return ExceptionMessageBoxSymbol.Warning;
                default:
                    return ExceptionMessageBoxSymbol.None;
            } // switch
        } // TranslateIconToSymbol

        private static void AddExceptionAdvancedInformation(Exception ex)
        {
            while (ex != null)
            {
                ex.Data["AdvancedInformation.Exception.Type"] = ex.GetType().FullName;
                ex.Data["AdvancedInformation.Exception.Assembly"] = ex.GetType().Assembly.ToString();
                ex = ex.InnerException;
            } // while
        } // AddExceptionAdvancedInformation
    } // class BaseProgram
} // namespace

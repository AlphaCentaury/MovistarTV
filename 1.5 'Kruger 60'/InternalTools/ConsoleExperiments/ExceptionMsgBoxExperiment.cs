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

using AlphaCentaury.WindowsForms.MsgBoxEx;
using Microsoft.SqlServer.MessageBox;
using System;
using System.Windows.Forms;

namespace IpTviewr.Internal.Tools.ConsoleExperiments
{
    internal class ExceptionMsgBoxExperiment : Experiment
    {
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool SetProcessDPIAware();

        protected override int Run(string[] args)
        {
            if (Environment.OSVersion.Version.Major >= 6)
                SetProcessDPIAware();

            var box = new ExceptionMessageBox();
            //box.Message = new NullReferenceException("null", new IndexOutOfRangeException("bounds", new FormatException()));
            //box.Message.Data.Add("AdvancedInformation.2", "Advanced 2");
            box.Message = new ArgumentNullException();
            box.InnerException = new ArgumentException();
            box.InnerException.Data.Add("AdvancedInformation.1", "Advanced");
            //box.Text = "Text";
            box.Caption = "Caption";
            box.Data.Add("AdvancedInformation.3", "advanced 3");
            //box.Buttons = ExceptionMessageBoxButtons.AbortRetryIgnore;
            box.Buttons = ExceptionMessageBoxButtons.YesNo;
            box.DefaultButton = ExceptionMessageBoxDefaultButton.Button2;
            //box.Show(null);

            DialogResult result;

            result = MessageBox.Show("Hello world!", "Caption", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2);
            Console.WriteLine(result);

            result = MessageBoxEx.Show("Hello world! from MessageBoxEx", null, MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2);
            Console.WriteLine(result);

            result = MessageBox.Show("Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.", null, MessageBoxButtons.OKCancel, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2);
            Console.WriteLine(result);

            result = MessageBoxEx.Show("Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.", null, MessageBoxButtons.OKCancel, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2);
            Console.WriteLine(result);

            result = MessageBoxEx.Show("OOPS!", new FormatException(null, new ArgumentNullException(null, new ArgumentOutOfRangeException(null, new OutOfMemoryException(null, new InsufficientExecutionStackException())))));
            Console.WriteLine(result);

            return 0;
        } // Run
    } // class ExceptionMsgBoxExperiment

    internal class NullHWnd : IWin32Window
    {
        public IntPtr Handle => IntPtr.Zero;
    } // class NullHWnd
} // class

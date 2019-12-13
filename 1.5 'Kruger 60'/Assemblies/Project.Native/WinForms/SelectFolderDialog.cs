// Copyright (C) 2014-2019, GitHub/Codeplex user AlphaCentaury
// 
// All rights reserved, except those granted by the governing license of this software.
// See 'license.txt' file in the project root for complete license information.
// 
// http://www.alphacentaury.org/movistartv https://github.com/AlphaCentaury

using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.ComponentModel;
using System.Security.Permissions;
using System.Security;
using System.Threading;
using System.Drawing;
using JetBrains.Annotations;

namespace IpTviewr.Native.WinForms
{
    /// <summary>Displays a dialog box from which the user can select a folder.</summary>
    /// <remarks>Requires Windows Vista or better.</remarks>

    public class SelectFolderDialog : CommonDialog
    {
        private OpenFileDialog x;

        public SelectFolderDialog()
        {
            SetDefaultValues();
        } // constructor

        #region Basic properties

        /// <summary>Gets or sets the initial directory displayed by the folder dialog box.</summary>
        /// <returns>The initial directory displayed by the folder dialog box. The default is an empty string ("").</returns>
        [DefaultValue("")]
        public string InitialDirectory { get; set; }

        /// <summary>Gets or sets a string containing the folder name selected in the dialog box.</summary>
        /// <returns>The folder name selected in the dialog box. The default value is an empty string ("").</returns>
        [DefaultValue("")]
        public string SelectedPath { get; set; }

        /// <summary>Gets or sets a value indicating whether the dialog box displays a warning if the user specifies a path that does not exist.</summary>
        /// <returns>
        /// <see langword="true" /> if the dialog box displays a warning when the user specifies a path that does not exist; otherwise, <see langword="false" />. The default value is <see langword="true" />.</returns>
        [DefaultValue(true)]
        public bool CheckPathExists { get; set; }

        #endregion

        #region Overrides of CommonDialog

        public override void Reset()
        {
            SetDefaultValues();
        } // Reset

        protected override bool RunDialog(IntPtr hwndOwner)
        {
            Win32Shell.IFileOpenDialog dialog = null;

            try
            {
                dialog = CreateDialog();

                var result = dialog.Show(hwndOwner);
                if (result != 0) return false;

                dialog.GetResult(out var item);
                item.GetDisplayName(Win32Shell.GetDisplayName.FILESYSPATH, out var path);
                Marshal.ReleaseComObject(item);
                SelectedPath = path;

                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                if (dialog != null) Marshal.ReleaseComObject(dialog);
            } // finally
        } // RunDialog

        #endregion

        private void SetDefaultValues()
        {
            InitialDirectory = string.Empty;
            SelectedPath = string.Empty;
            CheckPathExists = true;
        } // SetDefaultValues

        private Win32Shell.IFileOpenDialog CreateDialog()
        {
            // ReSharper disable once SuspiciousTypeConversion.Global
            // COM type cast
            var dialog = (Win32Shell.IFileOpenDialog)new Win32Shell.FileOpenDialog();

            var options = Win32Shell.FileOpenDialogOptions.PICKFOLDERS |
                          Win32Shell.FileOpenDialogOptions.FORCEFILESYSTEM |
                          Win32Shell.FileOpenDialogOptions.DONTADDTORECENT;
            if (CheckPathExists) options |= Win32Shell.FileOpenDialogOptions.PATHMUSTEXIST;
            dialog.SetOptions(options);

            SetPath(SelectedPath, dialog.SetFolder);
            SetPath(InitialDirectory, dialog.SetDefaultFolder);

            return dialog;

            void SetPath(string path, Action<Win32Shell.IShellItem> action)
            {
                if (string.IsNullOrEmpty(path)) return;

                uint attributes = 0;
                if ((Win32Shell.SHILCreateFromPath(SelectedPath, out var idl, ref attributes) == 0) &&
                    (Win32Shell.SHCreateShellItem(IntPtr.Zero, IntPtr.Zero, idl, out var item) == 0))
                {
                    action(item);
                } // if

                if (idl != IntPtr.Zero) Marshal.FreeCoTaskMem(idl);
            } // local SetPath
        } // CreateDialog
    } // class SelectFolderDialog
} // namespace

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
    /*
    <ThirdParty>
      <Component name="DotNetZip FolderBrowserDialogEx" type="source">
        <License>Ms-PL</License>
        <Authors>Dino Chiesa</Authors>
        <Copyright>Copyright © Dino Chiesa 2006 - 2011</Copyright>
        <Description>DotNetZip is a FAST, FREE class library and toolset for manipulating zip files. Use VB, C# or any .NET language to easily create, extract, or update zip files.</Description>
        <Remarks>Code has been refactored and modified to fix bugs and use Vista or later SH functions</Remarks>
      </Component>
    </ThirdParty>
    */

    /*
    The following code has been copied verbatim from the http://dotnetzip.codeplex.com project.

    It has been modified where needed to changed the class name from FolderBrowserDialogEx to
    SelectFolderDialog. http://dotnetzip.codeplex.com/SourceControl/changeset/view/29832#432677, 
    address Code Analysis issues,
    add C# 8.0 language constructs,
    organize the code in regions for legibility and
    use Vista or later SH functions.

    The code is licensed under the Ms-PL license (http://dotnetzip.codeplex.com/license)
    */

    // FolderBrowserDialogEx.cs
    //
    // A replacement for the builtin System.Windows.Forms.FolderBrowserDialog class.
    // This one includes an edit box, and also displays the full path in the edit box. 
    //
    // based on code from http://support.microsoft.com/default.aspx?scid=kb;[LN];306285 
    // 
    // 20 Feb 2009
    //
    // ========================================================================================
    // Example usage:
    // 
    // string _folderName = "c:\\dinoch";
    // private void button1_Click(object sender, EventArgs e)
    // {
    //     _folderName = (System.IO.Directory.Exists(_folderName)) ? _folderName : "";
    //     var dlg1 = new Ionic.Utils.FolderBrowserDialogEx
    //     {
    //         Description = "Select a folder for the extracted files:",
    //         ShowNewFolderButton = true,
    //         ShowEditBox = true,
    //         //NewStyle = false,
    //         SelectedPath = _folderName,
    //         ShowFullPathInEditBox= false,
    //     };
    //     dlg1.RootFolder = System.Environment.SpecialFolder.MyComputer;
    // 
    //     var result = dlg1.ShowDialog();
    // 
    //     if (result == DialogResult.OK)
    //     {
    //         _folderName = dlg1.SelectedPath;
    //         this.label1.Text = "The folder selected was: ";
    //         this.label2.Text = _folderName;
    //     }
    // }
    //

    [Designer("System.Windows.Forms.Design.FolderBrowserDialogDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
    [DefaultEvent("HelpRequest")]
    [DefaultProperty("SelectedPath")]
    [ToolboxBitmap(typeof(FolderBrowserDialog))]
    public class SelectFolderDialog : CommonDialog
    {
        //private static readonly int MaxPath = 260;
        private const int PathSize = 1024;

        // Fields
        private string _descriptionText;
        private Environment.SpecialFolder _rootFolder;
        private string _selectedPath;
        private bool _selectedPathNeedsCheck;
        private int _uiFlags;
        private IntPtr _hwndEdit;
        private IntPtr _rootFolderLocation;

        // Events
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public new event EventHandler HelpRequest
        {
            add => base.HelpRequest += value;
            remove => base.HelpRequest -= value;
        } // HelpRequest

        // ctor
        public SelectFolderDialog()
        {
            SetDefaultValues();
        } // SelectFolderDialog

        // Factory Methods
        public static SelectFolderDialog PrinterBrowser()
        {
            var dialog = new SelectFolderDialog();
            // avoid MBRO compiler warning when passing _rootFolderLocation as a ref:
            dialog.BecomePrinterBrowser();
            return dialog;
        } // PrinterBrowser

        public static SelectFolderDialog ComputerBrowser()
        {
            var dialog = new SelectFolderDialog();
            // avoid MBRO compiler warning when passing _rootFolderLocation as a ref:
            dialog.BecomeComputerBrowser();
            return dialog;
        } // ComputerBrowser

        #region Properties

        /// <summary>
        /// This description appears near the top of the dialog box, providing direction to the user.
        /// </summary>
        [DefaultValue("")]
        public string Description
        {
            get => _descriptionText;
            set => _descriptionText = value ?? string.Empty;
        } // Description

        [DefaultValue(0)]
        public Environment.SpecialFolder RootFolder
        {
            get => _rootFolder;
            set
            {
                if (!Enum.IsDefined(typeof(Environment.SpecialFolder), value))
                {
                    throw new InvalidEnumArgumentException(nameof(value), (int)value, typeof(Environment.SpecialFolder));
                }
                _rootFolder = value;
            }
        } // RootFolder

        /// <summary>
        /// Set or get the selected path.  
        /// </summary>
        public string SelectedPath
        {
            get
            {
                if (string.IsNullOrEmpty(_selectedPath) || !_selectedPathNeedsCheck) return _selectedPath;

                new FileIOPermission(FileIOPermissionAccess.PathDiscovery, _selectedPath).Demand();
                _selectedPathNeedsCheck = false;
                return _selectedPath;
            } // get
            set
            {
                _selectedPath = value ?? string.Empty;
                _selectedPathNeedsCheck = true;
            } // set
        } // SelectedPath

        /// <summary>
        /// Enable or disable the "New Folder" button in the browser dialog.
        /// </summary>
        [DefaultValue(true)]
        public bool ShowNewFolderButton { get; set; }

        /// <summary>
        /// Show an "edit box" in the folder browser.
        /// </summary>
        /// <remarks>
        /// The "edit box" normally shows the name of the selected folder.  
        /// The user may also type a pathname directly into the edit box.  
        /// </remarks>
        /// <seealso cref="ShowFullPathInEditBox"/>
        [DefaultValue(true)]
        public bool ShowEditBox { get; set; }

        /// <summary>
        /// Set whether to use the New Folder Browser dialog style.
        /// </summary>
        /// <remarks>
        /// The new style is resizable and includes a "New Folder" button.
        /// </remarks>
        [DefaultValue(true)]
        public bool NewStyle { get; set; } = true; // NewStyle

        [DefaultValue(false)]
        public bool DontIncludeNetworkFoldersBelowDomainLevel { get; set; }

        /// <summary>
        /// Show the full path in the edit box as the user selects it. 
        /// </summary>
        /// <remarks>
        /// This works only if ShowEditBox is also set to true. 
        /// </remarks>
        [DefaultValue(true)]
        public bool ShowFullPathInEditBox { get; set; } = true;

        [DefaultValue(false)]
        public bool ShowBothFilesAndFolders { get; set; }

        #endregion

        private int FolderBrowserCallback(IntPtr hwnd, uint msg, IntPtr lParam, IntPtr lpData)
        {
            switch (msg)
            {
                case BrowseForFolderMessages.BffmInitialized:
                    BffmInitialized();
                    break;

                case BrowseForFolderMessages.BffmSelChanged:
                    BffmSelChanged();
                    break;
            }
            return 0;

            void BffmInitialized()
            {
                // @AlphaCentaury: get handle to the edit box whether there's an initial path or not
                if (ShowEditBox && ShowFullPathInEditBox)
                {
                    // get handle to the Edit box inside the Folder Browser Dialog
                    _hwndEdit = UnsafeNativeMethods.User32.FindWindowExW(new HandleRef(null, hwnd), IntPtr.Zero, "Edit", null);
                    UnsafeNativeMethods.User32.SetWindowTextW(_hwndEdit, _selectedPath);
                } // if

                if (_selectedPath.Length != 0)
                {
                    UnsafeNativeMethods.User32.SendMessage(new HandleRef(null, hwnd), BrowseForFolderMessages.BffmSetSelectionW, (IntPtr)1, _selectedPath);
                    // @AlphaCentaury: moved to top to get handle to the edit box whether there's an initial path or not
                    //if (_showEditBox && _showFullPathInEditBox)
                    //{
                    //    // get handle to the Edit box inside the Folder Browser Dialog
                    //    _hwndEdit = UnsafeNativeMethods.User32.FindWindowExW(new HandleRef(null, hwnd), IntPtr.Zero, "Edit", null);
                    //    UnsafeNativeMethods.User32.SetWindowTextW(_hwndEdit, _selectedPath);
                    //}
                } // if
            } // BffmInitialized

            void BffmSelChanged()
            {
                var pidl = lParam;
                if (pidl == IntPtr.Zero) return;

                if (((_uiFlags & BrowseFlags.BifBrowseForPrinter) == BrowseFlags.BifBrowseForPrinter) ||
                    ((_uiFlags & BrowseFlags.BifBrowseForComputer) == BrowseFlags.BifBrowseForComputer))
                {
                    // we're browsing for a printer or computer, enable the OK button unconditionally.
                    UnsafeNativeMethods.User32.SendMessage(new HandleRef(null, hwnd), BrowseForFolderMessages.BffmEnableOk, (IntPtr)0, (IntPtr)1);
                    return;
                } // if

                //var pszPath = Marshal.AllocHGlobal(MaxPath * Marshal.SystemDefaultCharSize);
                var pszPath = Marshal.AllocHGlobal((PathSize + 1) * Marshal.SystemDefaultCharSize);

                // @AlphaCentaury: allows to get paths longer than MAX_PATH (260 chars)
                //var haveValidPath = UnsafeNativeMethods.Shell32.SHGetPathFromIDList(pidl, pszPath);
                var haveValidPath = UnsafeNativeMethods.Shell32.SHGetPathFromIDListEx(pidl, pszPath, PathSize, 0);
                var displayedPath = Marshal.PtrToStringAuto(pszPath);
                Marshal.FreeHGlobal(pszPath);

                // whether to enable the OK button or not. (if file is valid)
                UnsafeNativeMethods.User32.SendMessage(new HandleRef(null, hwnd), BrowseForFolderMessages.BffmEnableOk, (IntPtr)0, (IntPtr)(haveValidPath ? 1 : 0));

                // Maybe set the Edit Box text to the Full Folder path
                if (!haveValidPath || string.IsNullOrEmpty(displayedPath)) return;

                if (ShowEditBox && ShowFullPathInEditBox && ((_hwndEdit != IntPtr.Zero)))
                {
                    UnsafeNativeMethods.User32.SetWindowTextW(_hwndEdit, displayedPath);
                } // if

                if ((_uiFlags & BrowseFlags.BifStatusText) == BrowseFlags.BifStatusText)
                {
                    UnsafeNativeMethods.User32.SendMessage(new HandleRef(null, hwnd), BrowseForFolderMessages.BffmSetStatusText, (IntPtr)0, displayedPath);
                } // if
            } // BffmSelChanged
        } // FolderBrowserCallback

        private void SetDefaultValues()
        {
            _rootFolder = 0;
            _descriptionText = string.Empty;
            _selectedPath = string.Empty;
            _selectedPathNeedsCheck = false;
            ShowNewFolderButton = true;
            ShowEditBox = true;
            NewStyle = true;
            DontIncludeNetworkFoldersBelowDomainLevel = false;
            _hwndEdit = IntPtr.Zero;
            _rootFolderLocation = IntPtr.Zero;
        } // SetDefaultValues

        #region CommonDialog implementation

        public override void Reset()
        {
            SetDefaultValues();
        } // Reset

        [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
        protected override bool RunDialog(IntPtr hWndOwner)
        {
            var result = false;
            var pidl = IntPtr.Zero;
            var hGlobal = IntPtr.Zero;
            var pszPath = IntPtr.Zero;

            PrepareDialogInitialization(hWndOwner);

            try
            {
                hGlobal = Marshal.AllocHGlobal((PathSize + 1) * Marshal.SystemDefaultCharSize);
                pszPath = Marshal.AllocHGlobal((PathSize + 1) * Marshal.SystemDefaultCharSize);

                var browseInfo = new UnsafeNativeMethods.BrowseInfo
                {
                    pidlRoot = _rootFolderLocation,
                    Owner = hWndOwner,
                    pszDisplayName = hGlobal,
                    Title = _descriptionText,
                    Flags = _uiFlags,
                    callback = FolderBrowserCallback,
                    lParam = IntPtr.Zero,
                    iImage = 0
                };

                // display dialog
                pidl = UnsafeNativeMethods.Shell32.SHBrowseForFolderW(browseInfo);

                result = GetDialogResult(browseInfo, pidl, pszPath);
            }
            finally
            {
                var sHMalloc = UnsafeNativeMethods.GetShMalloc();
                sHMalloc.Free(_rootFolderLocation);
                _rootFolderLocation = IntPtr.Zero;
                if (pidl != IntPtr.Zero) sHMalloc.Free(pidl);
                if (pszPath != IntPtr.Zero) Marshal.FreeHGlobal(pszPath);
                if (hGlobal != IntPtr.Zero) Marshal.FreeHGlobal(hGlobal);
            } // try-finally

            return result;
        } // RunDialog

        private void PrepareDialogInitialization(IntPtr hWndOwner)
        {
            if (Control.CheckForIllegalCrossThreadCalls && (Application.OleRequired() != ApartmentState.STA))
            {
                throw new ThreadStateException("DebuggingException: ThreadMustBeSTA");
            } // if

            if (_rootFolderLocation == IntPtr.Zero)
            {
                UnsafeNativeMethods.Shell32.SHGetSpecialFolderLocation(hWndOwner, (int)_rootFolder, ref _rootFolderLocation);
                if (_rootFolderLocation == IntPtr.Zero)
                {
                    UnsafeNativeMethods.Shell32.SHGetSpecialFolderLocation(hWndOwner, 0, ref _rootFolderLocation);
                    if (_rootFolderLocation == IntPtr.Zero)
                    {
                        throw new InvalidOperationException("FolderBrowserDialogNoRootFolder");
                    } // if
                } // if
            } // if

            // get uiFlags according to user display choices
            _hwndEdit = IntPtr.Zero;
            _uiFlags = 0;
            if (DontIncludeNetworkFoldersBelowDomainLevel) _uiFlags += BrowseFlags.BifDontGoBelowDomain;
            if (NewStyle) _uiFlags += BrowseFlags.BifNewDialogStyle;
            if (!ShowNewFolderButton) _uiFlags += BrowseFlags.BifNoNewFolderButton;
            if (ShowEditBox) _uiFlags += BrowseFlags.BifEditBox;
            if (ShowBothFilesAndFolders) _uiFlags += BrowseFlags.BifBrowseIncludeFiles;
        } // PrepareDialogInitialization

        private bool GetDialogResult(UnsafeNativeMethods.BrowseInfo browseInfo, IntPtr pidl, IntPtr pszPath)
        {
            if (((_uiFlags & BrowseFlags.BifBrowseForPrinter) == BrowseFlags.BifBrowseForPrinter) ||
                ((_uiFlags & BrowseFlags.BifBrowseForComputer) == BrowseFlags.BifBrowseForComputer))
            {
                _selectedPath = Marshal.PtrToStringAuto(browseInfo.pszDisplayName);
                return true;
            } // if

            if (pidl == IntPtr.Zero) return false;

            // @AlphaCentaury: allows to get paths longer than MAX_PATH (260 chars)
            //UnsafeNativeMethods.Shell32.SHGetPathFromIDList(pidl, pszPath);
            UnsafeNativeMethods.Shell32.SHGetPathFromIDListEx(pidl, pszPath, PathSize, 0);
            _selectedPathNeedsCheck = true;
            _selectedPath = Marshal.PtrToStringAuto(pszPath);
            return true;
        } // GetDialogResult

        #endregion

        #region Helpers

        private void BecomePrinterBrowser()
        {
            _uiFlags += BrowseFlags.BifBrowseForPrinter;
            Description = "Select a printer:";
            UnsafeNativeMethods.Shell32.SHGetSpecialFolderLocation(IntPtr.Zero, CsIdl.Printers, ref _rootFolderLocation);
            ShowNewFolderButton = false;
            ShowEditBox = false;
        } // BecomePrinterBrowser

        private void BecomeComputerBrowser()
        {
            _uiFlags += BrowseFlags.BifBrowseForComputer;
            Description = "Select a computer:";
            UnsafeNativeMethods.Shell32.SHGetSpecialFolderLocation(IntPtr.Zero, CsIdl.Network, ref _rootFolderLocation);
            ShowNewFolderButton = false;
            ShowEditBox = false;
        } // BecomeComputerBrowser

        private static class CsIdl
        {
            public const int Printers = 4;
            public const int Network = 0x12;
        } // class CsIdl

        [PublicAPI]
        private static class BrowseFlags
        {
            public const int BifDefault = 0x0000;
            public const int BifBrowseForComputer = 0x1000;
            public const int BifBrowseForPrinter = 0x2000;
            public const int BifBrowseIncludeFiles = 0x4000;
            public const int BifBrowseIncludeUrls = 0x0080;
            public const int BifDontGoBelowDomain = 0x0002;
            public const int BifEditBox = 0x0010;
            public const int BifNewDialogStyle = 0x0040;
            public const int BifNoNewFolderButton = 0x0200;
            public const int BifReturnFsAncestors = 0x0008;
            public const int BifReturnOnlyFsDirs = 0x0001;
            public const int BifShareable = 0x8000;
            public const int BifStatusText = 0x0004;
            public const int BifUaHint = 0x0100;
            public const int BifValidate = 0x0020;
            public const int BifNoTranslateTargets = 0x0400;
        } // class BrowseFlags

        [PublicAPI]
        private static class BrowseForFolderMessages
        {
            // messages FROM the folder browser
            public const int BffmInitialized = 1;
            public const int BffmSelChanged = 2;
            public const int BffmValidateFailedA = 3;
            public const int BffmValidateFailedW = 4;
            public const int BffmIunknown = 5;

            // messages TO the folder browser
            public const int BffmSetStatusText = 0x464;
            public const int BffmEnableOk = 0x465;
            public const int BffmSetSelectionA = 0x466;
            public const int BffmSetSelectionW = 0x467;
        } // class BrowseForFolderMessages

        #endregion
    } // class SelectFolderDialog

    internal static class UnsafeNativeMethods
    {
        public delegate int BrowseFolderCallbackProc(IntPtr hwnd, uint msg, IntPtr lParam, IntPtr lpData);

        public static IMalloc GetShMalloc()
        {
            var ppMalloc = new UnsafeNativeMethods.IMalloc[1];
            Shell32.SHGetMalloc(ppMalloc);
            return ppMalloc[0];
        } // GetSHMalloc

        internal static class User32
        {
            [DllImport("user32.dll", CharSet = CharSet.Unicode)]
            public static extern IntPtr SendMessage(HandleRef hWnd, uint msg, IntPtr wParam, string lParam);

            [DllImport("user32.dll")]
            public static extern IntPtr SendMessage(HandleRef hWnd, uint msg, IntPtr wParam, IntPtr lParam);

            [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
            public static extern IntPtr FindWindowExW(HandleRef hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);

            [DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
            public static extern bool SetWindowTextW(IntPtr hWnd, string text);
        } // class User32

        [ComImport, Guid("00000002-0000-0000-c000-000000000046"), SuppressUnmanagedCodeSecurity, InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        public interface IMalloc
        {
            [PreserveSig]
            IntPtr Alloc(int cb);
            [PreserveSig]
            IntPtr Realloc(IntPtr pv, int cb);
            [PreserveSig]
            void Free(IntPtr pv);
            [PreserveSig]
            int GetSize(IntPtr pv);
            [PreserveSig]
            int DidAlloc(IntPtr pv);
            [PreserveSig]
            void HeapMinimize();
        } // interface IMalloc

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public class BrowseInfo
        {
            public IntPtr Owner;
            public IntPtr pidlRoot;
            public IntPtr pszDisplayName;
            [MarshalAs(UnmanagedType.LPWStr)]
            public string Title;
            public int Flags;
            public BrowseFolderCallbackProc callback;
            public IntPtr lParam;
            public int iImage;
        } // class RunDialog

        [SuppressUnmanagedCodeSecurity]
        internal static class Shell32
        {
            // Methods
            [DllImport("shell32.dll", CharSet = CharSet.Unicode)]
            public static extern IntPtr SHBrowseForFolderW([In] BrowseInfo lpbi);
            [DllImport("shell32.dll")]
            public static extern int SHGetMalloc([Out, MarshalAs(UnmanagedType.LPArray)] IMalloc[] ppMalloc);
            [DllImport("shell32.dll", CharSet = CharSet.Auto)]
            public static extern bool SHGetPathFromIDList(IntPtr pidl, IntPtr pszPath);
            [DllImport("shell32.dll", CharSet = CharSet.Auto)]
            public static extern bool SHGetPathFromIDListEx(IntPtr pidl, IntPtr pszPath, int cchPath, int uOpts);
            [DllImport("shell32.dll")]
            public static extern int SHGetSpecialFolderLocation(IntPtr hwnd, int csidl, ref IntPtr ppidl);
        } // internal static class Shell32
    } // class UnsafeNativeMethods
} // namespace

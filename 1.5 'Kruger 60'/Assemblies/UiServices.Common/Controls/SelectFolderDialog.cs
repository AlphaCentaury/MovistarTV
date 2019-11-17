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

namespace IpTviewr.UiServices.Common.Controls
{
    /*
    The following code has been copied verbatim from the http://dotnetzip.codeplex.com project
    It has been modified where needed to changed the class name from FolderBrowserDialogEx to
    SelectFolderDialog. http://dotnetzip.codeplex.com/SourceControl/changeset/view/29832#432677
    It has also been modified to address Code Analysis issues.
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
        private static readonly int MaxPath = 260;

        // Fields
        private UnsafeNativeMethods.BrowseFolderCallbackProc _callback;
        private string _descriptionText;
        private Environment.SpecialFolder _rootFolder;
        private string _selectedPath;
        private bool _selectedPathNeedsCheck;
        private bool _showNewFolderButton;
        private bool _showEditBox;
        private bool _showBothFilesAndFolders;
        private bool _newStyle = true;
        private bool _showFullPathInEditBox = true;
        private bool _dontIncludeNetworkFoldersBelowDomainLevel;
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
            this.Reset();
        } // SelectFolderDialog

        // Factory Methods
        public static SelectFolderDialog PrinterBrowser()
        {
            var x = new SelectFolderDialog();
            // avoid MBRO comppiler warning when passing _rootFolderLocation as a ref:
            x.BecomePrinterBrowser();
            return x;
        } // PrinterBrowser

        public static SelectFolderDialog ComputerBrowser()
        {
            var x = new SelectFolderDialog();
            // avoid MBRO comppiler warning when passing _rootFolderLocation as a ref:
            x.BecomeComputerBrowser();
            return x;
        } // ComputerBrowser


        // Helpers
        private void BecomePrinterBrowser()
        {
            _uiFlags += BrowseFlags.BifBrowseforprinter;
            Description = "Select a printer:";
            UnsafeNativeMethods.Shell32.SHGetSpecialFolderLocation(IntPtr.Zero, Csidl.Printers, ref this._rootFolderLocation);
            ShowNewFolderButton = false;
            ShowEditBox = false;
        } // BecomePrinterBrowser

        private void BecomeComputerBrowser()
        {
            _uiFlags += BrowseFlags.BifBrowseforcomputer;
            Description = "Select a computer:";
            UnsafeNativeMethods.Shell32.SHGetSpecialFolderLocation(IntPtr.Zero, Csidl.Network, ref this._rootFolderLocation);
            ShowNewFolderButton = false;
            ShowEditBox = false;
        } // BecomeComputerBrowser


        private class Csidl
        {
            public const int Printers = 4;
            public const int Network = 0x12;
        } // class CSIDL

        private static class BrowseFlags
        {
            public const int BifDefault = 0x0000;
            public const int BifBrowseforcomputer = 0x1000;
            public const int BifBrowseforprinter = 0x2000;
            public const int BifBrowseincludefiles = 0x4000;
            public const int BifBrowseincludeurls = 0x0080;
            public const int BifDontgobelowdomain = 0x0002;
            public const int BifEditbox = 0x0010;
            public const int BifNewdialogstyle = 0x0040;
            public const int BifNonewfolderbutton = 0x0200;
            public const int BifReturnfsancestors = 0x0008;
            public const int BifReturnonlyfsdirs = 0x0001;
            public const int BifShareable = 0x8000;
            public const int BifStatustext = 0x0004;
            public const int BifUahint = 0x0100;
            public const int BifValidate = 0x0020;
            public const int BifNotranslatetargets = 0x0400;
        } // class BrowseFlags

        private static class BrowseForFolderMessages
        {
            // messages FROM the folder browser
            public const int BffmInitialized = 1;
            public const int BffmSelchanged = 2;
            public const int BffmValidatefaileda = 3;
            public const int BffmValidatefailedw = 4;
            public const int BffmIunknown = 5;

            // messages TO the folder browser
            public const int BffmSetstatustext = 0x464;
            public const int BffmEnableok = 0x465;
            public const int BffmSetselectiona = 0x466;
            public const int BffmSetselectionw = 0x467;
        } // class BrowseForFolderMessages

        private int FolderBrowserCallback(IntPtr hwnd, uint msg, IntPtr lParam, IntPtr lpData)
        {
            switch (msg)
            {
                case BrowseForFolderMessages.BffmInitialized:
                    if (this._selectedPath.Length != 0)
                    {
                        UnsafeNativeMethods.User32.SendMessage(new HandleRef(null, hwnd), BrowseForFolderMessages.BffmSetselectionw, (IntPtr)1, this._selectedPath);
                        if (this._showEditBox && this._showFullPathInEditBox)
                        {
                            // get handle to the Edit box inside the Folder Browser Dialog
                            _hwndEdit = UnsafeNativeMethods.User32.FindWindowExW(new HandleRef(null, hwnd), IntPtr.Zero, "Edit", null);
                            UnsafeNativeMethods.User32.SetWindowTextW(_hwndEdit, this._selectedPath);
                        }
                    }
                    break;

                case BrowseForFolderMessages.BffmSelchanged:
                    var pidl = lParam;
                    if (pidl != IntPtr.Zero)
                    {
                        if (((_uiFlags & BrowseFlags.BifBrowseforprinter) == BrowseFlags.BifBrowseforprinter) ||
                            ((_uiFlags & BrowseFlags.BifBrowseforcomputer) == BrowseFlags.BifBrowseforcomputer))
                        {
                            // we're browsing for a printer or computer, enable the OK button unconditionally.
                            UnsafeNativeMethods.User32.SendMessage(new HandleRef(null, hwnd), BrowseForFolderMessages.BffmEnableok, (IntPtr)0, (IntPtr)1);
                        }
                        else
                        {
                            var pszPath = Marshal.AllocHGlobal(MaxPath * Marshal.SystemDefaultCharSize);
                            var haveValidPath = UnsafeNativeMethods.Shell32.SHGetPathFromIDList(pidl, pszPath);
                            var displayedPath = Marshal.PtrToStringAuto(pszPath);
                            Marshal.FreeHGlobal(pszPath);
                            // whether to enable the OK button or not. (if file is valid)
                            UnsafeNativeMethods.User32.SendMessage(new HandleRef(null, hwnd), BrowseForFolderMessages.BffmEnableok, (IntPtr)0, (IntPtr)(haveValidPath ? 1 : 0));

                            // Maybe set the Edit Box text to the Full Folder path
                            if (haveValidPath && !string.IsNullOrEmpty(displayedPath))
                            {
                                if (_showEditBox && _showFullPathInEditBox)
                                {
                                    if (_hwndEdit != IntPtr.Zero)
                                        UnsafeNativeMethods.User32.SetWindowTextW(_hwndEdit, displayedPath);
                                }

                                if ((_uiFlags & BrowseFlags.BifStatustext) == BrowseFlags.BifStatustext)
                                    UnsafeNativeMethods.User32.SendMessage(new HandleRef(null, hwnd), BrowseForFolderMessages.BffmSetstatustext, (IntPtr)0, displayedPath);
                            }
                        }
                    }
                    break;
            }
            return 0;
        } // FolderBrowserCallback

        private static UnsafeNativeMethods.IMalloc GetShMalloc()
        {
            var ppMalloc = new UnsafeNativeMethods.IMalloc[1];
            UnsafeNativeMethods.Shell32.SHGetMalloc(ppMalloc);
            return ppMalloc[0];
        } // GetSHMalloc

        public override void Reset()
        {
            this._rootFolder = (Environment.SpecialFolder)0;
            this._descriptionText = string.Empty;
            this._selectedPath = string.Empty;
            this._selectedPathNeedsCheck = false;
            this._showNewFolderButton = true;
            this._showEditBox = true;
            this._newStyle = true;
            this._dontIncludeNetworkFoldersBelowDomainLevel = false;
            this._hwndEdit = IntPtr.Zero;
            this._rootFolderLocation = IntPtr.Zero;
        } // Reset

        [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
        protected override bool RunDialog(IntPtr hWndOwner)
        {
            var result = false;
            if (_rootFolderLocation == IntPtr.Zero)
            {
                UnsafeNativeMethods.Shell32.SHGetSpecialFolderLocation(hWndOwner, (int)this._rootFolder, ref _rootFolderLocation);
                if (_rootFolderLocation == IntPtr.Zero)
                {
                    UnsafeNativeMethods.Shell32.SHGetSpecialFolderLocation(hWndOwner, 0, ref _rootFolderLocation);
                    if (_rootFolderLocation == IntPtr.Zero)
                    {
                        throw new InvalidOperationException("FolderBrowserDialogNoRootFolder");
                    }
                }
            }
            _hwndEdit = IntPtr.Zero;
            //_uiFlags = 0;
            if (_dontIncludeNetworkFoldersBelowDomainLevel)
                _uiFlags += BrowseFlags.BifDontgobelowdomain;
            if (this._newStyle)
                _uiFlags += BrowseFlags.BifNewdialogstyle;
            if (!this._showNewFolderButton)
                _uiFlags += BrowseFlags.BifNonewfolderbutton;
            if (this._showEditBox)
                _uiFlags += BrowseFlags.BifEditbox;
            if (this._showBothFilesAndFolders)
                _uiFlags += BrowseFlags.BifBrowseincludefiles;


            if (Control.CheckForIllegalCrossThreadCalls && (Application.OleRequired() != ApartmentState.STA))
            {
                throw new ThreadStateException("DebuggingException: ThreadMustBeSTA");
            }
            var pidl = IntPtr.Zero;
            var hglobal = IntPtr.Zero;
            var pszPath = IntPtr.Zero;
            try
            {
                var browseInfo = new UnsafeNativeMethods.Browseinfo();
                hglobal = Marshal.AllocHGlobal(MaxPath * Marshal.SystemDefaultCharSize);
                pszPath = Marshal.AllocHGlobal(MaxPath * Marshal.SystemDefaultCharSize);
                this._callback = new UnsafeNativeMethods.BrowseFolderCallbackProc(this.FolderBrowserCallback);
                browseInfo.pidlRoot = _rootFolderLocation;
                browseInfo.Owner = hWndOwner;
                browseInfo.pszDisplayName = hglobal;
                browseInfo.Title = this._descriptionText;
                browseInfo.Flags = _uiFlags;
                browseInfo.callback = this._callback;
                browseInfo.lParam = IntPtr.Zero;
                browseInfo.iImage = 0;
                pidl = UnsafeNativeMethods.Shell32.SHBrowseForFolderW(browseInfo);
                if (((_uiFlags & BrowseFlags.BifBrowseforprinter) == BrowseFlags.BifBrowseforprinter) ||
                ((_uiFlags & BrowseFlags.BifBrowseforcomputer) == BrowseFlags.BifBrowseforcomputer))
                {
                    this._selectedPath = Marshal.PtrToStringAuto(browseInfo.pszDisplayName);
                    result = true;
                }
                else
                {
                    if (pidl != IntPtr.Zero)
                    {
                        UnsafeNativeMethods.Shell32.SHGetPathFromIDList(pidl, pszPath);
                        this._selectedPathNeedsCheck = true;
                        this._selectedPath = Marshal.PtrToStringAuto(pszPath);
                        result = true;
                    }
                }
            }
            finally
            {
                var sHMalloc = GetShMalloc();
                sHMalloc.Free(_rootFolderLocation);
                _rootFolderLocation = IntPtr.Zero;
                if (pidl != IntPtr.Zero)
                {
                    sHMalloc.Free(pidl);
                }
                if (pszPath != IntPtr.Zero)
                {
                    Marshal.FreeHGlobal(pszPath);
                }
                if (hglobal != IntPtr.Zero)
                {
                    Marshal.FreeHGlobal(hglobal);
                }
                this._callback = null;
            }
            return result;
        }

        // Properties
        //[SRDescription("FolderBrowserDialogDescription"), SRCategory("CatFolderBrowsing"), Browsable(true), DefaultValue(""), Localizable(true)]

        /// <summary>
        /// This description appears near the top of the dialog box, providing direction to the user.
        /// </summary>
        public string Description
        {
            get => this._descriptionText;
            set => this._descriptionText = value ?? string.Empty;
        } // Description

        //[Localizable(false), SRCategory("CatFolderBrowsing"), SRDescription("FolderBrowserDialogRootFolder"), TypeConverter(typeof(SpecialFolderEnumConverter)), Browsable(true), DefaultValue(0)]
        public Environment.SpecialFolder RootFolder
        {
            get => this._rootFolder;
            set
            {
                if (!Enum.IsDefined(typeof(Environment.SpecialFolder), value))
                {
                    throw new InvalidEnumArgumentException("value", (int)value, typeof(Environment.SpecialFolder));
                }
                this._rootFolder = value;
            }
        } // RootFolder

        //[Browsable(true), SRDescription("FolderBrowserDialogSelectedPath"), SRCategory("CatFolderBrowsing"), DefaultValue(""), Editor("System.Windows.Forms.Design.SelectedPathEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor)), Localizable(true)]

        /// <summary>
        /// Set or get the selected path.  
        /// </summary>
        public string SelectedPath
        {
            get
            {
                if (((this._selectedPath != null) && (this._selectedPath.Length != 0)) && this._selectedPathNeedsCheck)
                {
                    new FileIOPermission(FileIOPermissionAccess.PathDiscovery, this._selectedPath).Demand();
                    this._selectedPathNeedsCheck = false;
                }
                return this._selectedPath;
            }
            set
            {
                this._selectedPath = value ?? string.Empty;
                this._selectedPathNeedsCheck = true;
            }
        } // SelectedPath

        //[SRDescription("FolderBrowserDialogShowNewFolderButton"), Localizable(false), Browsable(true), DefaultValue(true), SRCategory("CatFolderBrowsing")]

        /// <summary>
        /// Enable or disable the "New Folder" button in the browser dialog.
        /// </summary>
        public bool ShowNewFolderButton
        {
            get => this._showNewFolderButton;
            set => this._showNewFolderButton = value;
        } // ShowNewFolderButton

        /// <summary>
        /// Show an "edit box" in the folder browser.
        /// </summary>
        /// <remarks>
        /// The "edit box" normally shows the name of the selected folder.  
        /// The user may also type a pathname directly into the edit box.  
        /// </remarks>
        /// <seealso cref="ShowFullPathInEditBox"/>
        public bool ShowEditBox
        {
            get => this._showEditBox;
            set => this._showEditBox = value;
        } // ShowEditBox

        /// <summary>
        /// Set whether to use the New Folder Browser dialog style.
        /// </summary>
        /// <remarks>
        /// The new style is resizable and includes a "New Folder" button.
        /// </remarks>
        public bool NewStyle
        {
            get => this._newStyle;
            set => this._newStyle = value;
        } // NewStyle


        public bool DontIncludeNetworkFoldersBelowDomainLevel
        {
            get => _dontIncludeNetworkFoldersBelowDomainLevel;
            set => _dontIncludeNetworkFoldersBelowDomainLevel = value;
        } // DontIncludeNetworkFoldersBelowDomainLevel

        /// <summary>
        /// Show the full path in the edit box as the user selects it. 
        /// </summary>
        /// <remarks>
        /// This works only if ShowEditBox is also set to true. 
        /// </remarks>
        public bool ShowFullPathInEditBox
        {
            get => _showFullPathInEditBox;
            set => _showFullPathInEditBox = value;
        } // ShowFullPathInEditBox

        public bool ShowBothFilesAndFolders
        {
            get => _showBothFilesAndFolders;
            set => _showBothFilesAndFolders = value;
        } // ShowBothFilesAndFolders
    } // class SelectFolderDialog

    internal static class UnsafeNativeMethods
    {
        public delegate int BrowseFolderCallbackProc(IntPtr hwnd, uint msg, IntPtr lParam, IntPtr lpData);

        internal static class User32
        {
            [DllImport("user32.dll", CharSet = CharSet.Unicode)]
            public static extern IntPtr SendMessage(HandleRef hWnd, uint msg, IntPtr wParam, string lParam);

            [DllImport("user32.dll")]
            public static extern IntPtr SendMessage(HandleRef hWnd, uint msg, IntPtr wParam, IntPtr lParam);

            [DllImport("user32.dll", SetLastError = true, CharSet=CharSet.Unicode)]
            public static extern IntPtr FindWindowExW(HandleRef hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);

            [DllImport("user32.dll", CharSet=CharSet.Unicode, SetLastError = true)]
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
        public class Browseinfo
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
        } // class BROWSEINFO

        [SuppressUnmanagedCodeSecurity]
        internal static class Shell32
        {
            // Methods
            [DllImport("shell32.dll", CharSet = CharSet.Unicode)]
            public static extern IntPtr SHBrowseForFolderW([In] UnsafeNativeMethods.Browseinfo lpbi);
            [DllImport("shell32.dll")]
            public static extern int SHGetMalloc([Out, MarshalAs(UnmanagedType.LPArray)] UnsafeNativeMethods.IMalloc[] ppMalloc);
            [DllImport("shell32.dll", CharSet = CharSet.Auto)]
            public static extern bool SHGetPathFromIDList(IntPtr pidl, IntPtr pszPath);
            [DllImport("shell32.dll")]
            public static extern int SHGetSpecialFolderLocation(IntPtr hwnd, int csidl, ref IntPtr ppidl);
        } // internal static class Shell32
    } // class UnsafeNativeMethods
} // namespace

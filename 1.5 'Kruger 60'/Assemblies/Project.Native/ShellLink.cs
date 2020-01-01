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
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text;

namespace IpTviewr.Native
{
    public class ShellLink
    {
        public class UnsafeNativeMethods
        {
            public static readonly Guid ClsidFolderShortcut = new Guid(0x0AFACED1, 0xE828, 0x11D1, 0x91, 0x87, 0xB5, 0x32, 0xF1, 0xE9, 0x57, 0x5D);
            public static readonly Guid ClsidShellLink = new Guid("00021401-0000-0000-C000-000000000046");

            [ComImport, Guid("0000010c-0000-0000-c000-000000000046"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
            public interface IPersist
            {
                Guid GetClassID();
            } // interface IPersist

            [ComImport, Guid("0000010b-0000-0000-C000-000000000046"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
            public interface IPersistFile
            {
                Guid GetClassID();

                [PreserveSig]
                int IsDirty();

                void Load([In, MarshalAs(UnmanagedType.LPWStr)] string pszFileName, uint dwMode);

                void Save([In, MarshalAs(UnmanagedType.LPWStr)] string pszFileName, [In, MarshalAs(UnmanagedType.Bool)] bool fRemember);

                void SaveCompleted([In, MarshalAs(UnmanagedType.LPWStr)] string pszFileName);

                void GetCurFile([In, MarshalAs(UnmanagedType.LPWStr)] string ppszFileName);
            } // interface IPersistFile

            /// <summary>The IShellLink interface allows Shell links to be created, modified, and resolved</summary>
            [ComImport(), Guid("000214F9-0000-0000-C000-000000000046"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown), CoClass(typeof(ShellLinkClass))]
            public interface IShellLinkW
            {
                /// <summary>Retrieves the path and file name of a Shell link object</summary>
                void GetPath([Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszFile, int cchMaxPath, out Win32FindDataw pfd, SlgpFlags fFlags);
                /// <summary>Retrieves the list of item identifiers for a Shell link object</summary>
                void GetIDList(out IntPtr ppidl);
                /// <summary>Sets the pointer to an item identifier list (PIDL) for a Shell link object.</summary>
                void SetIDList(IntPtr pidl);
                /// <summary>Retrieves the description string for a Shell link object</summary>
                void GetDescription([Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszName, int cchMaxName);
                /// <summary>Sets the description for a Shell link object. The description can be any application-defined string</summary>
                void SetDescription([MarshalAs(UnmanagedType.LPWStr)] string pszName);
                /// <summary>Retrieves the name of the working directory for a Shell link object</summary>
                void GetWorkingDirectory([Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszDir, int cchMaxPath);
                /// <summary>Sets the name of the working directory for a Shell link object</summary>
                void SetWorkingDirectory([MarshalAs(UnmanagedType.LPWStr)] string pszDir);
                /// <summary>Retrieves the command-line arguments associated with a Shell link object</summary>
                void GetArguments([Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszArgs, int cchMaxPath);
                /// <summary>Sets the command-line arguments for a Shell link object</summary>
                void SetArguments([MarshalAs(UnmanagedType.LPWStr)] string pszArgs);
                /// <summary>Retrieves the hot key for a Shell link object</summary>
                void GetHotkey(out short pwHotkey);
                /// <summary>Sets a hot key for a Shell link object</summary>
                void SetHotkey(short wHotkey);
                /// <summary>Retrieves the show command for a Shell link object</summary>
                void GetShowCmd(out int piShowCmd);
                /// <summary>Sets the show command for a Shell link object. The show command sets the initial show state of the window.</summary>
                void SetShowCmd(int iShowCmd);
                /// <summary>Retrieves the location (path and index) of the icon for a Shell link object</summary>
                void GetIconLocation([Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszIconPath, int cchIconPath, out int piIcon);
                /// <summary>Sets the location (path and index) of the icon for a Shell link object</summary>
                void SetIconLocation([MarshalAs(UnmanagedType.LPWStr)] string pszIconPath, int iIcon);
                /// <summary>Sets the relative path to the Shell link object</summary>
                void SetRelativePath([MarshalAs(UnmanagedType.LPWStr)] string pszPathRel, int dwReserved);
                /// <summary>Attempts to find the target of a Shell link, even if it has been moved or renamed</summary>
                void Resolve(IntPtr hwnd, SlrFlags fFlags);
                /// <summary>Sets the path and file name of a Shell link object</summary>
                void SetPath([MarshalAs(UnmanagedType.LPWStr)] string pszFile);
            } // interface IShellLink

            [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
            public struct Win32FindDataw
            {
                public uint dwFileAttributes;
                public long ftCreationTime;
                public long ftLastAccessTime;
                public long ftLastWriteTime;
                public uint nFileSizeHigh;
                public uint nFileSizeLow;
                public uint dwReserved0;
                public uint dwReserved1;
                [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
                public string cFileName;
                [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 14)]
                public string cAlternateFileName;
            } // enum WIN32_FIND_DATAW

            /// <summary>IShellLink.GetPath fFlags: Flags that specify the type of path information to retrieve</summary>
            [Flags()]
            public enum SlgpFlags
            {
                /// <summary>Retrieves the standard short (8.3 format) file name</summary>
                SlgpShortpath = 0x1,
                /// <summary>Retrieves the Universal Naming Convention (UNC) path name of the file</summary>
                SlgpUncpriority = 0x2,
                /// <summary>Retrieves the raw path name. A raw path is something that might not exist and may include environment variables that need to be expanded</summary>
                SlgpRawpath = 0x4
            } // enum SLGP_FLAGS

            /// <summary>IShellLink.Resolve fFlags</summary>
            [Flags()]
            public enum SlrFlags
            {
                /// <summary>
                /// Do not display a dialog box if the link cannot be resolved. When SLR_NO_UI is set,
                /// the high-order word of fFlags can be set to a time-out value that specifies the 
                /// maximum amount of time to be spent resolving the link. The function returns if the
                /// link cannot be resolved within the time-out duration. If the high-order word is set
                /// to zero, the time-out duration will be set to the default value of 3,000 milliseconds
                /// (3 seconds). To specify a value, set the high word of fFlags to the desired time-out
                /// duration, in milliseconds.
                /// </summary>
                SlrNoUi = 0x1,
                /// <summary>Obsolete and no longer used</summary>
                SlrAnyMatch = 0x2,
                /// <summary>If the link object has changed, update its path and list of identifiers. 
                /// If SLR_UPDATE is set, you do not need to call IPersistFile::IsDirty to determine 
                /// whether or not the link object has changed.</summary>
                SlrUpdate = 0x4,
                /// <summary>Do not update the link information</summary>
                SlrNoupdate = 0x8,
                /// <summary>Do not execute the search heuristics</summary>
                SlrNosearch = 0x10,
                /// <summary>Do not use distributed link tracking</summary>
                SlrNotrack = 0x20,
                /// <summary>Disable distributed link tracking. By default, distributed link tracking tracks
                /// removable media across multiple devices based on the volume name. It also uses the
                /// Universal Naming Convention (UNC) path to track remote file systems whose drive letter
                /// has changed. Setting SLR_NOLINKINFO disables both types of tracking.</summary>
                SlrNolinkinfo = 0x40,
                /// <summary>Call the Microsoft Windows Installer</summary>
                SlrInvokeMsi = 0x80
            } // enum SLR_FLAGS
        } // class

        [ComImport, Guid("00021401-0000-0000-C000-000000000046")]
        public class ShellLinkClass
        {
        } // class ShellLink

        [ComImport, Guid("0AFACED1-E828-11D1-9187-B532F1E9575D")]
        public class FolderShortcutClass
        {
        } // class FolderShortcut

        public string TargetPath
        {
            get;
            set;
        } // TargetPath

        public string WorkingDirectory
        {
            get;
            set;
        } // WorkingDirectory

        public string Arguments
        {
            get;
            set;
        } // Arguments

        public string Description
        {
            get;
            set;
        } // Description

        public string IconLocation
        {
            get;
            set;
        } // IconLocation

        public int IconIndex
        {
            get;
            set;
        } // IconIndex

        public bool IsFolderShortcut
        {
            get;
            set;
        } // IsFolderShortcut

        [PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
        public string CreateShortcut(string shortcutPath)
        {
            UnsafeNativeMethods.IShellLinkW shortcut;
            UnsafeNativeMethods.IPersistFile shortcutFile;

            if (string.IsNullOrEmpty(shortcutPath)) throw new ArgumentNullException();
            if (string.IsNullOrEmpty(TargetPath)) throw new ArgumentNullException();

            shortcut = null;
            shortcutFile = null;
            try
            {
                if (!IsFolderShortcut)
                {
                    shortcut = new ShellLinkClass() as UnsafeNativeMethods.IShellLinkW;
                }
                else
                {
                    shortcut = new FolderShortcutClass() as UnsafeNativeMethods.IShellLinkW;
                } // if-else

                shortcut.SetPath(TargetPath);
                if (!string.IsNullOrEmpty(Arguments)) shortcut.SetArguments(Arguments);
                if (!string.IsNullOrEmpty(Description)) shortcut.SetDescription(Description);
                if (!string.IsNullOrEmpty(WorkingDirectory)) shortcut.SetWorkingDirectory(WorkingDirectory);
                if (!string.IsNullOrEmpty(IconLocation)) shortcut.SetIconLocation(IconLocation, IconIndex);

                shortcutFile = shortcut as UnsafeNativeMethods.IPersistFile;
                if (!shortcutPath.EndsWith(".lnk")) shortcutPath += ".lnk";
                shortcutPath = System.IO.Path.GetFullPath(shortcutPath);
                shortcutFile.Save(shortcutPath, true);

                return shortcutPath;
            }
            finally
            {
                if (shortcutFile != null) Marshal.ReleaseComObject(shortcutFile);
                if (shortcut != null) Marshal.ReleaseComObject(shortcut);
            } // try-finally
        } // Create
    } // ShellLink
} // namespace

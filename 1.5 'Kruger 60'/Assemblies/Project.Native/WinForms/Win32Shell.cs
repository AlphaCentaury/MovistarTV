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
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace IpTviewr.Native.WinForms
{
    public class Win32Shell
    {
        #region Shell item

        /// <summary>
        /// Win32 PROPERTYKEY struct
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 4)]
        public struct PropertyKey
        {
            public Guid fmtid;
            public uint pid;
        } // struct PropertyKey

        /// <summary>
        /// Win32 SIGDN enum
        /// </summary>
        public enum GetDisplayName : uint
        {
            NORMALDISPLAY = 0,
            PARENTRELATIVEPARSING = 0x80018001,
            DESKTOPABSOLUTEPARSING = 0x80028000,
            PARENTRELATIVEEDITING = 0x80031001,
            DESKTOPABSOLUTEEDITING = 0x8004C000,
            FILESYSPATH = 0x80058000,
            URL = 0x80068000,
            PARENTRELATIVEFORADDRESSBAR = 0x8007C001,
            PARENTRELATIVE = 0x80080001
        } // enum ShellGetDisplayName

        // <remarks>SIGDN</remarks>
        /// <summary>
        /// Win32 SIATTRIBFLAGS enum
        /// </summary>
        public enum ItemAttributeFlags
        {
            AND = 1,
            OR = 2,
            APPCOMPAT = 3,
        } // enum ItemAttributeFlags

        [Guid("43826D1E-E718-42EE-BC55-A1E261C37BFE")]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        [ComImport]
        public interface IShellItem
        {
            void BindToHandler([MarshalAs(UnmanagedType.Interface), In] IntPtr pbc, [In] ref Guid bhid, [In] ref Guid riid, out IntPtr ppv);

            void GetParent([MarshalAs(UnmanagedType.Interface)] out IShellItem ppsi);

            void GetDisplayName([In] GetDisplayName sigdnName, [MarshalAs(UnmanagedType.LPWStr)] out string ppszName);

            void GetAttributes([In] uint sfgaoMask, out uint psfgaoAttribs);

            void Compare([MarshalAs(UnmanagedType.Interface), In] IShellItem psi, [In] uint hint, out int piOrder);
        } // interface IShellItem

        [Guid("B63EA76D-1F85-456F-A19C-48159EFA858B")]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        [ComImport]
        public interface IShellItemArray
        {
            void BindToHandler([MarshalAs(UnmanagedType.Interface), In] IntPtr pbc, [In] ref Guid rbhid, [In] ref Guid riid, out IntPtr ppvOut);

            void GetPropertyStore([In] int Flags, [In] ref Guid riid, out IntPtr ppv);

            void GetPropertyDescriptionList([In] ref PropertyKey keyType, [In] ref Guid riid, out IntPtr ppv);

            void GetAttributes([In] ItemAttributeFlags dwAttribFlags, [In] uint sfgaoMask, out uint psfgaoAttribs);

            void GetCount(out uint pdwNumItems);

            void GetItemAt([In] uint dwIndex, [MarshalAs(UnmanagedType.Interface)] out IShellItem ppsi);

            void EnumItems([MarshalAs(UnmanagedType.Interface)] out IntPtr ppenumShellItems);
        } // interface IShellItemArray

        [DllImport("shell32.dll")]
        public static extern int SHCreateShellItem(IntPtr pidlParent, IntPtr psfParent, IntPtr pidl, out IShellItem ppsi);

        [DllImport("shell32.dll")]
        public static extern int SHILCreateFromPath([MarshalAs(UnmanagedType.LPWStr)] string pszPath, out IntPtr ppIdl, ref uint rgflnOut);

        #endregion

        #region OpenFileDialog

        /// <summary>
        /// Win32 COMDLG_FILTERSPEC struct
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
        public struct CommonDialogFilter
        {
            [MarshalAs(UnmanagedType.LPWStr)]
            internal string pszName;
            [MarshalAs(UnmanagedType.LPWStr)]
            internal string pszSpec;
        } // struct CommonDialogFilter

        /// <summary>
        /// Win32 FOS (FILEOPENDIALOGOPTIONS) enum
        /// </summary>
        [Flags]
        public enum FileOpenDialogOptions : uint
        {
            OVERWRITEPROMPT = 2,
            STRICTFILETYPES = 4,
            NOCHANGEDIR = 8,
            PICKFOLDERS = 0x00000020,
            FORCEFILESYSTEM = 0x00000040,
            ALLNONSTORAGEITEMS = 0x00000080,
            NOVALIDATE = 0x00000100,
            ALLOWMULTISELECT = 0x00000200,
            PATHMUSTEXIST = 0x00000800,
            FILEMUSTEXIST = 0x00001000,
            CREATEPROMPT = 0x00002000,
            SHAREAWARE = 0x00004000,
            NOREADONLYRETURN = 0x00008000,
            NOTESTFILECREATE = 0x00010000,
            HIDEMRUPLACES = 0x00020000,
            HIDEPINNEDPLACES = 0x00040000,
            NODEREFERENCELINKS = 0x00100000,
            DONTADDTORECENT = 0x02000000,
            FORCESHOWHIDDEN = 0x10000000,
            DEFAULTNOMINIMODE = 0x20000000,
        } // enum FileOpenDialogOptions

        [Guid("d57c7288-d4ad-4768-be02-9d969532d960")]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        [ComImport]
        public interface IFileOpenDialog
        {
            // from IModalDialog

            [MethodImpl(MethodImplOptions.PreserveSig)]
            int Show([In] IntPtr parent);

            // from IFileDialog

            void SetFileTypes([In] uint cFileTypes, [In] ref CommonDialogFilter rgFilterSpec);

            void SetFileTypeIndex([In] uint iFileType);

            void GetFileTypeIndex(out uint piFileType);

            void Advise([MarshalAs(UnmanagedType.Interface), In] IntPtr pfde, out uint pdwCookie);

            void Unadvise([In] uint dwCookie);

            void SetOptions([In] FileOpenDialogOptions fos);

            void GetOptions(out FileOpenDialogOptions pfos);

            void SetDefaultFolder([MarshalAs(UnmanagedType.Interface), In] IShellItem psi);

            void SetFolder([MarshalAs(UnmanagedType.Interface), In] IShellItem psi);

            void GetFolder([MarshalAs(UnmanagedType.Interface)] out IShellItem ppsi);

            void GetCurrentSelection([MarshalAs(UnmanagedType.Interface)] out IShellItem ppsi);

            void SetFileName([MarshalAs(UnmanagedType.LPWStr), In] string pszName);

            void GetFileName([MarshalAs(UnmanagedType.LPWStr)] out string pszName);

            void SetTitle([MarshalAs(UnmanagedType.LPWStr), In] string pszTitle);

            void SetOkButtonLabel([MarshalAs(UnmanagedType.LPWStr), In] string pszText);

            void SetFileNameLabel([MarshalAs(UnmanagedType.LPWStr), In] string pszLabel);

            void GetResult([MarshalAs(UnmanagedType.Interface)] out IShellItem ppsi);

            void AddPlace([MarshalAs(UnmanagedType.Interface), In] IShellItem psi, FileDialogCustomPlace fdcp);

            void SetDefaultExtension([MarshalAs(UnmanagedType.LPWStr), In] string pszDefaultExtension);

            void Close([MarshalAs(UnmanagedType.Error)] int hr);

            void SetClientGuid([In] ref Guid guid);

            void ClearClientData();

            void SetFilter([MarshalAs(UnmanagedType.Interface)] IntPtr pFilter);

            // from IFileOpenDialog

            void GetResults([MarshalAs(UnmanagedType.Interface)] out IShellItemArray ppenum);

            void GetSelectedItems([MarshalAs(UnmanagedType.Interface)] out IShellItemArray ppsai);
        } // 

        [ClassInterface(ClassInterfaceType.None)]
        [TypeLibType(TypeLibTypeFlags.FCanCreate)]
        [Guid("DC1C5A9C-E88A-4dde-A5A1-60F82A20AEF7")]
        [ComImport]
        public class FileOpenDialog
        {
        } // class FileOpenDialog

        #endregion
    } // class Win32Shell
} // namespace

// Copyright (C) 2014-2015, Codeplex user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text;

namespace Project.IpTv.Tools.FirstTimeConfig
{
    public class KnownFolders
    {
        internal class UnsafeNativeMethods
        {
            [DllImport("shell32.dll")]
            public static extern int SHGetKnownFolderPath([MarshalAs(UnmanagedType.LPStruct)] Guid rfid,
                 uint dwFlags, IntPtr hToken, out IntPtr pszPath);
        } // class UnsafeNativeMethods

        public class CurrentUser
        {
            public const string Music = "4BD8D571-6D19-48D3-BE97-422220080E43";
            public const string Videos = "18989B1D-99B5-455B-841C-AB7C74E4DDFC";
            // work in progress
        } // class CurrentUser

        public class Common
        {
            public const string SampleMusic = "B250C668-F57D-4EE1-A63C-290EE7D1AA1F";
            public const string SampleVideos = "859EAD94-2E85-48AD-A71A-0969CB56A6CD";
            // work in progress
        } // class Common

        public class System
        {
            public const string ProgramFiles = "905e63b6-c1bf-494e-b29c-65b732d3d21a";
            public const string ProgramFiles_x86 = "7C5A40EF-A0FB-4BFC-874A-C0F2E0B9FA8E";
            /// <remarks>This value is not supported on 32-bit operating systems. It also is not supported for 32-bit applications running on 64-bit operating systems. Attempting to use ProgramFiles_x64 in either situation results in an error.</remarks>
            public const string ProgramFiles_x64 = "6D809377-6AF0-444b-8957-A3773F02200E";
            // work in progress
        } // class System

        public class Virtual
        {
            // work in progress
        } // class Virtual

        [Flags]
        public enum Flags: uint
        {
            /// <summary>No special retrieval options.</summary>
            None = 0,

            /// <summary>Build a simple IDList (PIDL) This value can be used when you want to retrieve the file system path but do not specify this value if you are retrieving the localized display name of the folder because it might not resolve correctly.</summary>
            SimpleIdList = 0x00000100,

            /// <summary>Gets the folder's default path independent of the current location of its parent. 'DefaultPath' flag must also be set.</summary>
            NotParentRelative = 0x00000200,

            /// <summary>Gets the default path for a known folder. If this flag is not set, the function retrieves the current—and possibly redirected—path of the folder. The execution of this flag includes a verification of the folder's existence unless 'DoNotVerify' flag is set.</summary>
            DefaultPath = 0x00000400,

            /// <summary>Initializes the folder using its Desktop.ini settings. If the folder cannot be initialized, the function returns a failure code and no path is returned. This flag should always be combined with 'Create' flag.
            /// If the folder is located on a network, the function might take a longer time to execute.</summary>
            Init = 0x00000800,

            /// <summary>Gets the true system path for the folder, free of any aliased placeholders such as %USERPROFILE%, returned by SHGetKnownFolderIDList and IKnownFolder::GetIDList. This flag has no effect on paths returned by SHGetKnownFolderPath and IKnownFolder::GetPath. By default, known folder retrieval functions and methods return the aliased path if an alias exists.</summary>
            NoAlias = 0x00001000,

            /// <summary>Stores the full path in the registry without using environment strings. If this flag is not set, portions of the path may be represented by environment strings such as %USERPROFILE%. This flag can only be used with SHSetKnownFolderPath and IKnownFolder::SetPath.</summary>
            DoNotUnexpand = 0x00002000,

            /// <summary>Do not verify the folder's existence before attempting to retrieve the path or IDList.</summary>
            DoNotVerify = 0x00004000,

            /// <summary>Forces the creation of the specified folder if that folder does not already exist. The security provisions predefined for that folder are applied. If the folder does not exist and cannot be created, the function returns a failure code and no path is returned.
            /// This value can be used only with the following functions and methods: SHGetKnownFolderPath, SHGetKnownFolderIDList, IKnownFolder::GetIDList, IKnownFolder::GetPath and IKnownFolder::GetShellItem</summary>
            Create = 0x00008000,

            /// <summary> When running inside an app container, or when providing an app container token, this flag prevents redirection to app container folders. Instead, it retrieves the path that would be returned where it not running inside an app container. (Introduced in Windows 7.)</summary>
            NoAppContainerRedirection = 0x00010000,

            /// <summary>Return only aliased PIDLs. Do not use the file system path.(Introduced in Windows 7.)</summary>
            AliasOnly = 0x80000000
        } // Flags

        [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
        public static string GetKnownFolder(string knownFolderGuid, Flags flags)
        {
            IntPtr pszPath;

            var guid = new Guid(knownFolderGuid);
            var hResult = UnsafeNativeMethods.SHGetKnownFolderPath(guid, (uint)flags, IntPtr.Zero, out pszPath);
            if (hResult != 0)
            {
                throw Marshal.GetExceptionForHR(hResult);
            } // if

            var path = Marshal.PtrToStringUni(pszPath);
            Marshal.FreeCoTaskMem(pszPath);
            pszPath = IntPtr.Zero;

            return path;
        } // GetKnownFolder
    } // class KnownFolders
} // namespace

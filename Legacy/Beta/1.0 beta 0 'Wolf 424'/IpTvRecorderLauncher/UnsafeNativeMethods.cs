// Copyright (C) 2014, Codeplex user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Project.DvbIpTv.RecorderLauncher
{
    public partial class UnsafeNativeMethods
    {
        /// <remarks>Win32: JOBOBJECTINFOCLASS</remarks>
        public enum JobObjectInfoClass
        {
            /// JobObjectBasicAccountingInformation -> 1
            BasicAccountingInformation = 1,
            BasicLimitInformation,
            BasicProcessIdList,
            BasicUIRestrictions,
            SecurityLimitInformation,
            EndOfJobTimeInformation,
            AssociateCompletionPortInformation,
            BasicAndIoAccountingInformation,
            ExtendedLimitInformation,
            JobSetInformation,
            MaxJobObjectInfoClass,
        } // enum JobObjectInfoClass

        [StructLayout(LayoutKind.Sequential)]
        /// <remarks>Win32: JOBOBJECT_BASIC_LIMIT_INFORMATION</remarks>
        public struct JobObjectBasicLimitInformation
        {
            public Int64 PerProcessUserTimeLimit;
            public Int64 PerJobUserTimeLimit;
            public UInt32 LimitFlags;
            public UIntPtr MinimumWorkingSetSize;
            public UIntPtr MaximumWorkingSetSize;
            public UInt32 ActiveProcessLimit;
            public UIntPtr Affinity;
            public UInt32 PriorityClass;
            public UInt32 SchedulingClass;
        } // struct JobObjectBasicLimitInformation

        [StructLayout(LayoutKind.Sequential)]
        /// <remarks>Win32: IO_COUNTERS</remarks>
        public struct IoCounters
        {
            public UInt64 ReadOperationCount;
            public UInt64 WriteOperationCount;
            public UInt64 OtherOperationCount;
            public UInt64 ReadTransferCount;
            public UInt64 WriteTransferCount;
            public UInt64 OtherTransferCount;
        } // struct IoCounters

        [StructLayout(LayoutKind.Sequential)]
        /// <remarks>Win32: JOBOBJECT_EXTENDED_LIMIT_INFORMATION</remarks>
        public struct JobObjectExtendedLimitInformation
        {
            public JobObjectBasicLimitInformation BasicLimitInformation;
            public IoCounters IoInfo;
            public UIntPtr ProcessMemoryLimit;
            public UIntPtr JobMemoryLimit;
            public UIntPtr PeakProcessMemoryUsed;
            public UIntPtr PeakJobMemoryUsed;
        } // JobObjectExtendedLimitInformation

        /// <remarks>Win32: JOB_OBJECT_LIMIT_KILL_ON_JOB_CLOSE</remarks>
        public const int JobjObjectLimitKillOnJobClose = 0x00002000;

        // Return Type: HANDLE->void*
        // lpJobAttributes: LPSECURITY_ATTRIBUTES->_SECURITY_ATTRIBUTES*
        // lpName: LPCWSTR->WCHAR*
        [DllImport("kernel32.dll", EntryPoint = "CreateJobObjectW", SetLastError=true)]
        public static extern System.IntPtr CreateJobObject([In] IntPtr lpJobAttributes, [In] [MarshalAs(UnmanagedType.LPWStr)] string lpName);

        // Return Type: BOOL->int
        // hJob: HANDLE->void*
        // hProcess: HANDLE->void*
        [DllImport("kernel32.dll", EntryPoint = "AssignProcessToJobObject", SetLastError=true)]
        [return: MarshalAsAttribute(UnmanagedType.Bool)]
        public static extern bool AssignProcessToJobObject([In] IntPtr hJob, [In] IntPtr hProcess);

        //  Return Type: BOOL->int
        // hJob: HANDLE->void*
        // JobObjectInformationClass: JOBOBJECTINFOCLASS->_JOBOBJECTINFOCLASS
        // lpJobObjectInformation: LPVOID->void*
        // cbJobObjectInformationLength: DWORD->unsigned int
        [DllImport("kernel32.dll", EntryPoint = "SetInformationJobObject", SetLastError=true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetInformationJobObject([In] IntPtr hJob, JobObjectInfoClass JobObjectInformationClass, [In] IntPtr lpJobObjectInformation, uint cbJobObjectInformationLength);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool SetConsoleIcon(IntPtr hIcon);
    } // class UnsafeNativeMethods
} // namespace

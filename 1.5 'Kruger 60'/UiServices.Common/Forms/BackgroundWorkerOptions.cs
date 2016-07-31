// Copyright (C) 2014-2016, Codeplex user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project.IpTv.UiServices.Common.Forms
{
    public class BackgroundWorkerOptions
    {
        public string Caption
        {
            get;
            set;
        } // Caption

        public string TaskDescription
        {
            get;
            set;
        } // TaskDescription

        public bool AllowProgressBar
        {
            get;
            set;
        } // public bool AllowProgressBar

        public bool AllowCancelButton
        {
            get;
            set;
        } // AllowCancelButton

        public bool AllowAutoClose
        {
            get;
            set;
        } // AllowAutoClose

        public string TaskCancellingText
        {
            get;
            set;
        } // TaskCancellingText

        public string TaskCancelledText
        {
            get;
            set;
        } // TaskCancelledText

        public string TaskCompletedText
        {
            get;
            set;
        } // TaskCompletedText

        public Action<BackgroundWorkerOptions, IBackgroundWorkerDialog> BeforeTask
        {
            get;
            set;
        } // BeforeTask

        public Action<BackgroundWorkerOptions, IBackgroundWorkerDialog> BackgroundBeforeTask
        {
            get;
            set;
        } // BackgroundBeforeTask

        public Action<BackgroundWorkerOptions, IBackgroundWorkerDialog> BackgroundTask
        {
            get;
            set;
        } // BackgroundTask

        public Action<BackgroundWorkerOptions, IBackgroundWorkerDialog> BackgroundAfterTask
        {
            get;
            set;
        } // BackgroundAfterTask

        public Action<BackgroundWorkerOptions, IBackgroundWorkerDialog> AfterTask
        {
            get;
            set;
        } // AfterTask

        public object InputData
        {
            get;
            set;
        } // InputData

        public object OutputData
        {
            get;
            set;
        } // OputputData

        public Exception OutputException
        {
            get;
            internal set;
        } // OutputException
    } // class BackgroundWorkerOptions
} // namespace

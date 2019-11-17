// Copyright (C) 2014-2019, GitHub/Codeplex user AlphaCentaury
// 
// All rights reserved, except those granted by the governing license of this software.
// See 'license.txt' file in the project root for complete license information.
// 
// http://www.alphacentaury.org/movistartv https://github.com/AlphaCentaury

namespace IpTviewr.UiServices.Record
{
    partial class RecordChannelDialog
    {
        #region "Duration" tab events / setup & get data

        private void InitDurationData()
        {
            recordingTime.SetDuration(_currentStartDateTime, Task.Schedule.Kind, Task.Duration);
        } // InitDurationData()

        private void GetDurationData()
        {
            Task.Duration = recordingTime.GetDuration();
        } // GetDurationData

        #endregion
    } // partial class RecordChannelDialog
} // namespace

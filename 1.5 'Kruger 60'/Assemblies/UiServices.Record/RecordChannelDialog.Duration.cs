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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IpTviewr.UiServices.Record
{
    partial class RecordChannelDialog
    {
        #region "Duration" tab events / setup & get data

        private void InitDurationData()
        {
            recordingTime.SetDuration(CurrentStartDateTime, Task.Schedule.Kind, Task.Duration);
        } // InitDurationData()

        private void GetDurationData()
        {
            Task.Duration = recordingTime.GetDuration();
        } // GetDurationData

        #endregion
    } // partial class RecordChannelDialog
} // namespace

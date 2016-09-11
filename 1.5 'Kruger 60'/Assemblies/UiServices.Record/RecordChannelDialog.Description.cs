using IpTviewr.Services.Record.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IpTviewr.UiServices.Record
{
    partial class RecordChannelDialog
    {
        #region "Description" tab events / setup & get data

        private void InitDescriptionData()
        {
            textTaskDescription.Text = Task.Description.Description;
            checkAppendRecordingDetails.Checked = Task.Description.AddDetails;
        } // InitDescriptionData

        private void GetDescriptionData()
        {
            if (Task.Schedule.Kind == RecordScheduleKind.RightNow)
            {
                CurrentStartDateTime = DateTime.Now;
                UpdateTaskName();
            } // if
            Task.Description.Description = textTaskDescription.Text.Trim();
            Task.Description.AddDetails = checkAppendRecordingDetails.Checked;
        } // GetDescriptionData

        #endregion
    } // partial class RecordChannelDialog
} // namespace

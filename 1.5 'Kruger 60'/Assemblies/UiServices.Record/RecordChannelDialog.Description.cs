// Copyright (C) 2014-2019, GitHub/Codeplex user AlphaCentaury
// 
// All rights reserved, except those granted by the governing license of this software.
// See 'license.txt' file in the project root for complete license information.
// 
// http://www.alphacentaury.org/movistartv https://github.com/AlphaCentaury

using IpTviewr.Services.Record.Serialization;
using System;

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
                _currentStartDateTime = DateTime.Now;
                UpdateTaskName();
            } // if
            Task.Description.Description = textTaskDescription.Text.Trim();
            Task.Description.AddDetails = checkAppendRecordingDetails.Checked;
        } // GetDescriptionData

        #endregion
    } // partial class RecordChannelDialog
} // namespace

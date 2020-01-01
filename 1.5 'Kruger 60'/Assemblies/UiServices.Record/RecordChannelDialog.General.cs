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

using IpTviewr.Common;
using IpTviewr.UiServices.Configuration;
using IpTviewr.UiServices.Configuration.Logos;

namespace IpTviewr.UiServices.Record
{
    partial class RecordChannelDialog
    {
        #region 'General' form setup & get data

        private void InitGeneralData()
        {
            // service logo
            var serviceLogo = AppConfig.Current.ServiceLogoMappings.FromServiceKey(Task.Channel.ServiceKey);
            pictureChannelLogo.Image = serviceLogo.GetImage(LogoSize.Size64);

            // service name
            labelChannelName.Text = $"{Task.Channel.LogicalNumber} {Task.Channel.Name}";

            // program name
            if (Task.Program != null)
            {
                labelProgramDescription.Text = Task.Program.Title;
                labelProgramSchedule.Text = $"{FormatString.DateTimeFromToMinutes(Task.Program.LocalStartTime, Task.Program.LocalEndTime, LocalReferenceTime)} ({FormatString.TimeSpanTotalMinutes(Task.Program.Duration, FormatString.Format.Extended)})";
            }
            else
            {
                labelChannelName.Top = pictureChannelLogo.Top;
                labelChannelName.Height = pictureChannelLogo.Height;

                labelProgramDescription.Visible = false;
                labelProgramSchedule.Visible = false;
            } // if-else
        } // InitGeneralData

        private void GetGeneralData()
        {
            // nothing to get
        } // GetGeneralData

        #endregion
    } // partial class RecordChannelDialog
} // namespace

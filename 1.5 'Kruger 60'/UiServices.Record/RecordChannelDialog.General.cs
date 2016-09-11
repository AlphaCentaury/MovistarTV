using IpTviewr.Common;
using IpTviewr.UiServices.Configuration;
using IpTviewr.UiServices.Configuration.Logos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IpTviewr.UiServices.Record
{
    partial class RecordChannelDialog
    {
        #region 'General' form setup & get data

        private void InitGeneralData()
        {
            // service logo
            var serviceLogo = AppUiConfiguration.Current.ServiceLogoMappings.FromServiceKey(Task.Channel.ServiceKey);
            pictureChannelLogo.Image = serviceLogo.GetImage(LogoSize.Size64);

            // service name
            labelChannelName.Text = string.Format("{0} {1}", Task.Channel.LogicalNumber, Task.Channel.Name);

            // program name
            if (Task.Program != null)
            {
                labelProgramDescription.Text = Task.Program.Title;
                labelProgramSchedule.Text = string.Format("{0} ({1})", FormatString.DateTimeFromToMinutes(Task.Program.LocalStartTime, Task.Program.LocalEndTime, LocalReferenceTime),
                    FormatString.TimeSpanTotalMinutes(Task.Program.Duration, FormatString.Format.Extended));
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

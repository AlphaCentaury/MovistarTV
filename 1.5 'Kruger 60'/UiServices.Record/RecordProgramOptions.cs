using IpTviewr.Common;
using IpTviewr.Services.EpgDiscovery;
using IpTviewr.UiServices.Discovery;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IpTviewr.UiServices.Record
{
    public partial class RecordProgramOptions : Form
    {
        public enum RecordOption
        {
            None = 0,
            Default = 10,
            EditSettings = 15,
            Channel = 20
        } // enum RecordOption

        public RecordProgramOptions()
        {
            InitializeComponent();
            this.Icon = Properties.Resources.Icon_Recorder;
        } // constructor

        public UiBroadcastService SelectedService
        {
            get;
            set;
        } // SelectedService

        public EpgProgram SelectedProgram
        {
            get;
            set;
        } // SelectedProgram

        public DateTime LocalReferenceTime
        {
            get;
            set;
        } // LocalReferenceTime

        public RecordOption SelectedOption
        {
            get;
            private set;
        } // SelectedOption
        public bool AllowRecordChannel
        {
            get;
            set;
        } // AllowRecordChannel

        private void RecordProgramOptions_Load(object sender, EventArgs e)
        {
            pictureChannelLogo.Image = SelectedService.Logo.GetImage(Configuration.Logos.LogoSize.Size48);
            labelChannelName.Text = string.Format("{0} {1}", SelectedService.DisplayLogicalNumber, SelectedService.DisplayName);

            if ((SelectedProgram == null) || (SelectedProgram.IsBlank))
            {
                labelProgramDescription.Visible = false;
                labelProgramSchedule.Visible = false;

                pictureIconInfo.Image = Properties.Resources.Status_Warning_16x16;
                labelInfo.Text = Properties.RecordChannel.SelectedProgramIsNullOrBlank;

                radioRecordProgramDefault.Enabled = false;
                radioRecordProgramEdit.Enabled = false;

                if (AllowRecordChannel)
                {
                    radioRecordChannel.Checked = true;
                }
                else
                {
                    radioRecordChannel.Enabled = false;
                    buttonOk.Enabled = false;
                } // if-else
            }
            else
            {
                labelProgramDescription.Text = SelectedProgram.Title;
                labelProgramSchedule.Text = string.Format("{0} ({1})", FormatString.DateTimeFromToMinutes(SelectedProgram.LocalStartTime, SelectedProgram.LocalEndTime, LocalReferenceTime),
                    FormatString.TimeSpanTotalMinutes(SelectedProgram.Duration, FormatString.Format.Extended));

                if (SelectedProgram.IsCurrent(LocalReferenceTime))
                {
                    pictureIconInfo.Image = Properties.Resources.Status_Info_16x16;
                    labelInfo.Text = Properties.RecordChannel.SelectedProgramIsRightNow;
                } // if

                radioRecordProgramDefault.Checked = true;
            } // if-else
        } // RecordProgramOptions_Load

        private void RecordProgramOptions_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (DialogResult != DialogResult.OK)
            {
                SelectedOption = RecordOption.None;
            } // if
        } // RecordProgramOptions_FormClosed

        private void radioRecordProgramDefault_CheckedChanged(object sender, EventArgs e)
        {
            SelectedOption = RecordOption.Default;
            ChangeOkButtonText(false);
        } // radioRecordProgramDefault_CheckedChanged

        private void radioRecordProgramEdit_CheckedChanged(object sender, EventArgs e)
        {
            SelectedOption = RecordOption.EditSettings;
            ChangeOkButtonText(true);
        } // radioRecordProgramEdit_CheckedChanged

        private void radioRecordChannel_CheckedChanged(object sender, EventArgs e)
        {
            SelectedOption = RecordOption.Channel;
            ChangeOkButtonText(true);
        } // radioRecordChannel_CheckedChanged

        private void ChangeOkButtonText(bool withOptions)
        {
            buttonOk.Text = withOptions ? Properties.RecordChannel.RecordButtonSettings : Properties.RecordChannel.RecordButtonDefault;
        } // ChangeOkButtonText
    } // class RecordProgramOptions
} // namespace

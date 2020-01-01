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
using IpTviewr.Services.EpgDiscovery;
using IpTviewr.UiServices.Common;
using IpTviewr.UiServices.Discovery;
using System;
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
            // logo, channel name and program data
            SetChannelDetails();

            // info text
            var validProgram = (SelectedProgram != null) && (!SelectedProgram.IsBlank);
            if (!validProgram)
            {
                pictureIconInfo.Image = Properties.Resources.Status_Warning_16x16;
                labelInfo.Text = Properties.RecordChannel.SelectedProgramIsNullOrBlank;
            }
            else
            {
                if (SelectedProgram.IsOld(LocalReferenceTime))
                {
                    pictureIconInfo.Image = Properties.Resources.Status_Info_16x16;
                    labelInfo.Text = Properties.RecordChannel.SelectedProgramIsOld;
                    validProgram = false;
                }
                else if (SelectedProgram.IsCurrent(LocalReferenceTime))
                {
                    pictureIconInfo.Image = Properties.Resources.Status_Info_16x16;
                    labelInfo.Text = Properties.RecordChannel.SelectedProgramIsRightNow;
                }
                else
                {
                    pictureIconInfo.Visible = false;
                    labelInfo.Visible = false;
                }
            } // if-else

            // radio buttons
            radioRecordChannel.Enabled = AllowRecordChannel;
            radioRecordProgramDefault.Enabled = validProgram;
            radioRecordProgramEdit.Enabled = validProgram;

            // check radio
            if (radioRecordProgramDefault.Enabled)
            {
                radioRecordProgramDefault.Checked = true;
            }
            else if (radioRecordChannel.Enabled)
            {
                radioRecordChannel.Checked = true;
            }
            else
            {
                buttonOk.Enabled = false;
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
            buttonOk.ChangeImage(withOptions? Properties.Resources.Action_Ok_16x16 : Properties.Resources.Action_RecordButton_16x16);
            buttonOk.Text = withOptions ? Properties.RecordChannel.RecordButtonWithSettings : Properties.RecordChannel.RecordButtonRecord;
        } // ChangeOkButtonText

        private void SetChannelDetails()
        {
            pictureChannelLogo.Image = SelectedService.Logo.GetImage(Configuration.Logos.LogoSize.Size64);
            labelChannelName.Text = $"{SelectedService.DisplayLogicalNumber} {SelectedService.DisplayName}";

            if (SelectedProgram != null)
            {
                labelProgramDescription.Text = SelectedProgram.Title;
                labelProgramSchedule.Text = $"{FormatString.DateTimeFromToMinutes(SelectedProgram.LocalStartTime, SelectedProgram.LocalEndTime, LocalReferenceTime)} ({FormatString.TimeSpanTotalMinutes(SelectedProgram.Duration, FormatString.Format.Extended)})";
            }
            else
            {
                labelChannelName.Top = pictureChannelLogo.Top;
                labelChannelName.Height = pictureChannelLogo.Height;

                labelProgramDescription.Visible = false;
                labelProgramSchedule.Visible = false;

            } // if-else
        } // SetChannelDetails
    } // class RecordProgramOptions
} // namespace

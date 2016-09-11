// Copyright (C) 2014-2016, Codeplex/GitHub user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using IpTviewr.Common;
using IpTviewr.Core.IpTvProvider;
using IpTviewr.UiServices.Discovery;
using IpTviewr.UiServices.Common.Forms;
using IpTviewr.Services.EpgDiscovery;

namespace IpTviewr.UiServices.EPG
{
    public partial class EpgProgramMiniBar : UserControl
    {
        private UiBroadcastService Service;
        private EpgProgram EpgProgram;

        public EpgProgramMiniBar()
        {
            InitializeComponent();
        } // constructor

        public void DisplayData(UiBroadcastService service, EpgProgram epgProgram, DateTime referenceTime, string caption)
        {
            Service = service;
            EpgProgram = epgProgram;

            labelProgramCaption.Text = caption;
            labelProgramCaption.Visible = caption != null;
            labelProgramTime.Visible = (epgProgram != null);
            labelProgramDetails.Visible = (epgProgram != null);
            pictureProgramThumbnail.ImageLocation = null;
            buttonProgramProperties.Visible = (epgProgram != null);
            pictureProgramThumbnail.Cursor = (epgProgram != null) ? Cursors.Hand : Cursors.Default;

            if (EpgProgram == null)
            {
                labelProgramTitle.Text = Properties.Texts.EpgNoInformation;
                pictureProgramThumbnail.Image = Properties.Resources.EpgNoProgramImage;
            }
            else
            {
                labelProgramTitle.Text = EpgProgram.Title;
                labelProgramTime.Text = string.Format("{0} ({1})", FormatString.DateTimeFromToMinutes(EpgProgram.LocalStartTime, EpgProgram.LocalEndTime, referenceTime),
                    FormatString.TimeSpanTotalMinutes(EpgProgram.Duration, FormatString.Format.Extended));
                labelProgramDetails.Text = string.Format("{0} / {1}", (EpgProgram.Genre != null) ? EpgProgram.Genre.Description : Properties.Texts.EpgNoGenre,
                    (EpgProgram.ParentalRating != null) ? EpgProgram.ParentalRating.Description : Properties.Texts.EpgNoParentalRating);

                pictureProgramThumbnail.Image = Properties.Resources.EpgLoadingProgramImage;
                pictureProgramThumbnail.ImageLocation = null;
                // TODO: EPG
                // pictureProgramThumbnail.ImageLocation = IpTvProvider.Current.EpgInfo.GetEpgProgramThumbnailUrl(service, EpgProgram, false);
            } // if-else
        }  // DisplayData

        private void buttonProgramProperties_Click(object sender, EventArgs e)
        {
            EpgExtendedInfoDialog.ShowExtendedInfo(this, Service, EpgProgram);
        } // buttonProgramProperties_Click

        private void pictureProgramThumbnail_LoadCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if ((e.Error != null) || (e.Cancelled))
            {
                (sender as PictureBox).Image = Properties.Resources.EpgNoProgramImage;
            } // if
        } // pictureProgramThumbnail_LoadCompleted

        private void pictureProgramThumbnail_Click(object sender, EventArgs e)
        {
            EpgExtendedInfoDialog.ShowExtendedInfo(this, Service, EpgProgram);
        } // pictureProgramThumbnail_Click
    } // class EpgProgramMiniBar
} // namespace

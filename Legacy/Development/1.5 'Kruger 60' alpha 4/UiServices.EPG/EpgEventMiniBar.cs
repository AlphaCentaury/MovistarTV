// Copyright (C) 2014-2015, Codeplex user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Project.DvbIpTv.Services.EPG;
using Project.DvbIpTv.Common;
using Project.DvbIpTv.Core.IpTvProvider;
using Project.DvbIpTv.UiServices.Discovery;
using Project.DvbIpTv.UiServices.Common.Forms;

namespace Project.DvbIpTv.UiServices.EPG
{
    public partial class EpgEventMiniBar : UserControl
    {
        private UiBroadcastService Service;
        private EpgEvent EpgEvent;

        public EpgEventMiniBar()
        {
            InitializeComponent();
        } // constructor

        public void DisplayData(UiBroadcastService service, EpgEvent epgEvent, DateTime referenceTime, string caption)
        {
            Service = service;
            EpgEvent = epgEvent;

            labelProgramCaption.Text = caption;
            labelProgramCaption.Visible = caption != null;
            labelProgramTime.Visible = (epgEvent != null);
            labelProgramDetails.Visible = (epgEvent != null);
            pictureProgramThumbnail.ImageLocation = null;
            buttonProgramProperties.Visible = (epgEvent != null);
            pictureProgramThumbnail.Cursor = (epgEvent != null) ? Cursors.Hand : Cursors.Default;

            if (epgEvent == null)
            {
                labelProgramTitle.Text = Properties.Texts.EpgNoInformation;
                pictureProgramThumbnail.Image = Properties.Resources.EpgNoProgramImage;
            }
            else
            {
                labelProgramTitle.Text = epgEvent.Title;
                labelProgramTime.Text = string.Format("{0} ({1})", FormatString.DateTimeFromToMinutes(epgEvent.LocalStartTime, epgEvent.LocalEndTime, referenceTime),
                    FormatString.TimeSpanTotalMinutes(epgEvent.Duration, FormatString.Format.Extended));
                labelProgramDetails.Text = string.Format("{0} / {1}", (epgEvent.Genre != null) ? epgEvent.Genre.Description : Properties.Texts.EpgNoGenre,
                    (epgEvent.ParentalRating != null) ? epgEvent.ParentalRating.Description : Properties.Texts.EpgNoParentalRating);

                pictureProgramThumbnail.Image = Properties.Resources.EpgLoadingProgramImage;
                pictureProgramThumbnail.ImageLocation = null;
                pictureProgramThumbnail.ImageLocation = IpTvProvider.Current.EpgInfo.GetEpgProgramThumbnailUrl(service, epgEvent, false);
            } // if-else
        }  // DisplayData

        private void buttonProgramProperties_Click(object sender, EventArgs e)
        {
            EpgExtendedInfoDialog.ShowExtendedInfo(this, Service, EpgEvent);
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
            EpgExtendedInfoDialog.ShowExtendedInfo(this, Service, EpgEvent);
        } // pictureProgramThumbnail_Click
    } // class EpgEventMiniBar
} // namespace

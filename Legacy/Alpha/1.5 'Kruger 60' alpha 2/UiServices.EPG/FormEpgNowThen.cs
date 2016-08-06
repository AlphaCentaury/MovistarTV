// Copyright (C) 2014-2015, Codeplex user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using Project.DvbIpTv.Common;
using Project.DvbIpTv.Common.Serialization;
using Project.DvbIpTv.Common.Telemetry;
using Project.DvbIpTv.Services.EPG;
using Project.DvbIpTv.UiServices.Configuration.Logos;
using Project.DvbIpTv.UiServices.Discovery;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Project.DvbIpTv.UiServices.EPG
{
    public partial class FormEpgNowThen : Form
    {
        private Image EpgLoadingProgramImage;
        private Image EpgNoProgramImage;

        public FormEpgNowThen()
        {
            InitializeComponent();
        } // constructor

        private void DisposeForm(bool disposing)
        {
            if (!disposing) return;

            if (EpgLoadingProgramImage != null) EpgLoadingProgramImage.Dispose();
            if (EpgNoProgramImage != null) EpgNoProgramImage.Dispose();
        } // DisposeForm

        public static void ShowEpgEvents(UiBroadcastService service, EpgEvent[] epg, IWin32Window owner, DateTime referenceTime) 
        {
            using (var form = new FormEpgNowThen())
            {
                form.DisplayData(service, epg, referenceTime);
                form.ShowDialog(owner);
            } // using form
        } // ShowEpgBasicData

        private void FormBasicEpgData_Load(object sender, EventArgs e)
        {
            BasicGoogleTelemetry.SendScreenHit(this);
            EpgLoadingProgramImage = Properties.Resources.EpgLoadingProgramImage;
            EpgNoProgramImage = Properties.Resources.EpgNoProgramImage;

            pictureBoxBefore.ErrorImage = EpgNoProgramImage;
            pictureBoxNow.ErrorImage = EpgNoProgramImage;
            pictureBoxThen.ErrorImage = EpgNoProgramImage;
        } // FormBasicEpgData_Load

        private void pictureBox_LoadCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                (sender as PictureBox).Image = EpgNoProgramImage;
            } // if
        } // pictureBox_LoadCompleted

        private void buttonOk_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void DisplayData(UiBroadcastService service, EpgEvent[] epg, DateTime referenceTime)
        {
            pictureChannelLogo.Image = service.Logo.GetImage(LogoSize.Size48, true);
            labelChannelName.Text = string.Format("{0}\r\n{1}", service.DisplayLogicalNumber, service.DisplayName);

            DisplayData((epg == null) ? null : epg[0], labelBeforeTime, labelBeforeTitle, labelBeforeDetails, pictureBoxBefore, referenceTime);
            DisplayData((epg == null) ? null : epg[1], labelNowTime, labelNowTitle, labelNowDetails, pictureBoxNow, referenceTime);
            DisplayData((epg == null) ? null : epg[2], labelThenTime, labelThenTitle, labelThenDetails, pictureBoxThen, referenceTime);
        } // DisplayData

        private void DisplayData(EpgEvent epg, Label time, Label title, Label details, PictureBox picture, DateTime referenceTime)
        {
            time.Visible = (epg != null);
            details.Visible = (epg != null);
            picture.ImageLocation = null;

            if (epg == null)
            {
                title.Text = Properties.Texts.EpgNoInformation;
                picture.Image = EpgNoProgramImage;
            }
            else
            {
                title.Text = epg.Title;
                time.Text = string.Format("{0} ({1})", FormatString.DateTimeFromToMinutes(epg.LocalStartTime, epg.LocalEndTime, referenceTime),
                    FormatString.TimeSpanTotalMinutes(epg.Duration, FormatString.Format.Extended));
                details.Text = string.Format("{0} / {1}", (epg.Genre != null) ? epg.Genre.Description : Properties.Texts.EpgNoGenre,
                    (epg.ParentalRating != null)? epg.ParentalRating.Description : Properties.Texts.EpgNoParentalRating);

                picture.Image = EpgLoadingProgramImage;

                // TODO: clean-up code; avoid fixed URL; allow for bad CRIDs
                try
                {
                    var cridUri = new Uri(epg.CRID);
                    var path = cridUri.LocalPath.Split('/');
                    var crid = path[2];
                    var baseCrid = crid.Substring(0, 4);

                    picture.ImageLocation = string.Format("http://172.26.22.23:2001/appclient/incoming/covers/programmeImages/landscape/big/{0}/{1}.jpg", baseCrid, crid);
                }
                catch
                {
                    // ignore
                } // try-catch
            } // if-else
        }  // DisplayData
    }
}

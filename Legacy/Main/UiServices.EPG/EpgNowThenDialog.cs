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
    public partial class EpgNowThenDialog : Form
    {
        private UiBroadcastService Service;
        private EpgEvent[] EpgEvents;
        private DateTime ReferenceTime;

        public EpgNowThenDialog()
        {
            InitializeComponent();
        } // constructor

        public static void ShowEpgEvents(UiBroadcastService service, EpgEvent[] epg, IWin32Window owner, DateTime referenceTime) 
        {
            using (var form = new EpgNowThenDialog())
            {
                form.Service = service;
                form.EpgEvents = epg;
                form.ReferenceTime = referenceTime;
                form.ShowDialog(owner);
            } // using form
        } // ShowEpgBasicData

        private void FormBasicEpgData_Load(object sender, EventArgs e)
        {
            BasicGoogleTelemetry.SendScreenHit(this);

            pictureChannelLogo.Image = Service.Logo.GetImage(LogoSize.Size48, true);
            labelChannelName.Text = string.Format("{0}\r\n{1}", Service.DisplayLogicalNumber, Service.DisplayName);

            epgEventBefore.DisplayData(Service, (EpgEvents != null) ? EpgEvents[0] : null, ReferenceTime, Properties.Texts.EpgProgramBeforeCaption);
            epgEventNow.DisplayData(Service, (EpgEvents != null) ? EpgEvents[1] : null, ReferenceTime, Properties.Texts.EpgProgramNowCaption);
            epgEventThen.DisplayData(Service, (EpgEvents != null) ? EpgEvents[2] : null, ReferenceTime, Properties.Texts.EpgProgramThenCaption);
        } // FormBasicEpgData_Load
    } // class EpgNowThenDialog
} // namespace

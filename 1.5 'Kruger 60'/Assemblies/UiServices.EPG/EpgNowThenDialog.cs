// Copyright (C) 2014-2017, GitHub/Codeplex user AlphaCentaury
// 
// All rights reserved, except those granted by the governing license of this software.
// See 'license.txt' file in the project root for complete license information.
// 
// http://movistartv.alphacentaury.org/ https://github.com/AlphaCentaury

using IpTviewr.Common;
using IpTviewr.Common.Serialization;
using IpTviewr.Common.Telemetry;
using IpTviewr.Services.EpgDiscovery;
using IpTviewr.UiServices.Configuration.Logos;
using IpTviewr.UiServices.Discovery;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace IpTviewr.UiServices.EPG
{
    public partial class EpgNowThenDialog : Form
    {
        private UiBroadcastService Service;
        private EpgProgram[] EpgPrograms;
        private DateTime ReferenceTime;

        public EpgNowThenDialog()
        {
            InitializeComponent();
        } // constructor

        public static void ShowEpgPrograms(UiBroadcastService service, EpgProgram[] epg, IWin32Window owner, DateTime referenceTime) 
        {
            using (var form = new EpgNowThenDialog())
            {
                form.Service = service;
                form.EpgPrograms = epg;
                form.ReferenceTime = referenceTime;
                form.ShowDialog(owner);
            } // using form
        } // ShowEpgBasicData

        private void FormBasicEpgData_Load(object sender, EventArgs e)
        {
            BasicGoogleTelemetry.SendScreenHit(this);

            pictureChannelLogo.Image = Service.Logo.GetImage(LogoSize.Size48, true);
            labelChannelName.Text = string.Format("{0}\r\n{1}", Service.DisplayLogicalNumber, Service.DisplayName);

            EpgProgramBefore.DisplayData(Service, (EpgPrograms != null) ? EpgPrograms[0] : null, ReferenceTime, Properties.Texts.EpgProgramBeforeCaption);
            EpgProgramNow.DisplayData(Service, (EpgPrograms != null) ? EpgPrograms[1] : null, ReferenceTime, Properties.Texts.EpgProgramNowCaption);
            EpgProgramThen.DisplayData(Service, (EpgPrograms != null) ? EpgPrograms[2] : null, ReferenceTime, Properties.Texts.EpgProgramThenCaption);
        } // FormBasicEpgData_Load
    } // class EpgNowThenDialog
} // namespace

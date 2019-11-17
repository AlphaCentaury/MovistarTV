// Copyright (C) 2014-2019, GitHub/Codeplex user AlphaCentaury
// 
// All rights reserved, except those granted by the governing license of this software.
// See 'license.txt' file in the project root for complete license information.
// 
// http://www.alphacentaury.org/movistartv https://github.com/AlphaCentaury

using IpTviewr.Common.Telemetry;
using IpTviewr.Services.EpgDiscovery;
using IpTviewr.UiServices.Configuration.Logos;
using IpTviewr.UiServices.Discovery;
using System;
using System.Windows.Forms;

namespace IpTviewr.UiServices.EPG
{
    public partial class EpgNowThenDialog : Form
    {
        private UiBroadcastService _service;
        private EpgProgram[] _epgPrograms;
        private DateTime _referenceTime;

        public EpgNowThenDialog()
        {
            InitializeComponent();
        } // constructor

        public static void ShowEpgPrograms(UiBroadcastService service, EpgProgram[] epg, IWin32Window owner, DateTime referenceTime) 
        {
            using (var form = new EpgNowThenDialog())
            {
                form._service = service;
                form._epgPrograms = epg;
                form._referenceTime = referenceTime;
                form.ShowDialog(owner);
            } // using form
        } // ShowEpgBasicData

        private void FormBasicEpgData_Load(object sender, EventArgs e)
        {
            BasicGoogleTelemetry.SendScreenHit(this);

            pictureChannelLogo.Image = _service.Logo.GetImage(LogoSize.Size48);
            labelChannelName.Text = $"{_service.DisplayLogicalNumber}\r\n{_service.DisplayName}";

            EpgProgramBefore.DisplayData(_service, (_epgPrograms != null) ? _epgPrograms[0] : null, _referenceTime, Properties.Texts.EpgProgramBeforeCaption);
            EpgProgramNow.DisplayData(_service, (_epgPrograms != null) ? _epgPrograms[1] : null, _referenceTime, Properties.Texts.EpgProgramNowCaption);
            EpgProgramThen.DisplayData(_service, (_epgPrograms != null) ? _epgPrograms[2] : null, _referenceTime, Properties.Texts.EpgProgramThenCaption);
        } // FormBasicEpgData_Load
    } // class EpgNowThenDialog
} // namespace

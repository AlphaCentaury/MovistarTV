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
using IpTviewr.UiServices.Common.Forms;
using IpTviewr.UiServices.Configuration.Properties;
using IpTviewr.UiServices.Configuration.Push.v1;
using System;
using System.Globalization;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IpTviewr.UiServices.Configuration.Push.UI
{
    internal partial class UpdatesForm : CommonBaseForm
    {
        private PushUpdate _update;
        private bool _userRequested;

        /// <summary>
        /// Wraps a IPushUpdateContext returning false to IsHidden and delegating the rest methods to the wrapped IPushUpdateContext.
        /// This allows to always display the latest update when updates are manually checked, even if it was hidden.
        /// </summary>
        protected sealed class UpdateContextWrapper : IPushUpdateContext
        {
            private readonly IPushUpdateContext _context;

            public UpdateContextWrapper(IPushUpdateContext context)
            {
                _context = context;
            } // constructor

            public bool IsHidden(Guid message) => false;

            public void AddHidden(Guid message) => _context.AddHidden(message);

            public Version GetAppVersion() => _context.GetAppVersion();

            public DateTime LastChecked
            {
                get => _context.LastChecked;
                set => _context.LastChecked = value;
            } // LastChecked
        } // class UpdateContextWrapper

        public UpdatesForm()
        {
            InitializeComponent();
            labelMessage.Text = Texts.PushCheckingForUpdates;
            labelDownload.Text = Texts.PushDownloadLink;
            checkDoNotShowAgain.Text = Texts.PushDoNotShowAgain;
            buttonOk.Text = Texts.ButtonOk;
            UpdateData = null;
        } // constructor

        public PushUpdate UpdateData
        {
            get => _update;
            set
            {
                labelVersion.Visible = value != null;
                labelReleaseDate.Visible = value != null;
                labelDownload.Visible = value != null;
                linkDownload.Visible = value != null;
                checkDoNotShowAgain.Visible = (value != null) && !_userRequested;
                _update = value;

                if (value == null) return;
                labelMessage.Text = Texts.PushNewUpdate;
                labelVersion.Text = string.Format(Texts.PushVersionFormat, value.DisplayVersion);
                labelReleaseDate.Text = string.Format(CultureInfo.CurrentCulture, Texts.PushReleasedOnFormat, value.ReleasedDate);
                linkDownload.Text = value.Link;
                iconBox.SetImage(Resources.Flag_Green_256x);
            } // set
        } // Update

        public IPushUpdateContext UpdateContext { get; set; }

        public bool DoNotShowAgain => checkDoNotShowAgain.Checked;

        private void linkDownload_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Launcher.OpenUrl(this, _update.DownloadUrl, HandleException, null);
        } // linkDownload_LinkClicked

        protected override async void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            Text = Texts.PushCheckForUpdates;
            if (UpdateData != null) return;

            _userRequested = true;
            var result = await PushManager.CheckForUpdatesAsync(new UpdateContextWrapper(UpdateContext));
            OnPushUpdateResult(result);
        } // OnLoad

        private void OnPushUpdateResult(PushUpdateResult result)
        {
            if (result.Updates == null)
            {
                iconBox.SetImage(Resources.Flag_Red_256x);
                labelMessage.Text = Texts.PushUpdateError;
                return;
            } // if

            if (result.LastUpdate == null)
            {
                iconBox.SetImage(Resources.Flag_Blue_256x);
                labelMessage.Text = Texts.PushNoUpdates;
            }
            else
            {
                UpdateData = result.LastUpdate;
            } // if-else
        } // OnPushUpdateResult
    } // class UpdatesForm
} // namespace

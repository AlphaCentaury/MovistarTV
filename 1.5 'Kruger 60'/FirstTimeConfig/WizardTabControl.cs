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

using IpTviewr.Common.Telemetry;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace IpTviewr.Tools.FirstTimeConfig
{
    internal class WizardTabControl : TabControl
    {
        public WizardTabControl()
        {
            IsPageAllowed = new Dictionary<string, bool>();
            Initialization = new Dictionary<string, Action>();
        } // constructor

        public Label LabelTitle
        {
            get;
            set;
        } // LabelTitle

        public Button PreviousButton
        {
            get;
            set;
        } // PreviousButton

        public Button NextButton
        {
            get;
            set;
        } // NextButton

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IDictionary<string, bool> IsPageAllowed
        {
            get;
            set;
        } // IsPageAllowed

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IDictionary<string, Action> Initialization
        {
            get;
            set;
        } // Initialization

        protected override void OnCreateControl()
        {
            base.OnCreateControl();

            if (DesignMode) return;

            UpdateWizardButtons();
            PreviousButton.Click += PreviousButton_Click;
            NextButton.Click += NextButton_Click;
            Selected += WizardTabControl_Selected;
        } // OnCreateControl

        protected override void WndProc(ref Message m)
        {
            // Hide tabs by trapping the TCM_ADJUSTRECT message
            if (m.Msg == 0x1328 && !DesignMode)
            {
                m.Result = (IntPtr)1;
            }
            else
            {
                base.WndProc(ref m);
            } // if-else
        } // WndProc

        void WizardTabControl_Selected(object sender, TabControlEventArgs e)
        {
            if (DesignMode) return;

            if ((LabelTitle != null) && (e.TabPage != null))
            {
                LabelTitle.Text = e.TabPage.ToolTipText;
            } // if
        } // WizardTabControl_Selected

        protected override void OnSelecting(TabControlCancelEventArgs e)
        {
            bool isAllowed;

            base.OnSelecting(e);
            if (DesignMode) return;

            if (e.TabPage == null) return;

            isAllowed = (IsPageAllowed.TryGetValue(e.TabPage.Name, out isAllowed)) ? isAllowed : false;
            e.Cancel = !isAllowed;

            if (isAllowed)
            {
                Action init;

                if (Initialization.TryGetValue(e.TabPage.Name, out init))
                {
                    Initialization.Remove(e.TabPage.Name);
                    init();
                    AppTelemetry.FormEvent(e.TabPage.Text, FindForm());
                } // if
            } // if
        } // OnSelecting

        protected override void OnSelected(TabControlEventArgs e)
        {
            base.OnSelected(e);
            if (DesignMode) return;

            UpdateWizardButtons();
        } // OnSelected

        private void PreviousButton_Click(object sender, EventArgs e)
        {
            SelectedIndex = SelectedIndex - 1;
        } // PreviousButton_Click

        private void NextButton_Click(object sender, EventArgs e)
        {
            SelectedIndex = SelectedIndex + 1;
        } // NextButton_Click

        public void UpdateWizardButtons()
        {
            bool isAllowed;

            isAllowed = false;
            if (SelectedIndex > 0)
            {
                var page = TabPages[SelectedIndex - 1];
                isAllowed = (IsPageAllowed.TryGetValue(page.Name, out isAllowed)) ? isAllowed : false;
            } // if
            PreviousButton.Enabled = isAllowed;

            isAllowed = false;
            if ((SelectedIndex + 1) < TabCount)
            {
                var page = TabPages[SelectedIndex + 1];
                isAllowed = (IsPageAllowed.TryGetValue(page.Name, out isAllowed)) ? isAllowed : false;
            } // if

            NextButton.Enabled = isAllowed;
        } // UpdateWizardButtons

        public void ShowWizardButtons(bool show)
        {
            if (show)
            {
                UpdateWizardButtons();
            } // if

            PreviousButton.Visible = show;
            NextButton.Visible = show;
        } // ShowWizardButtons
    } // class WizardTabControl
} // namespace

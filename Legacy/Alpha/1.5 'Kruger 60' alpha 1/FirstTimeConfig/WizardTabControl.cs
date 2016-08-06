// Copyright (C) 2014-2015, Codeplex user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Project.DvbIpTv.Tools.FirstTimeConfig
{
    internal class WizardTabControl : TabControl
    {
        public WizardTabControl()
        {
            IsPageAllowed = new Dictionary<string, bool>();
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

        public IDictionary<string, bool> IsPageAllowed
        {
            get;
            set;
        } // IsPageAllowed

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            if (DesignMode) return;

            UpdateWizardButtons();
            PreviousButton.Click += PreviousButton_Click;
            NextButton.Click += NextButton_Click;
            this.Selected += WizardTabControl_Selected;
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
            if (LabelTitle != null)
            {
                LabelTitle.Text = SelectedTab.ToolTipText;
            } // if
        } // WizardTabControl_Selected

        protected override void OnSelecting(TabControlCancelEventArgs e)
        {
            bool isAllowed;

            base.OnSelecting(e);
            if (DesignMode) return;

            isAllowed = (IsPageAllowed.TryGetValue(e.TabPage.Name, out isAllowed)) ? isAllowed : false;
            e.Cancel = !isAllowed;
        } // OnSelecting

        protected override void OnSelected(TabControlEventArgs e)
        {
            base.OnSelected(e);
            if (DesignMode) return;

            UpdateWizardButtons();
        } // OnSelected

        private void PreviousButton_Click(object sender, EventArgs e)
        {
            this.SelectedIndex = this.SelectedIndex - 1;
        } // PreviousButton_Click

        private void NextButton_Click(object sender, EventArgs e)
        {
            this.SelectedIndex = this.SelectedIndex + 1;
        } // NextButton_Click

        public void UpdateWizardButtons()
        {
            bool isAllowed;

            isAllowed = false;
            if (this.SelectedIndex > 0)
            {
                var page = TabPages[this.SelectedIndex - 1];
                isAllowed =(IsPageAllowed.TryGetValue(page.Name, out isAllowed))? isAllowed : false;
            } // if
            PreviousButton.Enabled = isAllowed;

            isAllowed = false;
            if ((this.SelectedIndex + 1) < this.TabCount)
            {
                var page = TabPages[this.SelectedIndex + 1];
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

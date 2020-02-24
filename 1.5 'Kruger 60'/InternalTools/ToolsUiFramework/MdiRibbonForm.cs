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

using ComponentFactory.Krypton.Ribbon;
using ComponentFactory.Krypton.Toolkit;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace IpTviewr.Internal.Tools.UiFramework
{
    public partial class MdiRibbonForm : KryptonForm, IRibbonMdiForm
    {
        private int _mdiChildCount;
        private readonly Dictionary<string, KryptonRibbonTab> _contexts;

        public MdiRibbonForm()
        {
            InitializeComponent();
            _contexts = new Dictionary<string, KryptonRibbonTab>();
        } // constructor

        protected int MdiChildCount
        {
            get => _mdiChildCount;
            private set
            {
                commandCloseWindow.Enabled = (value > 0) && (ActiveMdiChild != null);
                commandCloseAllWindows.Enabled = value > 1;
                commandCascade.Enabled = value > 1;
                commandTileHorizontal.Enabled = value > 1;
                commandTileVertical.Enabled = value > 1;
                _mdiChildCount = value;
            } // set
        } // MdiChildCount

        protected virtual Form CreateNewMdiChild()
        {
            return null;
        } // CreateNewMdiChild

        #region Overrides of KryptonForm/Form

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            MdiChildCount = 0;
        } // OnLoad

        [SuppressMessage("ReSharper", "SuspiciousTypeConversion.Global")]
        protected override void OnMdiChildActivate(EventArgs e)
        {
            base.OnMdiChildActivate(e);
            commandCloseWindow.Enabled = true;
            ribbon.SelectedContext = null;

            foreach (var mdiChild in MdiChildren.OfType<IRibbonMdiChild>())
            {
                mdiChild.IsActiveChild = ReferenceEquals(mdiChild.Form, ActiveMdiChild);
            } // foreach

            ribbonComboWindows.SelectedItem = ActiveMdiChild;
        } // OnMdiChildActivate

        #endregion

        #region Event handlers

        private void ribbonButtonNewTool_Click(object sender, EventArgs e)
        {
            try
            {
                CreateNewToolWindow();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw;
            }
        } // ribbonButtonNewTool_Click

        private void commandCloseWindow_Execute(object sender, EventArgs e)
        {
            ActiveMdiChild?.Close();
        } // commandCloseWindow_Execute

        private void commandCloseAllWindows_Execute(object sender, EventArgs e)
        {
            foreach (var child in MdiChildren)
            {
                child.Close();
            } // foreach
        } // commandCloseAllWindows_Execute

        private void commandCascade_Execute(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
            LayoutMdi(MdiLayout.ArrangeIcons);
        } // commandCascade_Execute

        private void commandTileHorizontal_Execute(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
            LayoutMdi(MdiLayout.ArrangeIcons);
        } // commandTileHorizontal_Execute

        private void commandTileVertical_Execute(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
            LayoutMdi(MdiLayout.ArrangeIcons);
        } // commandTileVertical_Execute

        private void timerClearStatus_Tick(object sender, EventArgs e)
        {
            timerClearStatus.Stop();
            statusLabelStatus.Text = "Ready";
        } // timerClearStatus_Tick

        private void ribbonComboWindows_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ribbonComboWindows.SelectedItem is Form form)
            {
                form.Activate();
            } // if
        } // ribbonComboWindows_SelectedIndexChanged

        #endregion

        protected bool CreateNewToolWindow()
        {
            return OnMdiChildCreated(CreateNewMdiChild());
        } // CreateNewToolWindow

        protected Image RibbonAppButtonImage
        {
            get => ribbon.RibbonAppButton.AppButtonImage;
            set => ribbon.RibbonAppButton.AppButtonImage = value;
        } // RibbonAppButtonImage

        [SuppressMessage("ReSharper", "SuspiciousTypeConversion.Global")]
        private bool OnMdiChildCreated(Form form)
        {
            if (form == null) return false;
            form.MdiParent = this;

            if (form is IRibbonMdiChild child)
            {
                child.RibbonMdiForm = this;
                CreateRibbonContexts(child.TypeGuid, child.GetChildContexts());
            } // if

            form.FormClosed += MdiChildOnFormClosed;
            form.Show();
            ribbonComboWindows.Items.Add(form);
            ribbonComboWindows.SelectedIndex = ribbonComboWindows.Items.Count - 1;
            MdiChildCount++;

            return true;
        } // OnMdiChildCreated

        private void CreateRibbonContexts(Guid guid, IEnumerable<MdiRibbonContext> contexts)
        {
            if (contexts == null) return;

            foreach (var context in contexts)
            {
                if ((context.Tabs == null) || (context.Tabs.Length == 0)) return;

                var fullName = $"{guid:N}-{context.Name}";
                if (_contexts.ContainsKey(fullName)) continue;

                KryptonRibbonTab first = null;
                foreach (var tab in context.Tabs)
                {
                    if (tab == null) continue;
                    if (first == null) first = tab;
                    tab.ContextName = fullName;
                    ribbon.RibbonTabs.Add(tab);
                } // foreach

                ribbon.RibbonContexts.Add(new KryptonRibbonContext
                {
                    ContextColor = context.Color,
                    ContextName = fullName,
                    ContextTitle = context.Title,
                });

                _contexts.Add(fullName, first);
            } // foreach
        } // CreateRibbonContexts

        private void MdiChildOnFormClosed(object sender, FormClosedEventArgs e)
        {
            if (!(sender is Form form)) return;

            form.FormClosed -= MdiChildOnFormClosed;
            form.Dispose();

            MdiChildCount--;

            ribbonComboWindows.Items.Remove(form);
        } // MdiChildOnFormClosed

        #region IRibbonMdiForm implementation

        void IRibbonMdiForm.SetActiveContexts(IRibbonMdiChild child, params string[] contexts)
        {
            if ((contexts == null) || (contexts.Length == 0)) return;
            var guid = child.TypeGuid;

            var selected = from context in contexts
                           select $"{guid:N}-{context}";
            ribbon.SelectedContext = string.Join(",", selected);

            if ((!_contexts.TryGetValue($"{guid:N}-{contexts[0]}", out var tab) || (tab == null))) return;

            ribbon.Refresh(); // needed to avoid a bug that crashes the app
            ribbon.SelectedTab = tab;
        } // IRibbonMdiForm.SetActiveContexts

        void IRibbonMdiForm.SetStatusText(string status)
        {
            timerClearStatus.Start();
            statusLabelStatus.Text = status;
            statusStrip.Refresh();
        } // IRibbonMdiForm.SetStatusText

        #endregion

    } // class MdiRibbonForm
} // namespace

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

using ComponentFactory.Krypton.Toolkit;
using IpTviewr.Internal.Tools.UiFramework;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace IpTviewr.Internal.Tools
{
    public partial class Form1 : KryptonForm, IRibbonMdiChild
    {
        private readonly bool _random;

        public Form1()
        {
            InitializeComponent();
            TypeGuid = Guid.Parse("{E01C14D8-CF0B-411D-AF4C-8E84B39D72FD}");
            _random = (new Random().Next() % 2) == 0;
        } // constructor

        #region Implementation of IRibbonMdiChild

        public IRibbonMdiForm RibbonMdiForm { private get; set; }

        public bool IsActiveChild
        {
            set
            {
                Text = value ? "I'm the active MDI Child!" : "Inactive";
                if (value) RibbonMdiForm.SetActiveContexts(this, _random ? "Test" : "2");
            }
        }

        public Form Form => this;

        public Guid TypeGuid { get; private set; }

        public MdiRibbonContext[] GetChildContexts()
        {
            return new[]
            {
                new MdiRibbonContext
                {
                Color = Color.Aqua,
                Name = "Test",
                Title = "Form1",
                Tabs = new[] {kryptonRibbonTab1, kryptonRibbonTab2}
                },

                new MdiRibbonContext
                {
                Color = Color.BurlyWood,
                Name = "2",
                Title = "F-2",
                Tabs = new[] {kryptonRibbonTab3}
                }
            };
        } // CreateContexts

        #endregion
    }
}

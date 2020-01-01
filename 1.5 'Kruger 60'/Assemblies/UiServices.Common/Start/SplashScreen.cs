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

using System.Windows.Forms;

namespace IpTviewr.UiServices.Common.Start
{
    internal partial class SplashScreen : Form
    {
        public SplashScreen()
        {
            InitializeComponent();
        }  // constructor

        private void SplashScreen_Load(object sender, System.EventArgs e)
        {
            Text = Application.ProductName;
        } // SplashScreen_Load
    } // class SplashScreen
} // namespace

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

using System;

namespace IpTviewr.UiServices.EPG
{
    public class EpgMiniBarButtonClickedEventArgs : EventArgs
    {
        public EpgMiniBarButtonClickedEventArgs(EpgMiniGuide.Button button)
        {
            Button = button;
        } // constructor

        public EpgMiniGuide.Button Button
        {
            get;
            private set;
        } // Button
    } // class EpgMiniBarButtonClickedEventArgs

    public class EpgMiniBarNavigationButtonsChangedEventArgs : EventArgs
    {
        public EpgMiniBarNavigationButtonsChangedEventArgs(bool backEnabled, bool forwardEnabled)
        {
            IsBackEnabled = backEnabled;
            IsForwardEnabled = forwardEnabled;
        } // constructor

        public bool IsBackEnabled
        {
            get;
            private set;
        } // IsBackEnabled

        public bool IsForwardEnabled
        {
            get;
            private set;
        } // IsForwardEnabled
    } // class EpgMiniBarNavigationButtonsChangedEventArgs
} // namespace

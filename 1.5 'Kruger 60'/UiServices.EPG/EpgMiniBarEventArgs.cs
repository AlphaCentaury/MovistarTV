// Copyright (C) 2014-2016, Codeplex user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project.DvbIpTv.UiServices.EPG
{
    public class EpgMiniBarButtonClickedEventArgs : EventArgs
    {
        public EpgMiniBarButtonClickedEventArgs(EpgMiniBar.Button button)
        {
            Button = button;
        } // constructor

        public EpgMiniBar.Button Button
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

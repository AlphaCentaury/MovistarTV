// Copyright (C) 2015, Codeplex user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Project.DvbIpTv.UiServices.Configuration;
using Project.DvbIpTv.UiServices.Discovery;

namespace Project.DvbIpTv.Core.IpTvProvider
{
    public abstract class IpTvProvider
    {
        #region Static methods

        public static IpTvProvider Current
        {
            get;
            set;
        } // Current

        #endregion

        #region IpTvProvider Members

        public EPG.IEpgInfoProvider EpgInfo
        {
            get;
            protected set;
        } // EpgInfo

        public abstract InitializationResult Initialize();

        #endregion
    } // class IpTvProvider
} // namespace

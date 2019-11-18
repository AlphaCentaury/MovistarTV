// Copyright (C) 2014-2019, GitHub/Codeplex user AlphaCentaury
// 
// All rights reserved, except those granted by the governing license of this software.
// See 'license.txt' file in the project root for complete license information.
// 
// http://www.alphacentaury.org/movistartv https://github.com/AlphaCentaury

using System;
using IpTviewr.IpTvServices.EPG;
using IpTviewr.UiServices.Configuration;

namespace IpTviewr.IpTvServices
{
    public abstract class IpTvService
    {
        private static IpTvService _current;

        #region Static methods

        public static IpTvService Current
        {
            get => _current;
            set
            {
                if (_current != null) throw new InvalidOperationException();
                _current = value ?? throw new ArgumentNullException(nameof(value));
            } // set
        } // Current

        #endregion

        #region IpTvService Members

        public abstract IEpgInfoProvider EpgInfo { get; }

        public abstract InitializationResult Initialize();

        #endregion
    } // class IpTvService
} // namespace

// Copyright (C) 2014-2019, GitHub/Codeplex user AlphaCentaury
// 
// All rights reserved, except those granted by the governing license of this software.
// See 'license.txt' file in the project root for complete license information.
// 
// http://www.alphacentaury.org/movistartv https://github.com/AlphaCentaury

using System.ComponentModel.Composition;
using IpTviewr.IpTvServices.MovistarPlus.Implementation;
using IpTviewr.UiServices.Configuration;
using IpTviewr.UiServices.Configuration.IpTvService;

namespace IpTviewr.IpTvServices.MovistarPlus
{
    [Export(typeof(ITvService))]
    public class IpTvServiceMovistarPlus : IpTvService
    {

        #region IpTvService implementation

        public override IEpgInfoProvider EpgInfo { get; }

        public override InitializationResult Initialize()
        {
            var result = base.Initialize();
            return result.IsError ? result : InitializationResult.Ok;

            //EpgInfo = new EpgInfoProvider();
            // return InitializationResult.Ok;
        } // Initialize

        protected override ITvServiceTexts CreateServiceTexts() => new MovistarTexts();

        #endregion
    } // class IpTvServiceMovistarPlus
} // namespace

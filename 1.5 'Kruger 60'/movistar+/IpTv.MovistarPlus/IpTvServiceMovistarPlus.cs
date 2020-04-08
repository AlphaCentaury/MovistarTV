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

using IpTviewr.IpTvServices.MovistarPlus.Implementation;
using System.ComponentModel.Composition;
using IpTviewr.Common.Configuration;

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

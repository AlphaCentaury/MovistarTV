// Copyright (C) 2014-2019, GitHub/Codeplex user AlphaCentaury
// 
// All rights reserved, except those granted by the governing license of this software.
// See 'license.txt' file in the project root for complete license information.
// 
// http://www.alphacentaury.org/movistartv https://github.com/AlphaCentaury

using IpTviewr.Common.Configuration;
using IpTviewr.IpTvServices.Implementation;

namespace IpTviewr.IpTvServices
{
    public abstract class IpTvService : ITvService
    {
        #region ITvService implementation

        public abstract IEpgInfoProvider EpgInfo { get; }

        public ITvServiceTexts Texts { get; private set; }

        public virtual InitializationResult Initialize()
        {
            Texts = CreateServiceTexts();
            if (Texts is ServiceTexts texts)
            {
                texts.Initialize();
            } // if

            return InitializationResult.Ok;
        } // Initialize

        #endregion

        protected virtual ITvServiceTexts CreateServiceTexts()
        {
            var result = new ServiceTexts();
            result.Initialize();

            return result;
        } // CreateServiceTexts
    } // class IpTvService
} // namespace
